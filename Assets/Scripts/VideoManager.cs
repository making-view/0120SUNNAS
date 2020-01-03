using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RenderHeads.Media.AVProVideo;

public class VideoManager : MonoBehaviour
{
    [SerializeField] private float uiDepth = 2.5f;
    [SerializeField] private float uiDistance = 4;

    [SerializeField] private MediaPlayer player;
    [SerializeField] private string windowsPath;
    [SerializeField] private string androidPath;
    private string activePath;

    [SerializeField] private GameObject cameraRigs;
    [SerializeField] private OVRCameraRig ovrCameraRig;
    [SerializeField] private GameObject desktopCameraRig;
    [SerializeField] private GameObject videoPlayerControls;

    private Vector3 cameraRigsStartPosition;
    private Camera activeCamera;
    private float preVideoForwardRotation;
    private OVRManager VRCameraManager;
    private StartRoom startRoom;

    private void Awake()
    {
        activePath = androidPath;
        var cameras = ovrCameraRig.GetComponentsInChildren<Camera>();
        var cameraList = new List<Camera>(cameras);

        activeCamera = cameraList.FirstOrDefault(c => c.enabled);

#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        activePath = windowsPath;
        activeCamera = desktopCameraRig.GetComponentInChildren<Camera>();
#endif

        VRCameraManager = ovrCameraRig.GetComponent<OVRManager>();
        startRoom = FindObjectOfType<StartRoom>();
        player.Events.AddListener(OnVideoEvent);
    }

    private void Start()
    {
        cameraRigsStartPosition = cameraRigs.transform.position;
    }

    private void Update()
    {
        var heightVector = new Vector3(0, activeCamera.transform.position.y - uiDepth, 0);
        var directionVector = Vector3.Normalize(Vector3.ProjectOnPlane(activeCamera.transform.forward, Vector3.up)) * uiDistance;
        var cameraPosition = activeCamera.transform.position;
        var newPosition = new Vector3(cameraPosition.x + directionVector.x, heightVector.y, cameraPosition.z + directionVector.z);
 
        videoPlayerControls.transform.position = newPosition;
        videoPlayerControls.transform.LookAt(activeCamera.transform);
    }

    public void PlayVideo(string fileName)
    {
        preVideoForwardRotation = activeCamera.transform.rotation.eulerAngles.y;
        player.OpenVideoFromFile(MediaPlayer.FileLocation.AbsolutePathOrURL, activePath + fileName, true);
        cameraRigs.transform.position = player.transform.position;
        VRCameraManager.trackingOriginType = OVRManager.TrackingOrigin.EyeLevel;
        videoPlayerControls.SetActive(true);

        foreach (var button in FindObjectsOfType<ExperienceCard>())
        {
            button.Deactivate();
        }

        startRoom.Leave();
        startRoom.narrationPlayer.Stop();
    }

    public void StopVideo()
    {
        var restoredViewDirection = new Vector3(activeCamera.transform.rotation.eulerAngles.x, preVideoForwardRotation, activeCamera.transform.rotation.eulerAngles.z);

        player.Stop();
        cameraRigs.transform.position = cameraRigsStartPosition;
        activeCamera.transform.rotation = Quaternion.Euler(restoredViewDirection);
        ovrCameraRig.trackerAnchor.rotation = Quaternion.Euler(restoredViewDirection);
        VRCameraManager.trackingOriginType = OVRManager.TrackingOrigin.FloorLevel;
        videoPlayerControls.SetActive(false);

        startRoom.Enter();
    }

    public void OnVideoEvent(MediaPlayer mp, MediaPlayerEvent.EventType et, ErrorCode errorCode)
    {
        switch (et)
        {
            case MediaPlayerEvent.EventType.FinishedPlaying:
                StopVideo();
                break;
        }
    }
}

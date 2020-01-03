using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using RenderHeads.Media.AVProVideo;

public class StartRoom : MonoBehaviour
{
    [SerializeField] private string videoFileName;
    [SerializeField] private string windowsPath;
    [SerializeField] private string androidPath;
    [SerializeField] private float restartThreshold = 3.0f;

    private MediaPlayer scenePlayer;
    private string activePath;
    private bool isPlayingScene = false;
    private bool resetOnHMDMount = false;

    [SerializeField] public AudioSource narrationPlayer;


    void Awake()
    {
        scenePlayer = GetComponent<MediaPlayer>();
        activePath = androidPath;

        OVRManager.HMDMounted += HandleHMDMounted;
        OVRManager.HMDUnmounted += HandleHMDUnmounted;

#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        activePath = windowsPath;
#endif

        scenePlayer.OpenVideoFromFile(MediaPlayer.FileLocation.AbsolutePathOrURL, activePath + videoFileName, true);
        narrationPlayer.Play();
        isPlayingScene = true;
    }

    public void Leave()
    {
        scenePlayer.Stop();
        isPlayingScene = false;
    }

    public void Enter()
    {
        if (!isPlayingScene)
        {
            scenePlayer.Play();
        }
    }

    void HandleHMDMounted()
    {
        StopAllCoroutines();

        if (resetOnHMDMount)
        {
            narrationPlayer.Play();
        }
    }

    void HandleHMDUnmounted()
    {
        var videoManager = FindObjectOfType<VideoManager>();
        videoManager.StopVideo();
        narrationPlayer.Stop();

        StartCoroutine(ResetCountdown());
    }

    IEnumerator ResetCountdown()
    {
        float totalTime = 0;

        while (totalTime < restartThreshold)
        {
            totalTime += Time.deltaTime;

            yield return null;
        }

        resetOnHMDMount = true;
    }
}

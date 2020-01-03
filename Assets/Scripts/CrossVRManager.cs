using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossVRManager : MonoBehaviour
{
    public GameObject desktopCameraRig;
    public GameObject ovrCameraRig;

    void Awake()
    {
#if UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN || UNITY_EDITOR
        desktopCameraRig.SetActive(true);
        ovrCameraRig.SetActive(false);
#elif UNITY_ANDROID
        ovrCameraRig.SetActive(true);
        desktopCameraRig.SetActive(false);
#endif
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : ProgressBarButton
{
    [SerializeField] public Image progressBarFillImage;

    public void Start()
    {
        progressAudio = GetComponent<AudioSource>();
        progressBar = progressBarFillImage;
    }

    public override void Proceed()
    {
        var videoManager = FindObjectOfType<VideoManager>();
        videoManager.StopVideo();
        ResetProgress();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartExperienceButton : ProgressBarButton
{
    [SerializeField] public Image progressBarFillImage;
    [SerializeField] public Text titleBarText;
    [SerializeField] public string attractionVideoFile;

    public void Start()
    {
        progressAudio = GetComponent<AudioSource>();
        progressBar = progressBarFillImage;
    }

    public override void Proceed()
    {
        var videoManager = FindObjectOfType<VideoManager>();
        videoManager.PlayVideo(attractionVideoFile);
        ResetProgress();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ProgressBarButton : MonoBehaviour
{
    private Image _progressBar;
    private AudioSource _progressAudio;

    public bool isProgressBarFilled { get { return _progressBar.fillAmount >= 1.0f; } }
    public Image progressBar { set { _progressBar = value; } }
    public AudioSource progressAudio { set { _progressAudio = value; } }

    public void SetProgress(float progress)
    {
        try
        {
            _progressBar.fillAmount = progress;

            if (!_progressAudio.isPlaying)
            {
                _progressAudio.Play();
            }
        }
        catch (NullReferenceException)
        {
            Debug.LogError("progressBar and/or progressBarAudio is missing");
        }
    }

    public void ResetProgress()
    {
        try
        {
            _progressBar.fillAmount = 0;
            _progressAudio.Stop();
        }
        catch (NullReferenceException)
        {
            Debug.LogError("progressBar and/or progressBarAudio is missing");
        }
    }

    public abstract void Proceed();
}

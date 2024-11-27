using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class Video : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject video;

    private void Start()
    {
        videoPlayer.loopPointReached += OnOverVideo;
    }

    public void OnOverVideo(VideoPlayer videos)
    {
        video.SetActive(false);
    }
}

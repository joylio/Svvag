using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoStreaming : MonoBehaviour {

    public VideoClip[] videoClips;

    private int clipIndex = 0;

    void OnEnable()
    {
        VideoClip clip = videoClips[0];
        StartCoroutine(PlayClip(clip));
    }

    public void PlayNextClip()
    {
        StopCoroutine(PlayClip(videoClips[clipIndex]));
        if (clipIndex == 2)
        {
            clipIndex = -1;
        }
        VideoClip clip = videoClips[++clipIndex];
        StartCoroutine(PlayClip(clip));
    }

    public void TogglePlay()
    {
        VideoPlayer videoPlayer = GetComponent<VideoPlayer>();
        if (videoPlayer != null)
        {
            if(videoPlayer.isPlaying)
            {
                videoPlayer.Pause();
            }
            else
            {
                videoPlayer.Play();
            }
        }
    }


    IEnumerator PlayClip(VideoClip c)
    {
        VideoPlayer videoPlayer = GetComponent<VideoPlayer>();
        if (!videoPlayer)
        {
            videoPlayer = gameObject.AddComponent<VideoPlayer>();
        }
        videoPlayer.clip = c;

        while (!videoPlayer.isPrepared)
        {
            Debug.Log("---------loading video");
            yield return new WaitForSeconds(1f);
            break;
        }

        videoPlayer.playOnAwake = true;
        RawImage image = GetComponent<RawImage>();
        image.texture = videoPlayer.texture;
        image.gameObject.transform.forward = -Camera.main.transform.forward;
        videoPlayer.Play();
    }
}

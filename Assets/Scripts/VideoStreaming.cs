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
        StartCoroutine(PlayVideo(clip));
    }

    public void PlayNextClip()
    {
        if (clipIndex == 2)
        {
            clipIndex = -1;
        }
        VideoClip clip = videoClips[++clipIndex];
        StartCoroutine(PlayVideo(clip));
    }


    IEnumerator PlayVideo(VideoClip c)
    {
        VideoPlayer videoPlayer = gameObject.AddComponent<VideoPlayer>();
        videoPlayer.clip = c;

        while (!videoPlayer.isPrepared)
        {
            Debug.Log("---------loading video");
            yield return new WaitForSeconds(0.5f);
            break;
        }

        videoPlayer.playOnAwake = true;
        RawImage image = GetComponent<RawImage>();
        image.texture = videoPlayer.texture;
        image.gameObject.transform.forward = -Camera.main.transform.forward;
        videoPlayer.Play();
    }
}

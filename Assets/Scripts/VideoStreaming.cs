using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoStreaming : MonoBehaviour {

    public VideoClip clip;

    void OnEnable()
    {
        StartCoroutine(PlayVideo());
    }

    IEnumerator PlayVideo()
    {
        VideoPlayer videoPlayer = gameObject.AddComponent<VideoPlayer>();
        videoPlayer.clip = clip;
        
        while(!videoPlayer.isPrepared)
        {
            Debug.Log("------------------");
            yield return new WaitForSeconds(1);
            break;
        }

        videoPlayer.playOnAwake = true;
        RawImage image = GetComponent<RawImage>();
        image.texture = videoPlayer.texture;
        image.gameObject.transform.forward = -Camera.main.transform.forward;
        videoPlayer.Play();
    }
}

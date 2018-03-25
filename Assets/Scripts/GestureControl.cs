using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class GestureControl : MonoBehaviour {

    public float distanceAsSwipe;

    private Vector2 startPos;
    private Vector2 endPos;

    void Awake()
    {
        startPos = Vector2.zero;
        endPos = Vector2.zero;
    }

	void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                startPos = touch.position;
                endPos = touch.position;
            }
            else if(touch.phase == TouchPhase.Moved)
            {
                // TODO: Move the texture along the swipe 
            }
            else if(touch.phase == TouchPhase.Ended)
            {
                endPos = touch.position;

                float dist = Vector2.Distance(startPos, endPos);

                // Click gesture recognized.
                if(dist == 0f)
                {
                    GetComponent<VideoStreaming>().TogglePlay();
                }

                // Swipe gesture recognized.
                else if (dist >= distanceAsSwipe)
                {
                    GetComponent<VideoStreaming>().PlayNextClip();
                }

                //// TODO: If not swipe, move the texture back in place.
                //else
                //{

                //}
            }
        }
        else
        {
            startPos = Vector2.zero;
            endPos = Vector2.zero;
        }
    }
}

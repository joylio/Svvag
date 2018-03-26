using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class GestureControl : MonoBehaviour {

    public float swipeToNextDistance = 80f;
    public float swipeMinDistance;

    private RawImage m_image;
    private Rect m_originUVRect;

    private VideoStreaming m_videoStreaming;

    private Vector2 m_startPos;
    private Vector2 m_endPos;
    private float m_dist;
    private bool m_firstMove;

    void Awake()
    {
        m_image = GetComponent<RawImage>();
        m_originUVRect = m_image.uvRect;
        m_videoStreaming = GetComponent<VideoStreaming>();
        ResetTouchPos();
        m_dist = 0f;
        m_firstMove = true;
    }

	void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch(touch.phase)
            {
                case TouchPhase.Began:
                    m_startPos = touch.position;
                    m_endPos = touch.position;
                    m_firstMove = true;
                    break;

                case TouchPhase.Moved:
                    m_endPos = touch.position;
                    m_dist = m_endPos.x - m_startPos.x;

                    if (Mathf.Abs(m_dist) >= swipeMinDistance)
                    {
                        if (m_firstMove)
                        {
                            m_videoStreaming.TogglePlay();
                            m_firstMove = false;
                        }


                        m_image.uvRect = new Rect(new Vector2(m_originUVRect.x + m_dist / 100, m_originUVRect.y), m_originUVRect.size);
                    }

                    break;

                case TouchPhase.Ended:
                    m_endPos = touch.position;
                    m_dist = m_endPos.x - m_startPos.x;

                    // Swipe gesture recognized.
                    if (Mathf.Abs(m_dist) >= swipeToNextDistance)
                    {
                        m_image.uvRect = m_originUVRect;
                        m_videoStreaming.PlayNextClip();
                    }
                    // Click gesture recognized
                    // OR
                    // Swipe fails -> Move texture back in place.
                    else
                    {
                        m_videoStreaming.TogglePlay();
                        m_image.uvRect = m_originUVRect;
                    }

                    break;
            }
        }
        else
        {
            ResetTouchPos();
        }
    }

    private void ResetTouchPos()
    {
        m_startPos = Vector2.zero;
        m_endPos = Vector2.zero;
    }
}

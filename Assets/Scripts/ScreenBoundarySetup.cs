using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBoundarySetup : MonoBehaviour {

    [HideInInspector]
    public GameObject menuCanvas;

    public float thickness = 10f;

    private Camera m_cam;
    private Vector3 m_leftPos, m_rightPos, m_topPos, m_bottomPos;
    private float m_frustumHeight, m_frustumWidth;

    void Awake()
    {
        m_cam = Camera.main;
    }

    void Update()
    {
        if(menuCanvas.activeInHierarchy)
        {
            UpdateBoundary();
        }
    }

	private void UpdateBoundary()
    {
        m_frustumHeight = Mathf.Abs(2.0f * menuCanvas.transform.position.z * Mathf.Tan(m_cam.fieldOfView * 0.5f * Mathf.Deg2Rad));
        m_frustumWidth = Mathf.Abs(m_frustumHeight * m_cam.aspect);
        //Debug.Log("frustum height is " + m_frustumHeight);
        //Debug.Log("frustum width is " + m_frustumWidth);
        float distToCam = Mathf.Abs(menuCanvas.transform.position.z);
        //Debug.Log("distance to camera is " + distToCam);
        m_leftPos = m_cam.ViewportToWorldPoint(new Vector3(0f, 0.5f, distToCam));
        m_rightPos = m_cam.ViewportToWorldPoint(new Vector3(1f, 0.5f, distToCam));
        m_topPos = m_cam.ViewportToWorldPoint(new Vector3(0.5f, 1f, distToCam));
        m_bottomPos = m_cam.ViewportToWorldPoint(new Vector3(0.5f, 0f, distToCam));
        for (int i = 0; i < 4; i++)
        {
            BoxCollider col = transform.GetChild(i).GetComponent<BoxCollider>();
            if(!col)
            {
                Debug.Log("!!!!!!!!!!!!NO COLLIDER");
            }
            switch (i)
            {
                case 0:
                    // Set up the size.
                    col.size = new Vector3(thickness, m_frustumHeight, thickness);
                    // Set up the position.
                    col.transform.position = m_leftPos;
                    break;
                case 1:
                    col.size = new Vector3(thickness, m_frustumHeight, thickness);
                    col.transform.position = m_rightPos;
                    break;
                case 2:
                    col.size = new Vector3(m_frustumWidth, thickness, thickness);
                    col.transform.position = m_topPos;
                    break;
                case 3:
                    col.size = new Vector3(m_frustumWidth, thickness, thickness);
                    col.transform.position = m_bottomPos;
                    break;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBoundarySetup : MonoBehaviour {

    [HideInInspector]
    public GameObject menuCanvas;

    public float thickness = 10f;

    private Camera cam;
    private Vector3 leftPos, rightPos, topPos, bottomPos;
    private float frustumHeight, frustumWidth;

    void Awake()
    {
        cam = Camera.main;
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
        frustumHeight = Mathf.Abs(2.0f * menuCanvas.transform.position.z * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad));
        frustumWidth = Mathf.Abs(frustumHeight * cam.aspect);
        //Debug.Log("frustum height is " + frustumHeight);
        //Debug.Log("frustum width is " + frustumWidth);
        float distToCam = Mathf.Abs(menuCanvas.transform.position.z);
        //Debug.Log("distance to camera is " + distToCam);
        leftPos = cam.ViewportToWorldPoint(new Vector3(0f, 0.5f, distToCam));
        rightPos = cam.ViewportToWorldPoint(new Vector3(1f, 0.5f, distToCam));
        topPos = cam.ViewportToWorldPoint(new Vector3(0.5f, 1f, distToCam));
        bottomPos = cam.ViewportToWorldPoint(new Vector3(0.5f, 0f, distToCam));
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
                    col.size = new Vector3(thickness, frustumHeight, thickness);
                    // Set up the position.
                    col.transform.position = leftPos;
                    break;
                case 1:
                    col.size = new Vector3(thickness, frustumHeight, thickness);
                    col.transform.position = rightPos;
                    break;
                case 2:
                    col.size = new Vector3(frustumWidth, thickness, thickness);
                    col.transform.position = topPos;
                    break;
                case 3:
                    col.size = new Vector3(frustumWidth, thickness, thickness);
                    col.transform.position = bottomPos;
                    break;
            }
        }
    }
}

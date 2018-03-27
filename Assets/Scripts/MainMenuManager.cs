using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class MainMenuManager : MonoBehaviour {

    public Vector3[] StartPos
    {
        get
        {
            return m_startPos;
        }
    }

    [Header("Smooth Menu Movement")]
    public float force = 50f;

    [Header("Info Button Related")]
    public GameObject infoCanvas;

    private Vector3[] m_startPos;
    private int m_count;

    private bool m_infoBtnSelected;
    private bool m_likeBtnSelected;

    void Awake()
    {
        m_count = transform.childCount;
        m_startPos = new Vector3[m_count];
        for (int i = 0; i < m_count; i++)
        {
            RectTransform trans = transform.GetChild(i).GetComponent<RectTransform>();
            m_startPos[i] = trans.localPosition;
        }
        m_infoBtnSelected = false;
        m_likeBtnSelected = false;
    }

    void Update()
    {
        if(!m_infoBtnSelected)
        {
            for (int i = 0; i < m_count; i++)
            {
                RectTransform item = transform.GetChild(i).GetComponent<RectTransform>();
                if (item.localPosition != m_startPos[i])
                {
                    Vector3 dir = m_startPos[i] - item.localPosition;
                    //Debug.DrawRay(item.localPosition, dir);
                    item.gameObject.GetComponent<Rigidbody>().AddForce(dir * force);
                }
            }
        }
    }

    public void ResetLayout()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<RectTransform>().localPosition = StartPos[i];
        }
    }

    public void OnLikeButtonClick(Button like)
    {
        m_likeBtnSelected = !m_likeBtnSelected;
        Color color = like.gameObject.GetComponent<UnityEngine.UI.Image>().color;
        if (m_likeBtnSelected)
        {
            color = Color.red;
            like.gameObject.GetComponent<UnityEngine.UI.Image>().color = color;
        }
        else
        {
            color = Color.white;
            like.gameObject.GetComponent<UnityEngine.UI.Image>().color = color;
        }
        
    }

    public void OnInfoButtonClick(Button info)
    {
        m_infoBtnSelected = !m_infoBtnSelected;
        if(m_infoBtnSelected)
        {
            CameraDevice.Instance.Stop();
            MoveToTopLeft(info);
            infoCanvas.SetActive(true);
        }
        else
        {
            CameraDevice.Instance.Start();
            infoCanvas.SetActive(false);
        }
    }

    private void MoveToTopLeft(Button btn)
    {
        Vector3 btnPos = btn.gameObject.transform.position;
        Vector3 forceOrigin = Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f, btnPos.z));
        Vector3 dir = forceOrigin - btnPos;
        btn.gameObject.GetComponent<Rigidbody>().AddForce(dir * force * 10f);
    }
}

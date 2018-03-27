using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private Vector3[] m_startPos;
    private int m_count;

    void Awake()
    {
        m_count = transform.childCount;
        m_startPos = new Vector3[m_count];
        for (int i = 0; i < m_count; i++)
        {
            RectTransform trans = transform.GetChild(i).GetComponent<RectTransform>();
            m_startPos[i] = trans.localPosition;
        }
    }

    void FixedUpdate()
    {
        for(int i = 0; i < m_count; i++)
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

    public void ResetLayout()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<RectTransform>().localPosition = StartPos[i];
        }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class MenuManager : MonoBehaviour {

    [Header("Menu Position")]
    public float distanceToCam = 1000f;

    //[Header("Menu Layout")]
    //public GameObject mainMenu;
    //public GameObject secondaryMenu;
    //public float menuSpacing;

    [Header("Screen Boundary Setup")]
    public ScreenBoundarySetup screenBoundarySetup;


    void Update()
    {
        DisplayMenu();
    }

    private void DisplayMenu()
    {
        // Get the Vuforia StateManager
        StateManager sm = TrackerManager.Instance.GetStateManager();

        // Query the StateManager to retrieve the list of
        // currently 'active' trackables 
        //(i.e. the ones currently being tracked by Vuforia)
        IEnumerable<TrackableBehaviour> activeTrackables = sm.GetActiveTrackableBehaviours();

        // Iterate through the list of active trackables
        foreach (TrackableBehaviour tb in activeTrackables)
        {
            Debug.Log("Trackable: " + tb.TrackableName);
            GameObject trackable = GameObject.Find(tb.TrackableName);
            if(trackable)
            {
                screenBoundarySetup.menuCanvas = gameObject;
            }
            Vector3 pos = trackable.transform.position;
            //Debug.DrawRay(Camera.main.transform.position, pos);
            transform.up = -Camera.main.transform.forward;
            transform.position = Camera.main.ScreenToWorldPoint(Camera.main.WorldToScreenPoint(pos) + new Vector3(0f, 0f, distanceToCam));
        }
    }

    //public void OnMainMenuButtonClick(Button btn)
    //{
    //    if(secondaryMenu.activeSelf == true)
    //    {
    //        secondaryMenu.SetActive(false);
    //    }
    //    else
    //    {
    //        secondaryMenu.SetActive(true);
    //        Ray ray = new Ray(transform.position, -transform.position + btn.gameObject.transform.position);
    //        secondaryMenu.transform.position = ray.GetPoint(Vector3.Distance(transform.position, btn.gameObject.transform.position) + menuSpacing);
    //    }
    //}
}

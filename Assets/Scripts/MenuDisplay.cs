using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class MenuDisplay : MonoBehaviour {

    public float distanceToCam = 800f;

    public GameObject secondaryMenu;
    public float menuSpacing;

    private float offsetX = -400f;
    private float offsetY = -200f;

	void Update()
    {
        // Get the Vuforia StateManager
        StateManager sm = TrackerManager.Instance.GetStateManager();

        // Query the StateManager to retrieve the list of
        // currently 'active' trackables 
        //(i.e. the ones currently being tracked by Vuforia)
        IEnumerable<TrackableBehaviour> activeTrackables = sm.GetActiveTrackableBehaviours();

        // Iterate through the list of active trackables
        Debug.Log("List of trackables currently active (tracked): ");
        foreach (TrackableBehaviour tb in activeTrackables)
        {
            Debug.Log("Trackable: " + tb.TrackableName);
            GameObject trackable = GameObject.Find(tb.TrackableName);
            Vector3 pos = trackable.transform.position;
            Debug.DrawRay(Camera.main.transform.position, pos);
            transform.up = -Camera.main.transform.forward;
            transform.position = Camera.main.WorldToScreenPoint(pos) + new Vector3(offsetX, offsetY, distanceToCam);
        }
    }

    public void OnMainMenuButtonClick(Button btn)
    {
        if(secondaryMenu.activeSelf == true)
        {
            secondaryMenu.SetActive(false);
        }
        else
        {
            secondaryMenu.SetActive(true);
            Ray ray = new Ray(transform.position, -transform.position + btn.gameObject.transform.position);
            secondaryMenu.transform.position = ray.GetPoint(Vector3.Distance(transform.position, btn.gameObject.transform.position) + menuSpacing);
        }
    }
}

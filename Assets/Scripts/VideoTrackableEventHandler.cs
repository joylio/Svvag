using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoTrackableEventHandler : DefaultTrackableEventHandler {

    public GameObject videoMenu;
    
    override protected void OnTrackingFound()
    {
        base.OnTrackingFound();
        videoMenu.SetActive(true);
    }

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
        videoMenu.SetActive(false);
    }
}

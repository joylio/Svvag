﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoTrackableEventHandler : DefaultTrackableEventHandler {

    public GameObject videoMenu;
    public GameObject videoControlCanvas;
    
    override protected void OnTrackingFound()
    {
        base.OnTrackingFound();
        videoMenu.SetActive(true);
    }

    override protected void OnTrackingLost()
    {
        base.OnTrackingLost();
        videoMenu.SetActive(false);
        videoControlCanvas.SetActive(false);
    }
}

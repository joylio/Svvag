using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageTrackableEventHandler : DefaultTrackableEventHandler {

    #region PUBLIC_MEMBER_VARIABLES

    public GameObject menu;

    #endregion // PUBLIC_MEMBER_VARIABLES

    override protected void OnTrackingFound()
    {
        base.OnTrackingFound();

        // Enable menu:
        menu.SetActive(true);
    }

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();

        // Disable menu:
        menu.SetActive(false);
    }
}

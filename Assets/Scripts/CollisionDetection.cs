using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour {

	void OnCollisionEnter(Collision col)
    {
        Debug.Log("~~~~~~~" + col.collider.name);
    }
}

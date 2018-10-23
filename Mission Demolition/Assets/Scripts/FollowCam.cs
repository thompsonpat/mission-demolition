using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{

    // static point of interest
    static public GameObject POI;

	[Header ("Set in Inspector")]
	public float easing = 0.05f;
	public Vector2 minXY = Vector2.zero;

    [Header("Set Dynamically")]
    public float camZ;

    void Awake()
    {
        camZ = this.transform.position.z;
    }

    void FixedUpdate()
    {
        if (POI == null) return;
		Vector3 destination = POI.transform.position;
		// Limit the X & Y to minimum values
		destination.x = Mathf.Max(minXY.x, destination.x);
		destination.y = Mathf.Max(minXY.y, destination.y);
		destination = Vector3.Lerp(transform.position, destination, easing);
		// Keep camera away on Z axis
		destination.z = camZ;
		// Set the camera to the destination
		transform.position = destination;
		// Change orthographicSize to keep ground in view
		Camera.main.orthographicSize = destination.y + 10;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

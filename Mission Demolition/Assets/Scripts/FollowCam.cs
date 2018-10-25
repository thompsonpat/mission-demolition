using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{

    // static point of interest
    static public GameObject POI;

    [Header("Set in Inspector")]
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
        Vector3 destination;

        // If there is no POI, return to P:[0,0,0]
        if (POI == null)
        {
            destination = Vector3.zero;
        }
        else
        {
            // Get the position of the POI
            destination = POI.transform.position;
            // If POI is a Projectile, check to see if it's at rest
            if (POI.tag == "Projectile")
            {
                // If it is sleeping (not moving)
                if (POI.GetComponent<Rigidbody>().IsSleeping())
                {
                    // Return to default view
                    POI = null;
                    // In the next update
                    return;
                }
            }
        }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLine : MonoBehaviour
{

    static public ProjectileLine S; // Singleton

    [Header("Set in Inspector")]
    public float minDist = 0.1f;

    private LineRenderer line;
    private GameObject _poi;
    private List<Vector3> points;

    void Awake()
    {
        // Set the singleton
        S = this;

        // Get a reference to the LineRenderer
        line = GetComponent<LineRenderer>();

        // Disable the LineRenderer until it's needed
        line.enabled = false;

        // Initialize the points list
        points = new List<Vector3>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        // If there is no poi, search for one
        if (poi == null)
        {
            if (FollowCam.POI != null)
            {
                if (FollowCam.POI.tag == "Projectile")
                {
                    poi = FollowCam.POI;
                }

                // If we didn't find a poi
                else { return; }
            }

            // If we didn't find a poi
            else { return; }
        }

		// If there is a poi, it's location is added every FixedUpdate
		AddPoint();

		// Once FollowCam.POI is null, make the local point null too
		if (FollowCam.POI == null) { poi = null; }

    }

    public GameObject poi
    {
        get { return (_poi); }
        set
        {
			_poi = value;
            if (_poi != null)
            {
                // When _poi is set to something new, it resets everything
                line.enabled = false;
                points = new List<Vector3>();
                AddPoint();
            }
        }
    }

    // This can be used to clear the line directly
    public void Clear()
    {
        _poi = null;
        line.enabled = false;
        points = new List<Vector3>();
    }

    // This is called to add a point to the line
    public void AddPoint()
    {
        Vector3 pt = _poi.transform.position;
        // If the point isn't far enough from the last point
        if (points.Count > 0 && (pt - lastPoint).magnitude < minDist)
        {
            return;
        }

        // If this is the launch point
        if (points.Count == 0)
        {
            Vector3 launchPosDiff = pt - Slingshot.LAUNCH_POS;
			// ... it adds an extra bit of line to aid aiming later
            points.Add(pt + launchPosDiff);
            points.Add(pt);
            line.positionCount = 2;

            // Set the first two points
            line.SetPosition(0, points[0]);
            line.SetPosition(1, points[1]);

            // Enables the LineRenderer
            line.enabled = true;
        }

        // Normal behavior of adding a point
        else
        {
            points.Add(pt);
            line.positionCount = points.Count;
            line.SetPosition(points.Count - 1, lastPoint);
            line.enabled = true;
        }
    }

    // Returns the location of the most recently added point
    public Vector3 lastPoint
    {
        get
        {
            // If there are no points, returns Vector3.zero
            if (points == null) { return Vector3.zero; }
            return points[points.Count - 1];
        }
    }
}

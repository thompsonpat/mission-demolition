using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{

    // A static field accessbile by code anywhere
    public static bool goalMet = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

	// When the trigger is hit by something
    void OnTriggerEnter(Collider other)
    {
		// Check to see if it's a projectile
		if (other.gameObject.tag == "Projectile")
		{
			Goal.goalMet = true;

			// set the aphla fo the color to higher opacity
			Material mat = GetComponent<Renderer>().material;
			Color c = mat.color;
			c.a = 1;
			mat.color = c;
		}
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceBreaker : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision coll)
    {
        int breakVelocity = 15;

        // Find out what hit this basket
        GameObject collidedWith = coll.gameObject;

        if (collidedWith.tag == "Weak")
        {
			// Debug.Log(this.GetComponent<Rigidbody>().velocity.x);
            if (this.GetComponent<Rigidbody>().velocity.x > breakVelocity)
            {
                Destroy(collidedWith);
            }
        }
    }
}
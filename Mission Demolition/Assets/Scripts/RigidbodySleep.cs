using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodySleep : MonoBehaviour
{
    // [Header("Set in Inspector")]
	// public int massChange = 200;

    // Use this for initialization
    void Start()
    {
        int massChange = 50;
        Rigidbody rb = GetComponent<Rigidbody>();

        Material weakMaterial = Resources.Load("Mat_Glass", typeof(Material)) as Material;
        Material strongMaterial = Resources.Load("Mat_Metal", typeof(Material)) as Material;

        if (this.tag == "Weak") {
			GetComponent<Renderer>().material = weakMaterial;
			rb.mass -= massChange;
            if (rb.mass < 1) rb.mass = 1;
		}
		if (this.tag == "Strong") {
			GetComponent<Renderer>().material = strongMaterial;
			rb.mass += massChange;
		}

        if (rb != null) rb.Sleep();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
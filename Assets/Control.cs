using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetButtonDown("joystick button 1"))
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 1f);
	}
}

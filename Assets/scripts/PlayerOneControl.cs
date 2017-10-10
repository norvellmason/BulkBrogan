using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneControl : MonoBehaviour {

    public ActionController actionController;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
            actionController.MoveRight();

        if(Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            actionController.MoveLeft();

        if(Input.GetKeyDown(KeyCode.Space))
            actionController.Jump();
    }
}

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
        float horizontalAxis = Input.GetAxis("P2 Horizontal");

        if(Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) || horizontalAxis > 0.5)
            actionController.MoveRight();

        if(Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) || horizontalAxis < -.5)
            actionController.MoveLeft();

        if(Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("P2 Jump"))
            actionController.Jump();

        if(Input.GetKeyDown(KeyCode.A) || Input.GetButtonDown("P2 Punch"))
            actionController.Punch();

        if(Input.GetKeyDown(KeyCode.S) || Input.GetButtonDown("P2 Kick"))
            actionController.Kick();

        if(transform.position.y < -5)
        {
            transform.position = new Vector3((float)((Random.value - 0.5) * 8), 5, 0);
        }
    }
}

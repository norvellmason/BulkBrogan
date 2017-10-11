using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThreeController : MonoBehaviour {

    public ActionController actionController;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        float horizontalAxis = Input.GetAxis("P3 Horizontal");

        if(horizontalAxis > 0.5)
            actionController.MoveRight();

        if(horizontalAxis < -.5)
            actionController.MoveLeft();

        if(actionController.OnGround())
            actionController.ApplyHorizontalFriction();

        if(Input.GetButtonDown("P3 Jump"))
            actionController.Jump();

        if(Input.GetButtonDown("P3 Punch"))
            actionController.Punch();

        if(Input.GetButtonDown("P3 Kick"))
            actionController.Kick();

        if(Input.GetButtonDown("P3 BodySlam"))
            actionController.BodySlam();
    }
}

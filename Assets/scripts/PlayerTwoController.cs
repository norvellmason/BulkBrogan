using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoController : MonoBehaviour {

    public ActionController actionController;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalAxis = Input.GetAxis("P1 Horizontal");

        if(horizontalAxis > 0.5)
            actionController.MoveRight();
    
        if(horizontalAxis < -.5)
            actionController.MoveLeft();

        if(actionController.OnGround())
            actionController.ApplyHorizontalFriction();

        if(Input.GetButtonDown("P1 Jump"))
            actionController.Jump();

        if(Input.GetButtonDown("P1 Punch"))
            actionController.Punch();

        if(Input.GetButtonDown("P1 Kick"))
            actionController.Kick();

        if(Input.GetButtonDown("P1 BodySlam"))
            actionController.BodySlam();
    }
}

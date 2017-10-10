using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float movementSpeed = 5.0f;
    public float jumpStrength  = 10f;

    private new Rigidbody2D rigidbody;

    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
            rigidbody.velocity = new Vector2(movementSpeed, rigidbody.velocity.y);
        else if(Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            rigidbody.velocity = new Vector2(-movementSpeed, rigidbody.velocity.y);
        else
            rigidbody.velocity = new Vector2(rigidbody.velocity.x * 0.8f, rigidbody.velocity.y);

        if(Input.GetKeyDown(KeyCode.UpArrow))
            rigidbody.AddForce(new Vector2(0, jumpStrength));


    }
}

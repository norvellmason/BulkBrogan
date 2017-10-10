using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour {

    public float movementSpeed;
    public float jumpStrength;

    private new Rigidbody2D rigidbody;
    private new BoxCollider2D collider;

    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider  = GetComponent<BoxCollider2D>();
    }

    public void MoveRight()
    {
        if(rigidbody != null)
        {
            rigidbody.AddForce(new Vector2(movementSpeed, 0));
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void MoveLeft()
    {
        if(rigidbody != null)
        {
            rigidbody.AddForce(new Vector2(-movementSpeed, 0));
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void Jump()
    {
        if(rigidbody != null)
        {
            if(OnGround())
                rigidbody.AddForce(new Vector2(0, jumpStrength));
        }
    }

    private bool OnGround()
    {
        if(rigidbody != null && collider != null)
        {
            //return Physics2D.BoxCast(collider.transform.position - new Vector3(0, collider.size.y / 2 + 0.1f, 0), new Vector2(collider.size.x, 0), 0, new Vector2(0, -1), 0.5f);
            return Physics2D.Raycast(collider.transform.position - new Vector3(0, collider.size.y / 2 + 0.1f, 0), new Vector2(0, -1), 0f);
        }

        return false;
    }

}

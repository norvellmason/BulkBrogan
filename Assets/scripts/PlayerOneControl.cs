using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneControl : MonoBehaviour {

    public float speed = 6;
    public float jumpStrength;
    [Range(0, 1)]public float friction;

    public float punchStrength = 10f;
    public float kickStrength = 7f;

    public float hitmultiplier = 1.0f;

    private Rigidbody2D rigidbody;
    private bool facingRight = true;

	// Use this for initialization
	void Start ()
    {
        rigidbody = GetComponent<Rigidbody2D>();
	}

    public void Hit(Vector2 direction)
    {
        rigidbody.AddForce(direction * hitmultiplier);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(transform.position.y < -7f)
            rigidbody.position = new Vector2((float)((Random.value - 0.5) * 4), 3);



        if(Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
            // rigidbody.velocity = new Vector2(movemenSpeed, rigidbody.velocity.y);
            if(rigidbody.velocity.x < 0)
                rigidbody.AddForce(new Vector2(3 * speed, 0));
            else
                rigidbody.AddForce(new Vector2(speed, 0));

            if(!facingRight)
                Flip();
        }
        else if(Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            // rigidbody.velocity = new Vector2(-movemenSpeed, rigidbody.velocity.y);
            if(rigidbody.velocity.x > 0)
                rigidbody.AddForce(new Vector2(-3 * speed, 0));
            else
                rigidbody.AddForce(new Vector2(-speed, 0));

            if(facingRight)
                Flip();
        }

        rigidbody.velocity = new Vector2(rigidbody.velocity.x * friction, rigidbody.velocity.y);

        Debug.DrawRay(rigidbody.position + new Vector2(transform.localScale.x * 0.3f, 0), new Vector2(transform.localScale.x, 0));

        if(Input.GetKeyDown(KeyCode.Space) && OnGround())
            Jump();

        if(Input.GetKeyDown(KeyCode.A) && OnGround())
            Hit(punchStrength, 4);

        if(Input.GetKeyDown(KeyCode.S) && OnGround())
            Hit(kickStrength, 7);
    }

    private void Jump()
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpStrength);
    }
    private void Hit(float strength, float upwards)
    {
        RaycastHit2D hit = Physics2D.Raycast(rigidbody.position + new Vector2(transform.localScale.x * 0.3f, 0), new Vector2(transform.localScale.x, 0), 0.5f);

        if(hit)
            hit.collider.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x * strength, upwards);
    }

    private bool OnGround()
    {
        return Physics2D.Raycast(transform.position - new Vector3(0, 0.7f, 0), -transform.up, 0.1f);
    }

    private void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
        facingRight = !facingRight;
    }
}

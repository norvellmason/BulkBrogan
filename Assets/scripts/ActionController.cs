using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour {

    public float movementSpeed;
    public float airSpeed;
    public float maxSpeed;
    public float jumpStrength;
    public float friction;

    public Sprite idleSprite;
    public Sprite punchSprite;
    public Sprite kickSprite;

    public AudioClip jumpClip;
    public AudioClip hitClip;
    public AudioClip punchClip;
    public AudioClip kickClip;
    public AudioClip deathClip;
    public AudioClip ohnoClip;
    public AudioClip bodySlamClip;

    private new Rigidbody2D rigidbody;
    private new BoxCollider2D collider;
    private new SpriteRenderer renderer;
    private new AudioSource audio;

    private int punchTimer = 0;
    private int kickTimer = 0;
    private bool bodySlam = false;
    
    public int Life { get; private set; }
    public float HitMultiplier { get; private set; }

    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider  = GetComponent<BoxCollider2D>();
        renderer  = GetComponent<SpriteRenderer>();
        audio     = GetComponent<AudioSource>();

        Life = 3;
        HitMultiplier = 2;
    }

    void FixedUpdate()
    {
        if(renderer != null)
        {
            if(punchTimer > 0)
                punchTimer -= 1;

            if(kickTimer > 0)
                kickTimer -= 1;

            if(punchTimer == 0 && kickTimer == 0)
                renderer.sprite = idleSprite;
            else if(punchTimer > 0)
                renderer.sprite = punchSprite;
            else if(kickTimer > 0)
                renderer.sprite = kickSprite;
        }

        Debug.Log(HitMultiplier);

        if(rigidbody != null)
        {
            if(transform.position.y < -5 || transform.position.y > 80 || transform.position.x > 35 || transform.position.x < -55)
            {
                Life -= 1;

                if(Life < 0)
                {
                    Destroy(gameObject);
                }
                else
                {
                    transform.position = new Vector3((float)((Random.value - 0.5) * 8), 5, 0);
                    rigidbody.velocity = new Vector2(Random.value - 0.5f, 0);
                    HitMultiplier = 2;

                    PlaySound(deathClip);
                }

            }
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if(audio != null)
        {
            audio.PlayOneShot(clip);
        }
    }

    public void MoveRight()
    {
        if(rigidbody != null)
        {
            if(rigidbody.velocity.x < maxSpeed)
            {
                if(OnGround())
                {
                    if(rigidbody.velocity.x < 0)
                        rigidbody.AddForce(new Vector2(movementSpeed * 3, 0));
                    else
                        rigidbody.AddForce(new Vector2(movementSpeed, 0));
                }
                else
                {
                    if(rigidbody.velocity.x < 0)
                        rigidbody.AddForce(new Vector2(airSpeed * 3, 0));
                    else
                        rigidbody.AddForce(new Vector2(airSpeed, 0));
                }

            }

            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void MoveLeft()
    {
        if(rigidbody != null)
        {
            if(rigidbody.velocity.x > -maxSpeed)
            {
                if(OnGround())
                {
                    if(rigidbody.velocity.x > 0)
                        rigidbody.AddForce(new Vector2(movementSpeed * -3, 0));
                    else
                        rigidbody.AddForce(new Vector2(-movementSpeed, 0));
                }
                else
                {
                    if(rigidbody.velocity.x > 0)
                        rigidbody.AddForce(new Vector2(airSpeed * -3, 0));
                    else
                        rigidbody.AddForce(new Vector2(-airSpeed, 0));
                }
                
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }

    public void ApplyHorizontalFriction()
    {
        if(rigidbody != null)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x * friction, rigidbody.velocity.y);
        }
    }

    public void Jump()
    {
        if(rigidbody != null)
        {
            if(OnGround())
            {
                rigidbody.AddForce(new Vector2(0, jumpStrength));
                PlaySound(jumpClip);
            }
        }
    }

    public void Punch()
    {
        if(punchTimer == 0 && kickTimer == 0 && ! bodySlam)
        {
            punchTimer = 10;
            PlaySound(punchClip);

            Hit(200, 75);
        }
    }

    public void Kick()
    {
        if(punchTimer == 0 && kickTimer == 0 && !bodySlam)
        {
            kickTimer = 15;
            PlaySound(kickClip);

            Hit(300, 120);
        }
    }

    public void BodySlam()
    {
        if(punchTimer == 0 && kickTimer == 0 && !bodySlam)
        {
            //bodySlam = true;
            PlaySound(bodySlamClip);

            AddForce(new Vector2(0, -900));
        }
    }

    public void AddForce(Vector2 force)
    {
        if(rigidbody != null)
        {
            rigidbody.AddForce(force);
        }
    }

    public void TakeHit(Vector2 force)
    {
        AddForce(force * HitMultiplier);
        HitMultiplier *= 1.4f;
        PlaySound(hitClip);
    }

    private void Hit(float knockback, float knockup)
    {
        if(collider != null)
        {
            RaycastHit2D hit = Physics2D.Raycast(collider.transform.position + new Vector3((collider.size.x / 2 + 0.1f) * transform.localScale.x, 0, 0), new Vector2(transform.localScale.x, 0), 0.5f);

            if(hit)
            {
                /*Rigidbody2D hitBody = hit.collider.GetComponent<Rigidbody2D>();

                if(hitBody != null)
                {
                    hitBody.AddForce(new Vector2(knockback * transform.localScale.x, knockup));
                }*/
                
                ActionController hitController = hit.collider.GetComponent<ActionController>();

                if(hitController != null)
                    hitController.TakeHit(new Vector2(knockback * transform.localScale.x, knockup));
            }
        }
    }

    public bool OnGround()
    {
        if(rigidbody != null && collider != null)
        {
            //return Physics2D.BoxCast(collider.transform.position - new Vector3(0, collider.size.y / 2 + 0.1f, 0), new Vector2(collider.size.x, 0), 0, new Vector2(0, -1), 0.5f);
            return Physics2D.Raycast(collider.transform.position - new Vector3(0, collider.size.y / 2 + 0.1f, 0), new Vector2(0, -1), 0f);
        }

        return false;
    }

}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed = 7f;
    public float jumpSpeed = 15f;
    public Collider2D bottomCollider;
    public CompositeCollider2D terrainCollider;

    float vx = 0f;
    float preVx = 0f;

    float lastShoot;

    bool isGrounded;

    Vector2 originPosition;


    void Start()
    {
        originPosition = transform.position;
    }


    void Update()
    {
        PlayerMove();
        Fire();
    }


    void FixedUpdate()
    {
        transform.Translate(Vector2.right * vx * speed * Time.fixedDeltaTime);
    }


    public void Restart()
    {
        transform.eulerAngles = Vector3.zero;
        transform.position = originPosition;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        GetComponent<Rigidbody2D>().angularVelocity = 0;
        GetComponent<BoxCollider2D>().enabled = true;
    }


    void PlayerMove()
    {
        vx = Input.GetAxisRaw("Horizontal");

        if (vx < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }

        if (vx > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpSpeed;
        }

        //땅과의 접촉 여부
        if (bottomCollider.IsTouching(terrainCollider))
        {
            if (!isGrounded)
            {
                //착지
                if (vx == 0)
                {
                    GetComponent<Animator>().SetTrigger("Idle");
                }
                else
                {
                    GetComponent<Animator>().SetTrigger("Run");
                }
            }
            else
            {
                //걷는 중
                if (preVx != vx)
                {
                    if (vx == 0)
                    {
                        GetComponent<Animator>().SetTrigger("Idle");
                    }
                    else
                    {
                        GetComponent<Animator>().SetTrigger("Run");
                    }
                }
            }

            isGrounded = true;
        }
        else
        {
            if (isGrounded)
            {
                //점프 시작
                GetComponent<Animator>().SetTrigger("Jump");
            }

            isGrounded = false;
        }

        preVx = vx;
    }


    void Fire()
    {
        if (Input.GetButtonDown("Fire1") && lastShoot + 0.3f < Time.time)
        {
            Vector2 bulletV = Vector2.zero;

            if (GetComponent<SpriteRenderer>().flipX)
            {
                bulletV = new Vector2(-10, 0);
            }
            else
            {
                bulletV = new Vector2(10, 0);
            }

            GameObject bullet = ObjectPool.Instance.GetBullet();
            bullet.transform.position = transform.position;
            bullet.GetComponent<Bullet>().velocity = bulletV;
            lastShoot = Time.time;
        }
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            GetComponent<Rigidbody2D>().angularVelocity = 720;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
            GetComponent<BoxCollider2D>().enabled = false;

            GameManager.instance.Die();
        }
    }
}

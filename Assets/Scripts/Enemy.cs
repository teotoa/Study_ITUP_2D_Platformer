using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int hp = 3;
    public float speed = 3;
    public Collider2D frontBottomCollider;
    public Collider2D frontCollider;

    public CompositeCollider2D terrainCollider;

    Vector2 vx;


    void Start()
    {
        vx = Vector2.right * speed;
    }


    void Update()
    {
        if (frontCollider.IsTouching(terrainCollider) || !frontBottomCollider.IsTouching(terrainCollider))
        {
            vx = -vx;
            transform.localScale = new Vector2(-transform.localScale.x, 1);
        }
    }


    void FixedUpdate()
    {
        transform.Translate(vx * Time.fixedDeltaTime);
    }



    public void Hit(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            GetComponent<Rigidbody2D>().angularVelocity = 720;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(3, 10), ForceMode2D.Impulse);
            GetComponent<Collider2D>().enabled = false;

            Invoke("DestroyThis", 2.0f);
        }
    }


    void DestroyThis()
    {
        Destroy(gameObject);
    }
}

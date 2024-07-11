using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Vector2 velocity = new Vector2(10, 0);

    void Start()
    {

    }


    void FixedUpdate()
    {
        transform.Translate(velocity * Time.fixedDeltaTime);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Terrain")
        {
            gameObject.SetActive(false);
        }

        else if (other.tag == "Enemy")
        {
            other.GetComponent<Enemy>().Hit(1);

            gameObject.SetActive(false);
        }
    }
}

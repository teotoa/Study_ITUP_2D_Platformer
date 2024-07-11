using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public float timeAdd = 5f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.instance.Addtime(timeAdd);
            GetComponent<Animator>().SetTrigger("Eaten");
            Invoke("DestroyThis", 0.3f);
        }
    }


    void DestroyThis()
    {
        Destroy(gameObject);
    }
}

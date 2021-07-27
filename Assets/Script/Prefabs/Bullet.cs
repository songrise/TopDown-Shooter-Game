using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject explosion;
    Rigidbody rb;
    public float maxSpeed = 10f;
    public float currentSpeed = 0f;
    public Vector3 startPosition;
    public Vector3 direction;

    private int maxLifetime = 3;




    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //set gravity to avoid it from falling down too fast
        // rb.gravity = 0.1f;
        StartCoroutine(DestroyAfter(maxLifetime));
    }

    void OnCollisionEnter(Collision collision)
    {
        // if (collision.gameObject.tag == "Player")
        //     return;
        if (collision.gameObject.tag == "Enemy")
        {
            //deal  damage
        }
        Debug.Log("Bullet hit " + collision.gameObject.name);
        Instantiate(explosion, transform.position, transform.rotation);
        //destroy itself
        Destroy(gameObject);
    }

    IEnumerator DestroyAfter(float seconds)
    {
        for (int i = 0; i < this.maxLifetime; i++)
        {
            yield return new WaitForSeconds(1);
        }
        // if already destroyed
        if (gameObject != null)
            Destroy(gameObject);
    }
}

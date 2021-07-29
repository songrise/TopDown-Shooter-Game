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

    private int maxLifetime = 1;




    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //set gravity to avoid it from falling down too fast
        // rb.gravity = 0.1f;
        StartCoroutine(AutoRecycle());
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            return;
        if (collision.gameObject.tag == "Enemy")
        {
            //deal  damage
            collision.gameObject.GetComponent<SimpleEnemy>().OnHit();
        }
        Debug.Log("Bullet hit " + collision.gameObject.name);
        Instantiate(explosion, transform.position, transform.rotation);
        //destroy itself
        Destroy(gameObject);
    }


    IEnumerator AutoRecycle()
    {
        yield return new WaitForSeconds(1.5f);
        if (gameObject != null)
            ObjectPool.GetInstance().RecycleObj(gameObject);
    }
}

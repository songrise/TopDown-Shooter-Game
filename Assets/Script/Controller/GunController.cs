using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{

    public Rigidbody bullet;

    // 射速速率 一秒钟能打几发
    public float fireRate = 5;

    // 上一次开火的时间
    protected float lastFireTime;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        OnFire();
    }
    private void OnFire()
    {
        if (isAllowShooting() && Input.GetButton("Fire1"))
        {
            lastFireTime = Time.time;
            Vector3 firePos = transform.forward;
            //normalize the mouse position
            // mousePos.Normalize();
            Debug.Log("Firing at direction: " + firePos);

            Rigidbody b = Instantiate(bullet, transform.position + transform.forward * 2, Quaternion.identity);
            b.velocity = firePos * 50f;
            Debug.Log("Firing");
        }

    }
    protected bool isAllowShooting()
    {
        return (Time.time - lastFireTime > 1 / fireRate);
    }

}

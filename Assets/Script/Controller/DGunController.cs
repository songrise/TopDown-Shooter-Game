// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class GunController : MonoBehaviour
// {

//     public Rigidbody bullet;


//     // 射速速率 一秒钟能打几发
//     public float fireRate = 5;
//     public float bulletSpeed = 70f;
//     public GameObject firingFlame;

//     public float recoilIntensity = 0.1f;
//     private AudioSource audioSource;


//     // 上一次开火的时间
//     protected float lastFireTime;
//     // Start is called before the first frame update
//     void Start()
//     {
//         audioSource = GetComponent<AudioSource>();
//         //disable the flame
//         firingFlame.SetActive(false);
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         OnFire();
//     }

//     public void OnFire()
//     {
//         if (isAllowShooting() && Input.GetButton("Fire1"))
//         {
//             audioSource.Play();
//             firingFlame.SetActive(true);
//             lastFireTime = Time.time;
//             Vector3 firePos = transform.forward;
//             //normalize the mouse position
//             // mousePos.Normalize();
//             //add random direction
//             firePos.x += Random.insideUnitSphere.x * recoilIntensity;
//             firePos.z += Random.insideUnitSphere.z * recoilIntensity;

//             Rigidbody b = Instantiate(bullet, transform.position + transform.forward * 2f, Quaternion.identity);
//             b.velocity = firePos * bulletSpeed;
//             Debug.Log("Firing");
//         }
//         else
//         {
//             if (!Input.GetButton("Fire1"))
//             {
//                 //disable the flame
//                 firingFlame.SetActive(false);
//             }
//         }
//     }
//     protected bool isAllowShooting()
//     {
//         return (Time.time - lastFireTime > 1 / fireRate);
//     }

// }

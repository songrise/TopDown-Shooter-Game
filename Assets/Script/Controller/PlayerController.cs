using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float walkSpeed = 5f;
    // public Boundary boundary; // optional rectangle boundary

    float maxSpeed = 10f;
    float curSpeed;

    float sprintSpeed;


    private Rigidbody playerRb;

    private Vector3 cameraOffSet = new Vector3(0, 20, 20);
    private Vector3 cameraOrientation = new Vector3(45, 180, 0);



    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        // sprintSpeed = walkSpeed + (walkSpeed / 2);
    }

    void Update()
    {

        //camera follow the player
        Camera.main.transform.position = transform.position + cameraOffSet;
        Camera.main.transform.rotation = Quaternion.Euler(cameraOrientation);
    }

    void FixedUpdate()
    {
        curSpeed = walkSpeed;
        maxSpeed = curSpeed;

        // the movement magic
        //player movement


        playerRb.velocity = new Vector3(
            Mathf.Lerp(0, -Input.GetAxis("Horizontal") * curSpeed, 0.8f),
                playerRb.velocity.y,
                Mathf.Lerp(0, -Input.GetAxis("Vertical") * curSpeed, 0.8f)
            );

        // // optional: clamp position inside a rectangle
        // rb.position = new Vector3(
        //     Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
        //     rb.position.y,
        //     Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        // );

        lookAtCursor();
    }

    protected void lookAtCursor()
    {
        float rotateSpeed = 10f;
        // Generate a plane that intersects the transform's position with an upwards normal.
        Plane playerPlane = new Plane(Vector3.up, transform.position);

        // Generate a ray from the cursor position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Determine the point where the cursor ray intersects the plane.
        // This will be the point that the object must look towards to be looking at the mouse.
        // Raycasting to a Plane object only gives us a distance, so we'll have to take the distance,
        //   then find the point along that ray that meets that distance.  This will be the point
        //   to look at.
        float hitdist = 0.0f;
        // If the ray is parallel to the plane, Raycast will return false.
        if (playerPlane.Raycast(ray, out hitdist))
        {
            // Get the point along the ray that hits the calculated distance.
            Vector3 targetPoint = ray.GetPoint(hitdist);

            // Determine the target rotation.  This is the rotation if the transform looks at the target point.
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

            // Smoothly rotate towards the target point.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        }
    }


    // IEnumerator FireAtDirection(float fireRate)
    // {
    //     //fire at specified direction
    //     while (Input.GetButton("Fire1"))
    //     {

    //         Vector3 firePos = transform.forward;
    //         //normalize the mouse position
    //         // mousePos.Normalize();
    //         Debug.Log("Firing at direction: " + firePos);

    //         Rigidbody b = Instantiate(bullet, transform.position + transform.forward * 2, Quaternion.identity);
    //         b.velocity = firePos * 40f;
    //         Debug.Log("Firing");
    //         yield return new WaitForSeconds(fireRate);
    //     }
    // }


}

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

    float sprintSpeed = 20f;
    // private bool isMoving;
    private bool isDashing = false;
    private float angularSpeed = 15f;


    private Rigidbody playerRb;





    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        // sprintSpeed = walkSpeed + (walkSpeed / 2);
    }

    void Update()
    {
    }

    void FixedUpdate()
    {

        Vector3 targetPoint = getTargetPos();
        OnDash();
        OnMove();


        lookAtCursor(targetPoint);
    }

    private void OnMove()
    {
        curSpeed = walkSpeed;
        maxSpeed = curSpeed;
        //todo use transform implement movement


        // playerRb.velocity = new Vector3(
        //     Mathf.Lerp(0, -Input.GetAxis("Horizontal") * curSpeed, 0.8f),
        //         0,
        //         Mathf.Lerp(0, -Input.GetAxis("Vertical") * curSpeed, 0.8f)
        //     );
        if (!isDashing)
        {
            transform.position = transform.position - new Vector3(
            Mathf.Lerp(0, Input.GetAxis("Horizontal"), 0.2f),
           0,
           Mathf.Lerp(0, Input.GetAxis("Vertical"), 0.2f)
       );

        }

        // // optional: clamp position inside a rectangle
        // rb.position = new Vector3(
        //     Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
        //     rb.position.y,
        //     Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        // );


    }

    private Vector3 getTargetPos()
    {
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
            return targetPoint;
        }
        else
        {
            return Vector3.zero;
        }
    }

    protected void lookAtCursor(Vector3 targetPoint)
    {

        // Generate a plane that intersects the transform's position with an upwards normal.

        // Determine the target rotation.  This is the rotation if the transform looks at the target point.
        Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

        // Smoothly rotate towards the target point.
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, angularSpeed * Time.deltaTime);

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
    //todo reformat
    float dashDuration = 0.2f;// 控制冲
    float dashSpeed = 500;// 冲刺速度
    //todo 穿墙
    float dashTime = 0.2f;// 剩余冲刺时间
    Vector3 directionXOZ = Vector3.zero;
    private void OnDash()
    {
        if (!isDashing)
        {
            //if pressed space bar
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isDashing = true;
                //set angular speed higher for better expereince
                angularSpeed = 60f;
                //get the direction from wasd
                directionXOZ = new Vector3(
                    -Mathf.Lerp(0, Input.GetAxis("Horizontal"), 1f),
                    0,
                    -Mathf.Lerp(0, Input.GetAxis("Vertical"), 1f)
                    );
                // if no direction, by default, set the direction to forward
                if (directionXOZ == Vector3.zero)
                {
                    directionXOZ = transform.forward;
                }
                StartCoroutine(SlowDownInDash(dashDuration));
                Debug.Log("Dashing");
                directionXOZ.y = 0f;
            }
        }
        else
        {//else dashing
            if (dashTime <= 0)
            {
                angularSpeed = 15f;
                //end of a dash state
                TimeControl.GetInstance().revertGameSpeed();
                isDashing = false;

                dashTime = dashDuration;
                //set velocity to zero
                playerRb.velocity = Vector3.zero;
            }
            else
            {
                dashTime -= Time.deltaTime;
                //if started dash for serveral time, slowdown game

                playerRb.velocity = directionXOZ * dashTime * dashSpeed;// rigidbody = GetComponent<Rigidbody>(); 加在 Start() 函数中
            }
        }
        //remove y axis
        // Vector3 cursorPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        // //convert location to xzy, scale z direction by sqrt(2) because 45 degree
        // cursorPos.z = cursorPos.y * 1.414f;
        // cursorPos.y = 0;
        // //convert to local position


        // Time.timeScale = 0.3f;
        // //dash toward the mouse
        // playerRb.velocity = Vector3.forward * 10f;
    }
    // public GameObject pp;
    IEnumerator SlowDownInDash(float dashDuration)
    {
        //find post processing volume
        // pp.GetComponent<PostProcessingVolume>().volume = 0.5f;
        for (int i = 1; i < 8; i += 3)
        {
            //set chromatic Aberation
            // pp.GetComponent<PostProcessingVolume>().SetChromaticAberration(0.3f);
            //set fov
            Camera.main.fieldOfView = 70f;
            //slow down the game in different phase
            TimeControl.GetInstance().slowDownGame(i);
            yield return new WaitForSeconds(dashDuration / 3f);
        }
    }


    //todo refactor
    // void Awake()
    // {
    //     Debug.Log(Time.fixedDeltaTime);
    // }


}

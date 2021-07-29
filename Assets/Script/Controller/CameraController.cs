using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{


    public GameObject target; // the object to follow
    // [Info("Controll the camera shift according to cursor location on screen")]
    public float cameraShiftScale = 7;

    private Vector3 cameraOffSet = new Vector3(0, 20, 20);
    private Vector3 cameraOrientation = new Vector3(45, 180, 0);


    void Start()
    {

    }
    void Update()
    {
        setCamera();
    }
    private void setCamera()
    {
        //adjust the camera according to cursor

        Vector3 cursorPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        //convert location to xzy, scale z direction by sqrt(2) because 45 degree
        cursorPos.z = cursorPos.y > 0 ? cursorPos.y * 1.2f : cursorPos.y * 1.414f;
        cursorPos.y = 0;
        //camera follow the player
        Camera.main.transform.position = target.transform.position + cameraOffSet - cursorPos * cameraShiftScale;
        Camera.main.transform.rotation = Quaternion.Euler(cameraOrientation);
    }
    public void ShakeCamera()
    {
        Camera.main.transform.position = target.transform.position + cameraOffSet + new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1));
    }
    
}
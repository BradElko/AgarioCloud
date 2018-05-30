using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCameraController : MonoBehaviour {
    //public
    public GameObject player;
    public float rotationSpeed;

    //v3
    private Vector3 mousePos;
    private Vector3 playerPos;
    private Vector3 camPos;

    //float

    //Mouse -> Player
    private float sideX;
    private float sideZ;
    private float sideC;
    private float sideC2;

    //Player -> Camera
    private float camX;
    private float camZ;
    private float camC;

    //Mouse -> Player -> Camera
    private float rotX;
    private float rotZ;
    private float rotC;

    //Mouse -> Camera
    private float safeX;
    private float safeZ;
    private float safeC;

    //Angles && Rotations
    private float rotAngle;
    private float safeAngle;
    private Quaternion currRotation;
    private Quaternion safeRotation;
    private Quaternion safeZone;

    //other
    private float getAngle;
    private Quaternion mouseRot;
    private Quaternion startingLine;

    //camera
    private float updown;
    private Camera cam;

    void Start ()
    {
        //startingLine = Quaternion.Euler(transform.rotation * Vector3.up);
        //transform.rotation = Quaternion.Euler(30, 0, 0);
        transform.position = new Vector3(0, ((player.transform.localScale.y / 2) + 1), (player.transform.localScale.z * -4));
        updown = ((player.transform.localScale.y / 2) + 1) - 0.5f;
        //Debug.Log(updown);

        cam = this.GetComponent<Camera>();
    }
    void Update()
    {
        Ray p = cam.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 15));
        mousePos = (p.direction * 15);
        //Debug.Log(mousePos);
    }
    void LateUpdate ()
    {
       // mousePos = HitCameraController.mousePos;
        playerPos = player.transform.position;

        sideX = (mousePos.x - playerPos.x);
        sideZ = (mousePos.z - playerPos.z);
        sideC = (Mathf.Sqrt(Mathf.Pow(sideX, 2) + Mathf.Pow(sideZ, 2)));

        sideC2 = sideC - 5;
        // substract the safe-zone-radius
        if (sideC2 < 0f)
        {
            // Inside safe-zone, do nothing.
        }
        else
        {
            // Outside safe-zone, correct values to avoid jumping.
            float x_ratio = sideX / sideZ; // be careful with "divide by zero"
            float z_ratio = sideZ / sideX;
            sideX -= 5 * x_ratio;
            sideZ -= 5 * z_ratio;
        }
        /*mouseRot = Quaternion.Euler(0, sideC, 0);
        getAngle = Quaternion.Angle(startingLine, mouseRot);
        //Debug.Log(getAngle);*/

        camC = 5;
        camX = (camC * sideX) / sideC;
        camZ = (camC * sideZ) / sideC;
        camPos = new Vector3(
            (playerPos.x - camX), 
            (player.transform.localScale.y + 1), 
            (playerPos.z - camZ));

        rotX = (sideX + camX);
        rotZ = (sideZ + camZ);
        rotC = (Mathf.Sqrt(Mathf.Pow(rotX, 2) + Mathf.Pow(rotZ, 2)));

        safeX = (mousePos.x - camPos.x);
        safeZ = (mousePos.z - camPos.z);
        safeC = (Mathf.Sqrt(Mathf.Pow(safeX, 2) + Mathf.Pow(safeZ, 2)));

        //++
        if (rotX >= 0 && rotZ >= 0)
        {
            rotAngle = (Mathf.Asin(rotX / rotC) * Mathf.Rad2Deg);
        }
        if (safeX >= 0 && safeZ >= 0)
        {
            safeAngle = (Mathf.Asin(safeX / safeC) * Mathf.Rad2Deg);
        }
        //+-
        if (rotX >= 0 && rotZ <= 0)
        {
            rotAngle = 180 - (Mathf.Asin(rotX / rotC) * Mathf.Rad2Deg);
        }
        if (safeX >= 0 && safeZ <= 0)
        {
            safeAngle = 180 - (Mathf.Asin(safeX / safeC) * Mathf.Rad2Deg);
        }
        //-+
        if (rotX <= 0 && rotZ >= 0)
        {
            rotAngle = (Mathf.Asin(rotX / rotC) * Mathf.Rad2Deg);
        }
        if (safeX <= 0 && safeZ >= 0)
        {
            safeAngle = (Mathf.Asin(safeX / safeC) * Mathf.Rad2Deg);
        }
        //--
        if (rotX <= 0 && rotZ <= 0)
        {
            rotAngle = -180 - (Mathf.Asin(rotX / rotC) * Mathf.Rad2Deg);
        }
        if (safeX <= 0 && safeZ <= 0)
        {
            safeAngle = -180 - (Mathf.Asin(safeX / safeC) * Mathf.Rad2Deg);
        }
        // Move Camera
        //transform.position = camPos;

        // Rotate Camera
        currRotation = Quaternion.Euler(0, rotAngle, 0);
       // transform.rotation = currRotation;



        /*transform.rotation = Quaternion.RotateTowards(
            transform.rotation, currRotation, 
            (rotationSpeed * Time.deltaTime));*/
	}
}

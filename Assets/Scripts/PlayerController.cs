using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    //public
    public GameObject player;
    public Rigidbody rb;
    public float speed;
    public float maxSpeed;

    //v3
    private Vector3 movePlayer;

    //float
    private float startVol;
    private float verticalForce;
    private float horizontalForce;

    void Start ()
    {
        startVol = 5000;
        verticalForce = 0;
        horizontalForce = 0;

        player.transform.localScale = new Vector3(
            Mathf.Pow(startVol, 1f / 3f),
            Mathf.Pow(startVol, 1f / 3f),
            Mathf.Pow(startVol, 1f / 3f));

        player.transform.position = new Vector3(
            0.0f, ((player.transform.localScale.y / 2) + 1), 0.0f);

        rb.mass = startVol * 200;
    }
    void Update()
    {
        horizontalForce = Input.GetAxis("Horizontal");
        verticalForce = Input.GetAxis("Vertical");

        if(Input.anyKey == false)
        {
            verticalForce = 0;
            horizontalForce = 0;
        }
        movePlayer = new Vector3(horizontalForce, 0, verticalForce);
    }
	void FixedUpdate ()
    {
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        rb.AddForce(movePlayer * speed);
        rb.AddForce(0, (-Physics.gravity.y * rb.mass), 0);

        Debug.Log(rb.velocity);
    }
}

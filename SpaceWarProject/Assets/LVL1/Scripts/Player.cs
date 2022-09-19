using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float offset;

    private Rigidbody rb;

    private Vector3 moveInput;

    private Vector3 moveVelocity;
    private Camera _camera;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _camera = Camera.main;
    }


    void Update()
    {
        moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        moveVelocity = moveInput.normalized * speed;

        Vector3 screenMousePosition = Input.mousePosition;
        Debug.Log(Mathf.Atan2(screenMousePosition.y, screenMousePosition.x));
        transform.rotation = Quaternion.Euler(0f, Mathf.Atan2(screenMousePosition.y, screenMousePosition.x) * 100 * -1, 0f);
        //Debug.Log(screenMousePosition.x + " " + screenMousePosition.y + " " + screenMousePosition.z);
        //Vector3 worldMousePosition = _camera.ScreenToWorldPoint(screenMousePosition);
        //Debug.Log(worldMousePosition.x + " " + worldMousePosition.y + " " + worldMousePosition.z);

        //transform.rotation = new Vector3(0f,0f,0f);

        //
        // Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        // Debug.Log(difference.x+" "+difference.y + " "+difference.z);
        // float rotz = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        // transform.rotation = Quaternion.Euler(0f, rotz + offset, 0f);
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }
}

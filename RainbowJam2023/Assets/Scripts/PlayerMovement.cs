using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour {
    public float mainSpeed = 10.0f;
    private float totalRun= 1.0f;
    public Collider2D coll;
    public Rigidbody2D rb;

     
    void Update () {

    }

    private void FixedUpdate()
    {
        // Collider2D coll = GetComponent<Collider2D>();
        // if (Physics2D.IsTouchingLayers(coll))
        // {
        //     PushbackPlayerPosition(coll);
        // }
        //Keyboard commands
        Vector3 p = GetBaseInput();
        if (p.sqrMagnitude > 0){ // only move while a direction key is pressed
            p = p * mainSpeed;
            p = p * Time.deltaTime;
            rb.velocity = p;
            // transform.Translate(p);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void PushbackPlayerPosition(Collider2D coll)
    {
        Vector2 direction = transform.position - coll.transform.position;
        direction.y = 0;
        direction.Normalize();
        transform.position += (Vector3)direction * 1f;
    }

    private Vector3 GetBaseInput() { //returns the basic values, if it's 0 than it's not active.
        Vector3 p_Velocity = new Vector3();
        if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)){
            p_Velocity += new Vector3(0, 1 , 0);
        }
        if (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)){
            p_Velocity += new Vector3(0, -1, 0);
        }
        if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)){
            p_Velocity += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)){
            p_Velocity += new Vector3(1, 0, 0);
        }
        return p_Velocity;
    }
}
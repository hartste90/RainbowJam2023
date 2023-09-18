using System;
using UnityEngine;
using System.Collections;
 
public class PlayerMovement : MonoBehaviour {
    public float mainSpeed = 10.0f; //regular speed
    // public float shiftAdd = 250.0f; //multiplied by how long shift is held.  Basically running
    // public float maxShift = 1000.0f; //Maximum speed when holdin gshift
    private float totalRun= 1.0f;

    private Vector3 lastPosition;
     
    void Update () {
        lastPosition = transform.position;
        //Keyboard commands
        float f = 0.0f;
        Vector3 p = GetBaseInput();
        if (p.sqrMagnitude > 0){ // only move while a direction key is pressed
          if (Input.GetKey (KeyCode.LeftShift)){
              totalRun += Time.deltaTime;
              p = p * totalRun;// * shiftAdd;
              // p.x = Mathf.Clamp(p.x, -maxShift, maxShift);
              // p.y = Mathf.Clamp(p.y, -maxShift, maxShift);
              // p.z = Mathf.Clamp(p.z, -maxShift, maxShift);
          } else {
              totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 1000f);
              p = p * mainSpeed;
          }
         
          p = p * Time.deltaTime;
          transform.Translate(p);
        }
    }

    private void LateUpdate()
    {
        Collider2D coll = GetComponent<Collider2D>();
        Debug.Log(coll);
        if (Physics2D.IsTouchingLayers(coll))
        {
            Debug.Log("converging");
            PushbackPlayerPosition(coll);
        }
        else
        {
            Debug.Log("not convergings");
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
        if (Input.GetKey (KeyCode.W)){
            p_Velocity += new Vector3(0, 1 , 0);
        }
        if (Input.GetKey (KeyCode.S)){
            p_Velocity += new Vector3(0, -1, 0);
        }
        if (Input.GetKey (KeyCode.A)){
            p_Velocity += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey (KeyCode.D)){
            p_Velocity += new Vector3(1, 0, 0);
        }
        return p_Velocity;
    }
}
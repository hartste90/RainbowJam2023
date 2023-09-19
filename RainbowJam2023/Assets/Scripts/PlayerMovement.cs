using System;
using UnityEngine;
using System.Collections;
 
public class PlayerMovement : MonoBehaviour {
    public float mainSpeed = 10.0f;
    private float totalRun= 1.0f;

     
    void Update () {
        //Keyboard commands
        float f = 0.0f;
        Vector3 p = GetBaseInput();
        if (p.sqrMagnitude > 0){ // only move while a direction key is pressed
          totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 1000f);
              p = p * mainSpeed;
          p = p * Time.deltaTime;
          transform.Translate(p);
        }
    }

    private void LateUpdate()
    {
        Collider2D coll = GetComponent<Collider2D>();
        if (Physics2D.IsTouchingLayers(coll))
        {
            PushbackPlayerPosition(coll);
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
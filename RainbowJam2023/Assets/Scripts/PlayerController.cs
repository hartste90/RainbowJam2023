using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float maxDistance = 100f;
    private float speed = 6f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        
    }

    /// <summary>
    /// moves the player towards the mouse position
    /// </summary>
    void MovePlayer()
    {
        if (Input.GetMouseButton(0))
        {
            // Debug.Log("Mouse button is down");

            //get mouse position
            Vector3 fingerPos = Vector3.zero;
#if UNITY_EDITOR
            fingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //Desktop
#else
        fingerPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position); //Mobile
#endif
            fingerPos = new Vector3(fingerPos.x, fingerPos.y, 0);
            Vector3 pos = Vector3.MoveTowards(transform.position, fingerPos, speed * Time.deltaTime);
            transform.position = pos;
        }
    }
}

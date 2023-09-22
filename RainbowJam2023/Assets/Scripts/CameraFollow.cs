using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smoothTime = 0.3f;
    private Transform target;
    private Vector3 velocity = Vector3.zero;
    private Vector3 offset;
    
    void Start()
    {
        target = FindObjectOfType<PlayerManager>().transform;
        offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        // update position
        Vector3 targetPosition = target.position + offset;
        Camera.main.transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
 
    }
}

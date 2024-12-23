using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositionFollow : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private Transform[] targets;
    [SerializeField] private float smoothSpeed = 0.125f; 
    [SerializeField] private Vector3 offset;  

    private void LateUpdate()
    {
        if (targets == null || targets.Length == 0)
        {
            Debug.LogWarning("No targets assigned for the camera to follow.");
            return;
        }

        Transform activeTarget = FindActiveTarget();
        if (activeTarget == null)
        {
            Debug.LogWarning("No active target found for the camera to follow.");
            return;
        }

      
        Vector3 desiredPosition = activeTarget.position + offset;

        
        desiredPosition.y = transform.position.y;

       
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

       
        transform.position = smoothedPosition;
    }

    
    private Transform FindActiveTarget()
    {
        foreach (Transform target in targets)
        {
            if (target != null && target.gameObject.activeInHierarchy)
            {
                return target;
            }
        }
        return null;
    }
}

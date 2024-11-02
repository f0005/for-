using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class parallax : MonoBehaviour
{
    [SerializeField] private Vector2 parallaxEffect;
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;
   


    void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
    }
    private void LateUpdate() 
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * parallaxEffect.x, deltaMovement.y * parallaxEffect.y, 0);
        lastCameraPosition = cameraTransform.position;
    }
}

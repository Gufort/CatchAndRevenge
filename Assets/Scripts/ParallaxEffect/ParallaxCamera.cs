using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ParallaxCamera : MonoBehaviour 
{
    public delegate void ParallaxCameraDelegate(float deltaMovement);
    public event ParallaxCameraDelegate onCameraTranslate;

    private float oldPosition;

    void Start()
    {
        oldPosition = transform.position.x;
    }

    void Update()
    {
        if (transform.position.x != oldPosition)
        {
            float delta = oldPosition - transform.position.x;
            onCameraTranslate?.Invoke(delta);
            oldPosition = transform.position.x;
        }
    }
}

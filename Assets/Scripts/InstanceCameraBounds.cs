using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class InstanceCameraBounds : MonoBehaviour
{
    public int bounds = 3;
    public CinemachineConfiner confiner;
    public CinemachineVirtualCamera virtualCamera;
    public PolygonCollider2D newBounds;
    public Transform player;
    void Start()
    {
        confiner.m_BoundingShape2D = newBounds;
        virtualCamera.m_Lens.OrthographicSize = bounds;
        virtualCamera.Follow = player;
    }

}

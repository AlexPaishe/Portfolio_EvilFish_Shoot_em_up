using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallenCameraScript : MonoBehaviour
{
    [SerializeField] private Transform Player;
    private Vector3 offset;

    private void Awake()
    {
        offset = transform.position - Player.position;
    }

    void Update()
    {
        Vector3 fallenCamera = transform.position;
        fallenCamera.y = Player.position.y + offset.y;
        transform.position = fallenCamera;
    }
}

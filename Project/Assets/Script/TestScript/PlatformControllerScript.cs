using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlatformControllerScript : MonoBehaviour
{
    public Transform EndPlatform;

    void Start()
    {
        WorldControllerScript.instance.NavMeshBuilding();
        WorldControllerScript.instance.OnPlatformMovement += TryDelAndAddPlatform;
    }

    private void TryDelAndAddPlatform()//Уничтожение платформы
    {
        if (transform.position.z < WorldControllerScript.instance.minZ)
        {
            WorldControllerScript.instance.WorldBuilder.CreatPlatform();
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (WorldControllerScript.instance != null)
        {
            WorldControllerScript.instance.OnPlatformMovement -= TryDelAndAddPlatform;
        }
    }
}

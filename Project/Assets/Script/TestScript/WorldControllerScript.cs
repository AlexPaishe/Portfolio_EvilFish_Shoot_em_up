using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorldControllerScript : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;
    [SerializeField] public float minZ = -15f;
    [SerializeField] public WorldBuilderScript WorldBuilder;

    public delegate void TryToDelAndAddPlatform();
    public event TryToDelAndAddPlatform OnPlatformMovement;

    public static WorldControllerScript instance;

    private NavMeshSurface surface;

    void Awake()
    {
        if(WorldControllerScript.instance != null)
        {
            Destroy(gameObject);
            return;
        }
        WorldControllerScript.instance = this;
        surface = GetComponent<NavMeshSurface>();
    }

    private void Start()
    {
        StartCoroutine(OnPlatformMovementCorutine());
    }

    void Update()
    {
        transform.position -= Vector3.forward * speed * Time.deltaTime;
    }

    private void OnDestroy()
    {
        WorldControllerScript.instance = null;
    }

    IEnumerator OnPlatformMovementCorutine()//Проверка на создание платформы
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);
            if(OnPlatformMovement != null)
            {
                OnPlatformMovement();
            }
        }
    }

    public void NavMeshBuilding()//Создание Навигационной сетки
    {
        surface.BuildNavMesh();
    }
}

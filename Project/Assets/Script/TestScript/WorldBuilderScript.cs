using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBuilderScript : MonoBehaviour
{
    [Header("Platforms")]
    [SerializeField] private GameObject[] platformFirstZone;
    [SerializeField] private GameObject[] platformSecondZone;
    [SerializeField] private GameObject[] platformThirdZone;
    [SerializeField] private Transform platformContainer;

    [Header("Settings")]
    [SerializeField] private int LengthZone;
    private int currentPlatform = 0;
    private int NowZone = 0;

    private Transform lastplatform = null;
    private SpawnerScript spawner;

    void Awake()
    {
        spawner = FindObjectOfType<SpawnerScript>();
    }

    private void Start()
    {
        Init();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            CreatPlatform();
        }
    }

    public void Init()//Инициализация начальных платформ
    {
        for(int i = 0; i < 5; i++)
        {
            CreatPlatform();
        }
    }

    public void CreatPlatform()//Создание платформы и выбор биома
    {
        currentPlatform++;
        if(currentPlatform == LengthZone)
        {
            NowZone = Random.Range(0, 3);
            currentPlatform = 0;
        }

        if (currentPlatform % 2 == 0)
        {
            spawner.Spawn();
        }

        if(currentPlatform % 4 == 0)
        {
            spawner.SpawnBonus();
        }

        switch (NowZone)
        {
            case 0:
                CreateFirstZonePlatform();
                break;
            case 1:
                CreateSecondZonePlatform();
                break;
            case 2:
                CreateThirdZonePlatform();
                break;
        }
    }

    private void CreateFirstZonePlatform()//Создание платформ первого биома
    {
        Vector3 pos = (lastplatform == null) ? platformContainer.position 
            : lastplatform.GetComponent<PlatformControllerScript>().EndPlatform.position;

        int index = Random.Range(0, platformFirstZone.Length);
       GameObject res = Instantiate(platformFirstZone[index], pos, Quaternion.identity, platformContainer);
        lastplatform = res.transform;
    }

    private void CreateSecondZonePlatform()//Создание платформ второго биома
    {
        Vector3 pos = (lastplatform == null) ? platformContainer.position
            : lastplatform.GetComponent<PlatformControllerScript>().EndPlatform.position;

        int index = Random.Range(0, platformSecondZone.Length);
        GameObject res = Instantiate(platformSecondZone[index], pos, Quaternion.identity, platformContainer);
        lastplatform = res.transform;
    }

    private void CreateThirdZonePlatform()//Создание платформ третьего биома
    {
        Vector3 pos = (lastplatform == null) ? platformContainer.position
            : lastplatform.GetComponent<PlatformControllerScript>().EndPlatform.position;

        int index = Random.Range(0, platformThirdZone.Length);
        GameObject res = Instantiate(platformThirdZone[index], pos, Quaternion.identity, platformContainer);
        lastplatform = res.transform;
    }
}

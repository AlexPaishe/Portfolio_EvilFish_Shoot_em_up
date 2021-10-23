using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject[] _platforms;

    private SpawnerScript _spawn;

    private void Awake()
    {
        int rand = Random.Range(0, _platforms.Length);
        for (int i = 0; i < _platforms.Length; i++)
        {
            if (i == rand)
            {
                _platforms[i].SetActive(true);
                _platforms[i].transform.position = transform.position;
            }
            else
            {
                _platforms[i].SetActive(false);
            }
        }
        _spawn = FindObjectOfType<SpawnerScript>();
    }

    /// <summary>
    /// Изменение спауна врагов и предметов и появление новой платформы
    /// </summary>
    public void NewStep()
    {
        Vector3 NewTransform = transform.position;
        NewTransform.y = transform.position.y - 20;
        transform.position = NewTransform;

        _spawn.SpawnPoint();

        GameObject[] NewPlatformsVariation = new GameObject[_platforms.Length - 1];
        int variation = 0;
        for(int i = 0; i < _platforms.Length; i++)
        {
            if(_platforms[i].activeSelf == false)
            {
                NewPlatformsVariation[variation] = _platforms[i];
                variation++;
            }
        }

        int rand = Random.Range(0, NewPlatformsVariation.Length);
        NewPlatformsVariation[rand].SetActive(true);
        NewPlatformsVariation[rand].transform.position = transform.position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy
{
    public GameObject prefab;
    public int count;
}
public class SpawnerScript : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemies;
    [SerializeField] private List<GameObject> _enemiesOnScene;
    [SerializeField] public Vector3[] _enemiesSpawn;
    [SerializeField] private int _maxEnemies;
    [SerializeField] private GameObject _bonus;
    [SerializeField] private GameObject _medicineChest;

    private PlatformManagerScript _manager;

    private int _waveMax = 1;
    private int _wave = 0;

    private void Awake()
    {
        _manager = FindObjectOfType<PlatformManagerScript>();
        for(int i = 0; i < _enemiesSpawn.Length; i++)
        {
            _enemiesSpawn[i].y = _manager.gameObject.transform.position.y + 1.5f;
        }
        for(int i = 0; i < _enemies.Count; i++)
        {
            for(int j = 0; j< _enemies[i].count; j++)
            {
                GameObject enemyObject = Instantiate(_enemies[i].prefab);
                _enemiesOnScene.Add(enemyObject);
                int rand = Random.Range(0, _enemiesSpawn.Length);
                enemyObject.transform.position = _enemiesSpawn[rand];
                enemyObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Реализация спавна врагов с увеличением от количества волн
    /// </summary>
    public void Spawn()
    {
        if(_waveMax > _maxEnemies)
        {
            _waveMax = _maxEnemies;
        }

        for (int i = 0; i < _enemiesOnScene.Count; i++)
        {
            if (_wave < _waveMax)
            {
                if (_waveMax > 15 && i < _enemies[0].count && _waveMax - _wave > 8)
                {
                    if (_enemiesOnScene[i].activeSelf == false)
                    {
                        _enemiesOnScene[i].SetActive(true);
                        _wave += 7;
                    }
                }
                else if(_waveMax > 10 && i >= _enemies[0].count && i < _enemies[0].count * 2 && _waveMax - _wave > 6)
                {
                    if (_enemiesOnScene[i].activeSelf == false)
                    {
                        _enemiesOnScene[i].SetActive(true);
                        _wave += 5;
                    }
                }
                else if (_waveMax > 5 && i >= _enemies[0].count * 2 && i < _enemies[0].count * 3 && _waveMax - _wave > 3)
                {
                    if (_enemiesOnScene[i].activeSelf == false)
                    {
                        _enemiesOnScene[i].SetActive(true);
                        _wave += 2;
                    }
                }
                else if (i >= _enemies[0].count * 3)
                {
                    if (_enemiesOnScene[i].activeSelf == false)
                    {
                        _enemiesOnScene[i].SetActive(true);
                        _wave++;
                    }
                }
            }
        }
        _wave = 0;
        _waveMax++;
    }

    /// <summary>
    /// Реализация спавна бонуса
    /// </summary>
    public void SpawnBonus()
    {
        int rand = Random.Range(0, _enemiesSpawn.Length);
        GameObject bonusWeapon = Instantiate(_bonus, _enemiesSpawn[rand], Quaternion.identity);
    }

    /// <summary>
    /// Реализация спавна аптечки
    /// </summary>
    public void SpawnMedicineChest()
    {
        int rand = Random.Range(0, _enemiesSpawn.Length);
        GameObject chest = Instantiate(_medicineChest, _enemiesSpawn[rand], Quaternion.identity);
    }

    /// <summary>
    /// Изменение спавна под новую платформу
    /// </summary>
    public void SpawnPoint()
    {
        float newPos_Y = _manager.gameObject.transform.position.y + 1.5f;
        for (int i = 0; i < _enemiesSpawn.Length; i++)
        {
            _enemiesSpawn[i].y = newPos_Y;
        }

        for(int i = 0; i < _enemiesOnScene.Count; i++)
        {
            if(_enemiesOnScene[i].activeSelf == false)
            {
                int rand = Random.Range(0, _enemiesSpawn.Length);
                _enemiesOnScene[i].gameObject.transform.position = _enemiesSpawn[rand];
            }
        }
    }
}

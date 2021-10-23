using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    private bool _go = false;
    private bool _newPlatform = false;
    private PlatformManagerScript _manager;
    private SpawnerScript _spawn;

    private float _spawnpoint = -10f;
    private int _step = 0;

    private void Awake()
    {
        _manager = FindObjectOfType<PlatformManagerScript>();
        _spawn = FindObjectOfType<SpawnerScript>();
    }

    void Update()
    {
        if(_go == true)
        {
            transform.position -= Vector3.forward * _speed * Time.deltaTime;
        }

        if(transform.position.z < -155)
        {

            this.gameObject.SetActive(false);
        }
        else if(transform.position.z < -120)
        {
            if (_newPlatform == false)
            {
                _newPlatform = true;
                _manager.NewStep();
            }
        }

        if (transform.position.z < _spawnpoint && _spawnpoint > -110)
        {
            _spawn.Spawn();
            if(_step % 2 == 1)
            {
                _spawn.SpawnBonus();
            }
            if(_step % 4 == 0)
            {
                int rand = Random.Range(0, 5);
                if(rand == 1||rand == 2|| rand == 3|| rand == 4)
                {
                    _spawn.SpawnMedicineChest();
                }
            }
            _spawnpoint -= 30;
            _step++;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            if (collision.collider.gameObject.layer == 10)
            {
                _go = true;
            }
            else
            {
                _go = false;
            }
        }
    }

    private void OnEnable()
    {
        _newPlatform = false;
        _go = false;
        _spawnpoint = -10f;
    }
}

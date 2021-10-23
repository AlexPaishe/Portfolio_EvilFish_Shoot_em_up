using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody)),
    RequireComponent(typeof(AudioSource))]
public class PlayerMovementScript : MonoBehaviour
{
    private Rigidbody _rb;
    private CursorScript _cursor;

    [SerializeField] private float _speed;
    [SerializeField] private Animator _anima;
    [SerializeField] private GameObject _legs;

    private AudioSource _sound;
    private InterfaceScript _inter;
    private bool _pause = false;
    private bool _fallen = true;
   
    private void Awake()
    {
        _cursor = FindObjectOfType<CursorScript>();
        _rb = GetComponent<Rigidbody>();
        _sound = GetComponent<AudioSource>();
        _inter = FindObjectOfType<InterfaceScript>();
    }

    private void FixedUpdate()
    {
        float ver = Input.GetAxisRaw("Vertical");
        float hor = Input.GetAxisRaw("Horizontal");
        Move(ver, hor);
        //Vector3 target = cursor.transform.position;
        //target.y = transform.position.y;
        //transform.LookAt(target);
    }

    private void Update()
    {
        FallenAndPause();
    }

    /// <summary>
    /// Передвижение главного героя
    /// </summary>
    /// <param name="vertical"></param>
    /// <param name="horizontal"></param>
    private void Move(float vertical, float horizontal)
    {
        //agent.destination = new Vector3(transform.position.x + (1 * horizontal * speed * Time.deltaTime), transform.position.y,
        //    transform.position.z + (1 * vertical * speed * Time.deltaTime));
        //rb.AddForce(Vector3.forward * vertical * speed);
        //rb.AddForce(Vector3.right * horizontal * speed);
        _rb.AddForce(new Vector3(1 * horizontal * _speed, 0, 1 * vertical * _speed));
        if(horizontal !=0 || vertical !=0)
        {
            _anima.speed = 1.6f;
        }
        else if (horizontal == 0 && vertical == 0)
        {
            _anima.speed = 1f;
        }

        if(_rb.velocity.y >= 0)
        {
            _sound.pitch = _anima.speed;
        }

        _anima.SetFloat("Velocity", _rb.velocity.y);
        Vector3 LookLegs = _legs.transform.position;
        LookLegs.x += horizontal;
        LookLegs.z += vertical;
        if (_rb.velocity.y == 0 && horizontal!=0 || _rb.velocity.y == 0 && vertical !=0)
        {
            _legs.transform.LookAt(LookLegs);
        }
        else if(_rb.velocity.y == 0 && horizontal == 0 && vertical == 0)
        {
            LookLegs.z += 1;
            _legs.transform.LookAt(LookLegs);
        }
    }

    /// <summary>
    /// Отключение и включение звуков шагов при падении и паузе
    /// </summary>
    private void FallenAndPause()
    {
        bool fire = _inter.PauseReturn();
        if (fire == false && _pause == false && _rb.velocity.y >= 0 && _fallen == true)
        {
            _fallen = false;
            _sound.Play();
        }
        else if (fire == false && _pause == false && _fallen == false && _rb.velocity.y < 0)
        {
            _fallen = true;
            _sound.Pause();
        }
        else if (fire == false && _pause == false && _fallen == true && _rb.velocity.y < 0)
        {
            _sound.Pause();
        }
        if (fire == true && _pause == false)
        {
            _pause = true;
            _sound.Pause();
        }
        else if (fire == false && _pause == true)
        {
            _pause = false;
            _sound.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            switch(other.name)
            {
                case "WallOne":
                    _rb.velocity = Vector3.forward * 4;
                    break;
                case "WallTwo":
                    _rb.velocity = -Vector3.forward * 4;
                    break;
                case "WallThree":
                    _rb.velocity = -Vector3.right * 4;
                    break;
                case "WallFour":
                    _rb.velocity = Vector3.right * 4;
                    break;
            }
        }
    }
}

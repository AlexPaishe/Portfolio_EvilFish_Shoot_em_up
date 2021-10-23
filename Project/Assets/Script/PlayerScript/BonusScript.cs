using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusScript : MonoBehaviour
{
    [SerializeField] private GameObject[] _bonusVariation;
    [SerializeField] private float _speed;
    private int bonusNumber;
    private Vector3 Target;

    void Start()
    {
        bonusNumber = Random.Range(0, _bonusVariation.Length);
        for(int i = 0; i < _bonusVariation.Length;i++)
        {
            if(i == bonusNumber)
            {
                _bonusVariation[i].SetActive(true);
            }
            else
            {
                _bonusVariation[i].SetActive(false);
            }
        }
        Target = transform.position;
        Target.z = transform.position.z - 40;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Target, _speed * Time.deltaTime);
        transform.Rotate(0, 90 * Time.deltaTime, 0);
        if(transform.position == Target)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<ShootingScript>().NewWeapon(bonusNumber);
            Destroy(gameObject);
        }
    }
}

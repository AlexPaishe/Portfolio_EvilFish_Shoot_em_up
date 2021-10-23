using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicineChestScript : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Vector3 _target;

    void Start()
    {
        _target = transform.position;
        _target.z = transform.position.z - 40;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);
        transform.Rotate(0, 90 * Time.deltaTime, 0);
        if (transform.position == _target)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<HealthPlayer>().Healing();
            Destroy(gameObject);
        }
    }
}

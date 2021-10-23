using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerTest : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 Target;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Target = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                Target = hit.point;
                agent.destination = Target;
            }
        }
    }
}

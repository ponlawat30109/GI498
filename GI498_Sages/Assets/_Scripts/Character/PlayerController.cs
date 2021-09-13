using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent _agent;

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit _raycastHitInfo;

            if (Physics.Raycast(_ray, out _raycastHitInfo))
            {
                MoveToPoint(_raycastHitInfo.point);
            }
        }
    }

    void MoveToPoint(Vector3 point)
    {
        _agent.SetDestination(point);
    }
}

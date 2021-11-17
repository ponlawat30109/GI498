using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
// using UnityEngine.InputSystem;
// using UnityEngine.EventSystems;
// using Cinemachine;

public class PlayerController : MonoBehaviour/*, IPointerDownHandler*/
{
    private NavMeshAgent _agent;
    // private CinemachineVirtualCamera _vcam;
    // private CinemachineFollowZoom _followZoom;

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        // _vcam = GetComponent<CinemachineVirtualCamera>();
        // _followZoom = GetComponent<CinemachineFollowZoom>();
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

        // _followZoom.
    }

    void MoveToPoint(Vector3 point)
    {
        _agent.SetDestination(point);
    }

    // void OnPointerClick(PointerEventData eventData)
    // {
    //     if (eventData.clickCount == 2)
    //     {
    //         Debug.Log("double click");
    //     }
    // }
}

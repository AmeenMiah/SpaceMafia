using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ClostestEnemyTarget : MonoBehaviour
{
    public ClosestEnemy nearestEnemy;
    public CinemachineTargetGroup cinemachineTarget;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        nearestEnemy = ClosestEnemy.FindClosestEnemy(transform.position);
        cinemachineTarget.m_Targets[1].target = nearestEnemy.transform;
    }
}

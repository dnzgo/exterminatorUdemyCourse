using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraArm : MonoBehaviour
{

    [SerializeField] float armLength;
    [SerializeField] Transform child;
    
    void Update()
    {
        child.position = transform.position - child.forward * armLength;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(child.position, transform.position);
    }

}

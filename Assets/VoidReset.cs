using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidReset : MonoBehaviour
{
    [SerializeField] private GameObject voidResetPoint;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.position = voidResetPoint.transform.position;
        }
    }
}

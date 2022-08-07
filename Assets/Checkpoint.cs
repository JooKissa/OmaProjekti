using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GameObject spawnpoint;
    // Start is called before the first frame update
    void Start()
    {
        spawnpoint = GameObject.FindGameObjectWithTag("Spawnpoint");
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            spawnpoint.transform.position = gameObject.transform.position + new Vector3(0,1,0);
        }
    }
}

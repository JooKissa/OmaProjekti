using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrader : MonoBehaviour
{
    [SerializeField] private int upgraderStage;
    private bool used = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !used)
        {
            collision.gameObject.SendMessage("upgradeStage", upgraderStage);
            used = true;
            GetComponent<Renderer>().material.color = new Color(0, 0, 0);
        }
    }
}

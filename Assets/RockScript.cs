using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockScript : MonoBehaviour
{
    public Material Rock1;
    public Material Rock2;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(timer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator timer()
    {
        GetComponent<Renderer>().material = Rock1;
        yield return new WaitForSeconds(5);
        GetComponent<Renderer>().material = Rock2;
        yield return new WaitForSeconds(5);
        StartCoroutine(timer());
    }
}

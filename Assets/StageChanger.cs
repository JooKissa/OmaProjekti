using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageChanger : MonoBehaviour
{
    [SerializeField] private int tpDestination;
    [SerializeField] private bool isSpecial;
    [SerializeField] private int specialBoost = 1200;

    private void Start()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isSpecial)
            {
                StartCoroutine(special(collision));
            }
            else
            {
                Game.Instance.SendMessage("GoStage", tpDestination);
            }
        }
    }
    private IEnumerator special(Collision collision)
    {
        collision.gameObject.GetComponent<Rigidbody>().AddForce(transform.up * specialBoost, ForceMode.VelocityChange);
        yield return new WaitForSeconds(5);
        Game.Instance.loadScreen.SetActive(true);
        Game.Instance.SendMessage("GoStage", tpDestination);
        yield return new WaitForSeconds(0.5f);
        Game.Instance.loadScreen.SetActive(false);
    }
}

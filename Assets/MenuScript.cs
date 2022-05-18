using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private Text levelNumber;
    [SerializeField] private GameObject bBack;
    [SerializeField] private GameObject bForw;
    private int currentSelectable = 1;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
        //levelNumber.text = currentSelectable.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void onClickBack()
    {

    }
    private void OnClickForw()
    {

    }
    public void OnStart()
    {
        gameObject.SetActive(false);
        Game.Instance.SendMessage("GoStage", 1);
    }
}

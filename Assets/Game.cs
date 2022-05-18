using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

    public static Game Instance;
    [SerializeField] private GameObject[] teleports;
    [SerializeField] private GameObject[] stageObjects;
    [HideInInspector] public GameObject Player;
    private GameObject spawnPoint;
    [HideInInspector] public GameObject loadScreen;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null) Instance = this;
        Player = GameObject.FindGameObjectWithTag("Player");
        spawnPoint = GameObject.FindGameObjectWithTag("Spawnpoint");
        loadScreen = GameObject.FindGameObjectWithTag("LoadScreen");
        loadScreen.SetActive(false);
        foreach(GameObject stage in stageObjects)
        {
            if (stage != stageObjects[0]) stage.SetActive(false);
        }
        GoStage(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1)) {
            GoStage(1);
        }
        if (Input.GetKey(KeyCode.Alpha2)) {
            GoStage(2);
        }
        if (Input.GetKey(KeyCode.Alpha3)) {
            GoStage(3);
        }
        if (Input.GetKey(KeyCode.Alpha4)) {
            GoStage(4);
        }
        if (Input.GetKey(KeyCode.Alpha5)) {
            GoStage(5);
        }
        if (Input.GetKey(KeyCode.Alpha6)) {
            GoStage(5);
        }
    }

    private void GoStage(int destination)
    {
        StartCoroutine(Process(destination));
    }
    private IEnumerator Process(int destination)
    {
        loadScreen.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        foreach (GameObject stage in stageObjects)
        {
            if (stage != stageObjects[0]) stage.SetActive(false);
        }
        stageObjects[destination].SetActive(true);
        Player.transform.position = teleports[destination].transform.position;
        spawnPoint.transform.position = teleports[destination].transform.position;
        yield return new WaitForSeconds(0.5f);
        loadScreen.SetActive(false);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public UIManager uiManager;
    public Vector2 rotateRadiusRange;
    
    public Vector2 rotateSpeedRange;
    public Vector2 falldownRange;
    public Vector2 initHeightRange;

    public Vector2 initIntervalRange;
    public Vector2 initNumRange;
    public Vector2 sphereSizeRange;
    public float timer;
    public float currentInterval;
    public GameObject spherePrefabs;
    public int targetScore;
    public int scores;

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        currentInterval = Random.Range(initIntervalRange.x, initIntervalRange.y);
    }

    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime;
        if (timer > currentInterval) {
            timer = 0;
            currentInterval = Random.Range(initIntervalRange.x, initIntervalRange.y);
            for (int i = 0; i < Random.Range(initNumRange.x, initNumRange.y); ++i) {
                float tmpHeight = Random.Range(initHeightRange.x,initHeightRange.y);
                float tmpRadius = Random.Range(rotateRadiusRange.x,rotateRadiusRange.y);
                float tmpRad = Random.Range(0, 2*Mathf.PI);
                Vector3 tmpLoc = new Vector3(tmpRadius*Mathf.Cos(tmpRad),tmpHeight,tmpRadius*Mathf.Sin(tmpRad));
                GameObject obj = GameObject.Instantiate(spherePrefabs, tmpLoc, Quaternion.identity);
                obj.GetComponent<ObjectController>().falldownSpeed = Random.Range(falldownRange.x, falldownRange.y);
                obj.GetComponent<ObjectController>().rotateSpeed = Random.Range(rotateSpeedRange.x, rotateSpeedRange.y);
            }
        }

        if (scores == targetScore) {
            Time.timeScale = 0;
            UIManager.instance.ActivateDurationText(Time.time);
        }
    }

    public void AddScore() {
        scores += 1;
        uiManager.SetScores(scores);
    }
}

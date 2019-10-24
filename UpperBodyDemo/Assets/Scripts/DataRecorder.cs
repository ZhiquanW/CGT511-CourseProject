using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataRecorder : MonoBehaviour {
    public Transform controllerTransform;

    public List<float> speedList;
    public List<float> timeList;
    public List<float> disList;
    public Vector3 prePos;
    public float timeInterval;
    public float timer;
    
    // Use this for initialization
    void Start() {
        prePos = controllerTransform.position;
    }

    // Update is called once per frame
    void Update() {
        if (timer > timeInterval) {
            disList.Add(Vector3.Magnitude(controllerTransform.position - prePos));
            speedList.Add(Vector3.Magnitude(controllerTransform.position - prePos)/timeInterval);
            prePos = controllerTransform.position;
        }
    }
    
    public void recordTime(float time) {
        timeList.Add(time);
    }
}
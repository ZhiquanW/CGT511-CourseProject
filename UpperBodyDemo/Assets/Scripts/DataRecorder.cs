using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataRecorder : MonoBehaviour {
    public static DataRecorder Instance;
    public string fileName;
    public Transform controllerTransform;
    public List<float> speedList;
    public List<float> timeList;
    public List<float> disList;
    public Vector3 prePos;
    public float timeInterval;
    public float timer;
    public bool toggleOutput;
    private void Awake() {
        Instance = this;
    }

    // Use this for initialization
    void Start() {
        prePos = controllerTransform.position;
    }

    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime;
        if (timer > timeInterval) {
            timer = 0;
            disList.Add(Vector3.Magnitude(controllerTransform.position - prePos));
            speedList.Add(Vector3.Magnitude(controllerTransform.position - prePos)/timeInterval);
            prePos = controllerTransform.position;
        }

        if (toggleOutput) {
            toggleOutput = !toggleOutput;
            OutputData();
        }
    }
    
    public void RecordTime(float time) {
        timeList.Add(time);
    }
    void OutputData() {
        using (System.IO.StreamWriter file = 
            new System.IO.StreamWriter(fileName+".txt", true)) {
            //output speed list
            file.WriteLine("Speed");
            foreach (var s in speedList) {
                file.Write(s+" ");
            }
            //output dis list
            file.WriteLine("Distance");
            foreach (var dis in disList) {
                file.Write(dis+" ");
            }
            //output time list
            file.Write("Time");
            foreach (var t in timeList) {
                file.Write(t+" ");
            }
        }
    }
}

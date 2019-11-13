using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataRecorder : MonoBehaviour {
    public static DataRecorder Instance;
    public string fileName;
    public Transform controllerTransform;
    public Transform returnPos;
    public List<float> timeList;
    public List<float> disList;
    public float playerID;
    public float deviceID;
    public float totalDev = 0;
    public float totalDis = 0;
    public Vector3 prePos;
    public float timeInterval;
    public float timer;
    public float durationTimer = 0;
    public bool toggleOutput;
    public bool isRecording = false;
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
            durationTimer += Time.deltaTime;
            if (timer > timeInterval) {
                timer = 0;
                totalDis += Vector3.Magnitude(controllerTransform.position - prePos);
                prePos = controllerTransform.position;
                totalDev += DisP2L(controllerTransform.position, returnPos.position,
                    GameManager.instance.targetBlock.transform.position);
            }

            if (toggleOutput) {
                toggleOutput = !toggleOutput;
                OutputData();
            }
    }

    // Invoke after the controller touched the target block

    
    public float DisP2L(Vector3 point, Vector3 linePoint1, Vector3 linePoint2) {
        float fProj = Vector3.Dot(point - linePoint1, (linePoint1 - linePoint2).normalized);
        return Mathf.Sqrt((point - linePoint1).sqrMagnitude - fProj * fProj);
    }
    void OutputData() {
        using (System.IO.StreamWriter file = 
            new System.IO.StreamWriter(fileName+".txt", true)) {
            file.WriteLine("PlayerNum:"+playerID);
            file.WriteLine("DeviceID:"+deviceID);
            file.WriteLine("Distance:"+totalDis);
            file.WriteLine("Speed:"+(totalDis/timer));
            file.WriteLine("Deviation:"+totalDev);
        }
    }
}

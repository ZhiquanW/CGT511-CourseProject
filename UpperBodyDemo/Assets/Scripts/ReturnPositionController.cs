using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnPositionController : MonoBehaviour {
    public Transform CameraTransform;
    public Vector3 offset;
    public bool isReturned;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        this.transform.localPosition = CameraTransform.localPosition + offset;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Controller")) {
            if (GameManager.instance.targetBlock!=null && GameManager.instance.targetBlock.isTouched) {
                Debug.Log("Controller Returned");
                GameManager.instance.targetBlock.isTouched = false;
                GameManager.instance.targetBlock.targetColor = GameManager.instance.targetBlock.originalColor;
                isReturned = true;
                DataRecorder.Instance.isRecording = true;
                AudioManagement.Instance.playSound();
                 
                GameManager.instance.currentTargetIndex += 1;
                if (GameManager.instance.currentTargetIndex < GameManager.instance.targetSequence.Length) {
                    GameManager.instance.LaunchNextTouch();
                }
                else {
                    if (DataRecorder.Instance.isRecorded == false) {
                        DataRecorder.Instance.OutputData();
                        DataRecorder.Instance.isRecorded = true;
                    }

                    foreach (var cube in GameManager.instance.blockMat) {
                        cube.GetComponent<BlockController>().targetColor = Color.clear;
                    }

                    GameManager.instance.isStarted = false;
                }

            }
        }
    }

 
}
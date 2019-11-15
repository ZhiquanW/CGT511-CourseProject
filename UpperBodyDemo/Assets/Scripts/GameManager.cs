using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public int scores;
    public GameObject blockPrefab;
    public ReturnPositionController returnPos;
    public Vector2 blocksSize;
    public float curtainRadius;
    public float curtainDegree;
    public float curtainRatio;
    private Vector2 curtainSize;
    private Vector2 curtainIntervals; //(radians,length)
    public Vector3 centerPos;
    public GameObject[,] blockMat;
    public Vector2[] targetSequence;
    public int currentTargetIndex;
    public BlockController targetBlock;
    public float timer = 0;
    public bool isStarted = false;
    public Text timerText;
    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        curtainSize.x = 2 * Mathf.PI * curtainRadius * curtainDegree * Mathf.Deg2Rad;
        curtainSize.y = curtainRatio * curtainSize.x;
        curtainIntervals.x = curtainDegree / blocksSize.x;
        curtainIntervals.y = curtainSize.y / blocksSize.y;
        blockMat = new GameObject[(int) blocksSize.x, (int) blocksSize.y];
        InitCurtain();
    }

    private void Update() {
        if (!isStarted) {
            return;
        }

        timerText.text = timer.ToString();
        timer += Time.deltaTime;
    

    }

    public void LaunchNextTouch() {
        returnPos.isReturned = false;
        Vector2 tmpIndex = targetSequence[currentTargetIndex];
        targetBlock = blockMat[(int) tmpIndex.x, (int) tmpIndex.y].GetComponent<BlockController>();
        targetBlock.isTarget = true;
    }

    void InitCurtain() {
        for (int i = 0; i < blocksSize.x; ++i) {
            for (int j = 0; j < blocksSize.y; ++j) {
                blockMat[i, j] = InitBlock(i, j);
            }
        }
    }

    public void StartGame() {
        isStarted = true;
        DataRecorder.Instance.deviceID = Convert.ToInt32(DataRecorder.Instance.deviceIDField.text);
        DataRecorder.Instance.fileName += "Device" + DataRecorder.Instance.deviceID;
        LaunchNextTouch();
    }
    GameObject InitBlock(int _x, int _y) {
        float radianOffset = (_x - (blocksSize.x - 1) / 2) / (blocksSize.x - 1) * curtainDegree * Mathf.Deg2Rad;
        Vector3 tmpPos = centerPos + new Vector3(curtainRadius * Mathf.Sin(radianOffset), _y * curtainIntervals.y,
                             curtainRadius * Mathf.Cos(radianOffset));
        return Instantiate(blockPrefab, tmpPos, Quaternion.identity);
    }
}


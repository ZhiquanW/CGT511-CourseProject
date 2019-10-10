using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

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
    private Vector2 curtainIntervals;//(radians,length)
    public Vector3 centerPos;
    private GameObject[,] blockMat;
    public Vector2[] targetSequence;
    public int currentTargetIndex;
    public BlockController targetBlock;
    public float waitTime = 0;
    public float timer = 0;
    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        curtainSize.x = 2 * Mathf.PI * curtainRadius *curtainDegree* Mathf.Deg2Rad;
        curtainSize.y = curtainRatio * curtainSize.x;
        curtainIntervals.x = curtainDegree / blocksSize.x;
        curtainIntervals.y = curtainSize.y / blocksSize.y;
        blockMat = new GameObject[(int) blocksSize.x, (int)blocksSize.y];
        InitCurtain();
    }

    private void Update() {
        if (returnPos.isReturned) {
            timer += Time.deltaTime;
        }
        if (timer > waitTime) {
            timer = 0;
            ++currentTargetIndex;
            LaunchNextTouch();
        }
    }

    void LaunchNextTouch() {
        returnPos.isReturned = false;
        Vector2 tmpIndex = targetSequence[currentTargetIndex];
        targetBlock = blockMat[(int) tmpIndex.x, (int) tmpIndex.y].GetComponent<BlockController>();
        targetBlock.isTarget = true;
    }
    void InitCurtain() {
        for (int i = 0; i<blocksSize.x; ++i) {
            for (int j = 0; j < blocksSize.y; ++j) {
                blockMat[i,j] = InitBlock(i,j);
            }
        }
    }

    GameObject InitBlock(int _x,int _y) {
        float radianOffset = (_x - (blocksSize.x-1)/2)/(blocksSize.x-1) * curtainDegree*Mathf.Deg2Rad;
        Vector3 tmpPos = centerPos + new Vector3(curtainRadius* Mathf.Sin(radianOffset), _y * curtainIntervals.y, curtainRadius*Mathf.Cos(radianOffset));
        return Instantiate(blockPrefab, tmpPos, Quaternion.identity);
    }
}
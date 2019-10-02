using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public int scores;
    public GameObject blockPrefab;
    public Vector2 blocksSize;
    public float curtainRadius;
    public float curtainDegree;
    public float curtainRatio;
    private Vector2 curtainSize;
    private Vector2 curtainIntervals;//(radians,length)
    public Vector3 centerPos;

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        curtainSize.x = 2 * Mathf.PI * curtainRadius *curtainDegree* Mathf.Deg2Rad;
        curtainSize.y = curtainRatio * curtainSize.x;
        curtainIntervals.x = curtainDegree / blocksSize.x;
        curtainIntervals.y = curtainSize.y / blocksSize.y;
        InitCurtain();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitCurtain() {
        for (int i = 0; i<blocksSize.x; ++i) {
            for (int j = 0; j < blocksSize.y; ++j) {
                InitBlock(i,j);
            }
        }
    }

    void InitBlock(int _x,int _y) {
        float radianOffset = (_x - (blocksSize.x-1)/2)/(blocksSize.x-1) * curtainDegree*Mathf.Deg2Rad;
        Debug.Log(radianOffset);
        float zOffset = Mathf.Cos(radianOffset);
        float xOffset = Mathf.Sin(radianOffset);
        float yOffset = _y * curtainIntervals.y;
        Vector3 tmpPos = centerPos + new Vector3(curtainRadius* Mathf.Sin(radianOffset), _y * curtainIntervals.y, curtainRadius*Mathf.Cos(radianOffset));
        Instantiate(blockPrefab, tmpPos, Quaternion.identity);
    }
}

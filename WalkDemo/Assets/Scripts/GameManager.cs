using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    public Transform player;

    public Indicator indicator;
    private LineRenderer lineRenderer;
    private List<Vector3> positions;
    public Dropdown dropDown;

    private FileWrite fileWrite;
    private string[] fileContent = new string[3];

    public InputField PlayerNum;
    private int condition;
    private int index = 0;
    private float time = 0.0f;
    private float dis = 0.0f;
    private float dev = 0.0f;
	// Use this for initialization
	void Start () {
        if(instance == null)
        {
            instance = this;
        }
        //lineRenderer = GetComponent<LineRenderer>();
        positions = new List<Vector3>();
        fileWrite = new FileWrite();
        condition = dropDown.value + 1;
    }
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;

	}
    
    IEnumerator RecordData()
    {
        Debug.Log("StartRecord");
        while (true)
        {
            Vector3 pos = new Vector3(player.position.x, 0f, player.position.z);
            positions.Add(pos);
            //lineRenderer.positionCount = positions.Count;
            //lineRenderer.SetPosition(index,positions[index]);
            if (index > 0)
            {
                dis += Vector3.Distance(positions[index], positions[index - 1]);
            }
            dev += indicator.CalculateDeviation(pos);
            index++;
            yield return new WaitForSeconds(0.1f);
        }
        
    }
    
    public void SaveResults()
    {
        dev = dev / positions.Count;
        float speed = dis / time;
        string playerNum = PlayerNum.text;
        // Modify here
        fileContent[condition - 1] += "\n\nPlayerNum:" + playerNum + "\nDistance:" + dis.ToString("F2")
            + "\nTime:" + time.ToString("F2") + "\nSpeed:" + speed.ToString("F2") + "\nDeviation:" + dev.ToString("F2");
        for (int i = 0; i < 3; i++)
        {
            string fileName = "Condition" + (i + 1).ToString() + ".txt";
            fileWrite.PrintResults(fileName, fileContent[i]);
            fileContent[i] = "";
        }
    }
    public void Reset()
    {
        indicator.Reset();
        positions.Clear();
        index = 0;
        dis = 0;
        dev = 0;
        StopAllCoroutines();
    }
    public void StartRecord()
    {
        time = 0;
        condition = dropDown.value + 1;
        
        StartCoroutine(RecordData());
    }
}

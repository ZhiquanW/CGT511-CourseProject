using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour {
    public static UIManager instance;
    public Text scoreText;

    public Text durationText;

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetScores(int _s) {
        scoreText.text = "Scores : " + _s.ToString();
    }

    public void ActivateDurationText(float _t) {
        durationText.text = _t.ToString() + " s";
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour {
    public MeshRenderer blockRenderer;

    public bool isTarget;

    public bool isTouched;

    public Color targetColor;
    public Color originalColor;
    public Color targetedColor;
    public Color touchedColor;
    public float colorChangeSpeed;

    public float timer;
    // Start is called before the first frame update
    void Start() {
        originalColor = blockRenderer.material.color;
    }

    // Update is called once per frame
    void Update() {
        blockRenderer.material.color = Color.Lerp(blockRenderer.material.color,targetColor,colorChangeSpeed);
        if (isTarget) {
            targetColor = targetedColor;
        }

    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Controller")) {
            GameManager.instance.scores += 1;
            if (isTarget) {
                isTouched = true;
                targetColor = touchedColor;
                AudioManagement.Instance.playSound();
            }
            isTarget = false;
        }
    }
}
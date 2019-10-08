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
    public float colorChangeSpeed;
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
        else {
            targetColor = originalColor;
        }
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("1s");
        if (other.gameObject.CompareTag("Controller")) {
            GameManager.instance.scores += 1;
            isTouched = true;
        }
    }
}

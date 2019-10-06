using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnPositionController : MonoBehaviour {

    public bool isReturned;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Controller")) {
            if (GameManager.instance.targetBlock.isTouched) {
                isReturned = true;
            }
        }
    }
}

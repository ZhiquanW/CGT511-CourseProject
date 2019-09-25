using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour {
    public float falldownSpeed;

    public float rotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Vector3.zero, Vector3.up,rotateSpeed* Time.deltaTime);
        transform.Translate(falldownSpeed*Time.deltaTime*Vector3.down);
    }

    public void SetFallDownSpeed(float _s) {
        falldownSpeed = _s;
    }

    public void SetRotateSpeed(float _s) {
        rotateSpeed = _s;
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Ground")) {
            Destroy(this.gameObject);
        }else if (other.gameObject.CompareTag("Hand")) {
            Destroy(this.gameObject);
            GameManager.instance.AddScore();
            AudioManager.instance.PlayTouched();
        }

        
    }
}

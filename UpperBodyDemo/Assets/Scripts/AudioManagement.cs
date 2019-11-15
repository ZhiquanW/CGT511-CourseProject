using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManagement : MonoBehaviour {
	public AudioSource aSource;

	static public AudioManagement Instance;

	private void Awake() {
		Instance = this;
	}

	// Use this for initialization
	void Start () {
		aSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void playSound() {
		aSource.pitch = Random.Range(0.9f, 1.1f);
		aSource.Play();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    Transform target;

	// Use this for initialization
	void Start () {
        target = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 NewCamera = target.position;
        NewCamera.z = transform.position.z;
        transform.position = NewCamera;
	}
}

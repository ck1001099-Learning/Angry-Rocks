using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour {

	public Transform target;

	public float cameraRangeXMin;
	public float cameraRangeXMax;
	public float cameraRangeYMin;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		CalculatePosition ();
	}

	public void CalculatePosition(){
		if (target != null) {
			Vector3 newPosition = target.position;
			if (newPosition.y < cameraRangeYMin) {
				newPosition.y = cameraRangeYMin;
			}
			if (newPosition.x < cameraRangeXMin) {
				newPosition.x = cameraRangeXMin;
			}
			if (newPosition.x > cameraRangeXMax) {
				newPosition.x = cameraRangeXMax;
			}
			newPosition.z = -10;
			this.transform.DOMove (newPosition, 1f);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {

	public int hitPoint = 2;
	public float threshold = 2;

	private GameController gameController;

	// Use this for initialization
	void Start () {
		gameController = GameObject.Find ("GameController").GetComponent<GameController> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag != "attack")
			return;
		if (coll.relativeVelocity.magnitude < threshold)
			return;
		hitPoint = hitPoint - 1;

		if (hitPoint == 0)
			Dead ();
	}

	public void Dead(){
		gameController.Dead (this.transform);
		Destroy(this.gameObject);
	}
}

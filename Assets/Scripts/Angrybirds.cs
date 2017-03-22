using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angrybirds : MonoBehaviour {

	public GameController gameController;
	public GameObject stage;

	public float maxLength = 3f;

	public GameObject frontCatapult;
	public GameObject backCatapult;
	private LineRenderer lineFrontCatapult;
	private LineRenderer lineBackCatapult;

	private Rigidbody2D rb2d;

	private int launchMode = 0;

	public float initVelocity;
	public float threshold;

	public float birdRangeXMin;
	public float birdRangeXMax;

	public AudioSource audioLaungh;

	// Use this for initialization
	void Start () {
		lineFrontCatapult = frontCatapult.GetComponent<LineRenderer> ();
		lineBackCatapult = backCatapult.GetComponent<LineRenderer> ();
		lineFrontCatapult.sortingLayerName = frontCatapult.GetComponent<SpriteRenderer> ().sortingLayerName;
		lineBackCatapult.sortingLayerName = backCatapult.GetComponent<SpriteRenderer> ().sortingLayerName;
		lineFrontCatapult.enabled = true;
		lineBackCatapult.enabled = true;
		rb2d = this.GetComponent<Rigidbody2D> ();
		audioLaungh = this.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (launchMode == 1) {
			rb2d.velocity = -(this.transform.position - frontCatapult.transform.position).normalized * initVelocity;
			audioLaungh.Play ();
			launchMode = 2;
		} else if (launchMode == 0) {
			Vector3 frontEnd = this.transform.position + (this.transform.position - frontCatapult.transform.position).normalized * 0.64f * 0.6f;
			Vector3 backEnd = this.transform.position + (this.transform.position - backCatapult.transform.position).normalized * 0.64f * 0.6f;
			lineFrontCatapult.SetPositions (new Vector3[]{ frontCatapult.transform.position, frontEnd });
			lineBackCatapult.SetPositions (new Vector3[]{ backCatapult.transform.position, backEnd });
		} else if (launchMode == 2 && (this.transform.position - frontCatapult.transform.position).magnitude <= threshold) {
			lineFrontCatapult.enabled = false;
			lineBackCatapult.enabled = false;
			rb2d.gravityScale = 1;
			gameController.FlyBird ();
			launchMode = 3;
		} else if (launchMode == 3) {
			if (rb2d.velocity.magnitude == 0) {
				gameController.CreateBird ();
				Destroy (this.gameObject);
			}
			if (this.transform.position.x > birdRangeXMax || this.transform.position.x < birdRangeXMin) {
				gameController.CreateBird ();
				Destroy (this.gameObject);
			}
		}
	}

	void OnMouseUp(){
		if (launchMode == 0)
			launchMode = 1;
	}

	void OnMouseDrag(){
		if (launchMode == 0) {
			Vector3 mousePoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			mousePoint.z = 0;
			Vector3 direction = (mousePoint - frontCatapult.transform.position).normalized;
			float magnitude = (mousePoint - frontCatapult.transform.position).magnitude;
			if (magnitude > maxLength)
				magnitude = maxLength;
			this.transform.position = frontCatapult.transform.position + direction * magnitude;
		}
	}
}

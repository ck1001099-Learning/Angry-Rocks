using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public Camera mainCamera;

	public GameObject angryBird;
	public GameObject frontCatapult;
	public GameObject backCatapult;

	public Vector3 angryBirdInitPos;

	public GameObject[] stageList;

	public Text stageText;
	public Text birdsText;

	public int initBird;
	public int numRemainBird;
	public int stageNum;
	private GameObject stage;

	public ParticleSystem particlesystem;
	private bool dead;

	public GameObject clear;
	public GameObject fail;

	public AudioSource deadAudio;

	// Use this for initialization
	void Start () {
		SetText ();
		deadAudio = this.transform.FindChild("Dead").GetComponent<AudioSource> ();
		//New Stage
		stageNum = 1;
		stage = GameObject.Instantiate (stageList[stageNum-1]);
		dead = false;
		numRemainBird = initBird;
		CreateBird ();
	}
	
	// Update is called once per frame
	void Update () {
		SetText ();
		if (Input.GetKey (KeyCode.Z)) {
			if (!dead)
				Reset ();
			else if (dead) {
				if (stageNum == 1)
					Stage2 ();
				else if (stageNum == 2)
					Stage1 ();
			}
		}
	}

	public void SetText(){
		stageText.text = "STAGE " + stageNum.ToString ();
		birdsText.text = "rocks:" + numRemainBird.ToString ();
	}
	public void FlyBird(){
		numRemainBird = numRemainBird - 1;
	}
	public void CreateBird(){
		if (numRemainBird > 0) {
			if (dead)
				return;
			GameObject bird = GameObject.Instantiate (angryBird);
			bird.transform.parent = stage.transform;
			bird.transform.position = angryBirdInitPos;
			bird.GetComponent<Angrybirds> ().frontCatapult = frontCatapult;
			bird.GetComponent<Angrybirds> ().backCatapult = backCatapult;
			bird.GetComponent<Angrybirds> ().gameController = this.GetComponent<GameController> ();
			mainCamera.GetComponent<CameraController> ().target = bird.transform;
		} else {
			if (!dead)
				fail.gameObject.SetActive (true);
		}
	}
	public void Reset(){
		//Destroy
		particlesystem.gameObject.SetActive (false);
		fail.gameObject.SetActive (false);
		Destroy (stage);
		//New Stage
		stage = GameObject.Instantiate(stageList[stageNum-1]);
		dead = false;
		numRemainBird = initBird;
		CreateBird ();
	}

	public void Stage1(){
		//Destroy
		particlesystem.gameObject.SetActive(false);
		clear.gameObject.SetActive (false);
		Destroy (stage);
		//New Stage
		stageNum = 1;
		stage = GameObject.Instantiate (stageList [stageNum - 1]);
		dead = false;
		numRemainBird = initBird;
		CreateBird ();
	}

	public void Stage2(){
		//Destroy
		particlesystem.gameObject.SetActive (false);
		clear.gameObject.SetActive (false);
		Destroy (stage);
		//New Stage
		stageNum = 2;
		stage = GameObject.Instantiate (stageList[stageNum-1]);
		dead = false;
		numRemainBird = initBird;
		CreateBird ();
	}

	public void Dead(Transform trans){
		deadAudio.Play ();
		dead = true;
		mainCamera.GetComponent<CameraController> ().target = trans;
		particlesystem.gameObject.SetActive (true);
		particlesystem.transform.position = trans.position;
		clear.gameObject.SetActive (true);
	}
}

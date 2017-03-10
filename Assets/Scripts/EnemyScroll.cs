using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScroll : MonoBehaviour {

	public float pipeVelocity;
	public bool shoted = false;
	public bool attack = false;
	public Transform enemyRespawnRight;
	bool inFirst = false;
	public Animator animator;



	// Use this for initialization
	void Start () {

		animator =  GetComponent<Animator> ();

		enemyRespawnRight = GameObject.Find ("EnemyRespawnRight").transform;
	}

	IEnumerator FinishFirt (float waitTime){

		Debug.Log ("Parou");


		inFirst = true;
		yield return new WaitForSeconds(waitTime);
		inFirst = false;

		Debug.Log ("Continuou");

	}

	IEnumerator DoLast(){
	
		while (inFirst)
			yield return new WaitForSeconds (0.1f);

		transform.position = new Vector3(enemyRespawnRight.position.x, enemyRespawnRight.position.y, enemyRespawnRight.position.z) ;

		shoted = false;
		animator.SetTrigger ("Enemy1Alive");

	}

	// Update is called once per frame
	void Update () {

		if (!shoted ) {
			transform.position += Vector3.left * pipeVelocity * Time.deltaTime;
		}


		if (shoted) {
			Debug.Log ("Shoted");

			StartCoroutine(FinishFirt (1.6f));
			StartCoroutine(DoLast ());

		}

	}




}

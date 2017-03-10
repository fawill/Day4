using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	[Header("References")]
	public EnemyScroll pipePrefab;
//	public Transform enemyRespawnRight;

	[Header("Pipes")]
	public int maxPipes;
	public float pipesOffSet;
	private int firstPipeIndex;
	private float worldOffSet;

	[Header("Flappy Bird States")]
	public bool isDead = false;

	private Transform[] pipes;

	// Use this for initialization
	void Start () {

		pipes = new Transform[maxPipes];


		worldOffSet = (Camera.main.ViewportToWorldPoint (
			new Vector3 (pipesOffSet, 0f, 0f)
		)
			- 
			(Camera.main.ViewportToWorldPoint(
				new Vector3(0f, 0f, 0f))
			)
		)
			.x;

		Vector3 pipePosition;

		for (int i = 0; i < maxPipes; i++) {
			//Encontra a posicao de cada pipe
			pipePosition = Camera.main.ViewportToWorldPoint (new Vector3 ((1f + pipesOffSet * i), 0f, 0f));

			pipePosition.z = 0f;

			//Criar cada pipe
			pipes [i] = Instantiate(pipePrefab.transform) as Transform;
			pipes [i].parent = transform;
			pipes [i].localPosition = pipePosition;

		}

		firstPipeIndex = 0;

	}
	
	// Update is called once per frame
	void Update () {

		//if(isDead){

		//	return;
		//}

		Vector3 screenPos = Camera.main.WorldToViewportPoint (
			                    pipes [firstPipeIndex].position);

		/*
		if ( (pipePrefab.GetComponent<EnemyScroll>().shoted  )) {

			Debug.Log ("positon reset");

			//index da posicao anterior
			int lastPipeIndex = (pipes.Length + firstPipeIndex - 1) % pipes.Length;

			//posiciona atras do ultimo pipe
			//pipes [firstPipeIndex].localPosition = pipes [lastPipeIndex].localPosition + new Vector3(worldOffSet, 0f, 0f);

			pipePrefab.GetComponent<EnemyScroll> ().shoted = false;
			pipePrefab.GetComponent<EnemyScroll> ().attack = false;

			pipes [firstPipeIndex].position = new Vector3(enemyRespawnRight.position.x, enemyRespawnRight.position.y, enemyRespawnRight.position.z) ;



			//Define um posicao aleatoria no eixo y
		//	Vector3 pipesPos = pipes [firstPipeIndex].localPosition;
		//	pipesPos.y = (Camera.main.ViewportToWorldPoint ( new Vector3(0f, 0f, 0f))).y;

		//	pipes [firstPipeIndex].localPosition = pipesPos;

			/*

			pipes [firstPipeIndex].localPosition.y = Camera.main.ViewportToWorldPoint (
													new Vector3 (0f, Random.Range (-0.25f, 0.25f), 0f)
												).y;
*/

			//atualiza index do primeiro elemento
			//firstPipeIndex = (firstPipeIndex + 1) % pipes.Length;
		
//		}
	


	}

	void OnCollisionEnter2D(Collision2D collision){
		//Se o pipe entao mata o flappybird

		if(collision.collider.CompareTag("Pipe")){
			//Diiiiiiie!!!!
			isDead = true;

			//Deixa de colidir
			GetComponent<Collider2D> ().isTrigger = true;


		}

	}





}

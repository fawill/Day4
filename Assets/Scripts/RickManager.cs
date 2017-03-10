using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RickManager : MonoBehaviour {


	public Transform shotBeginRight;
	public Transform shotCheckRight;

	public Transform shotBeginLeft;
	public Transform shotCheckLeft;

	public Transform rickBody;

	[Header("Rick Behavior")]
	public bool isFacingLeft = false;

	[Header("Components")]
	public Rigidbody2D rigidbody2D ;
	public SpriteRenderer spriterenderer;

	public Animator animator;

	private ScoreManager scoreManager;
	public GameObject gameOver;

	// Use this for initialization
	void Start () {
	
		scoreManager = GameObject.FindObjectOfType<ScoreManager> ();
		animator = GetComponent<Animator> ();
		spriterenderer = GetComponent<SpriteRenderer> ();
		rigidbody2D = GetComponent<Rigidbody2D> ();

	}
	
	// Update is called once per frame
	void Update () {
		HandleHorizontalMovement ();

		if (RaycastAgainstLayerRick ("Enemy", rickBody)) {
			Debug.Log ("Rick is Dead");

			gameOver.SetActive(true);


		}

	}

	void HandleHorizontalMovement(){


		if (Input.GetMouseButtonDown(0)) {
			animator.SetTrigger ("RickShot"); 

			if (isFacingLeft) {
				if (RaycastAgainstLayer ("Enemy", shotCheckRight)) {
					Debug.Log ("acertou");
				}
			} else {
				if (RaycastAgainstLayer ("Enemy", shotCheckLeft)) {
					Debug.Log ("acertou");
				}
			}

			isFacingLeft = !isFacingLeft;
		}

		spriterenderer.flipX = isFacingLeft;

	}


	RaycastHit2D RaycastAgainstLayerRick(string layerName, Transform endPoint){
		// layer 00000000000000000000000000001001

		int layer = LayerMask.NameToLayer (layerName); //camada 1, camada 2, 3..
		int layerMask = 1 << layer; // camada 2 -> 100, camada 4 -> 10000

		// eg. camadas 2, 4 = (1 << 2) + (1 << 4) = 100 + 10000 = 10100

		Vector2 originPosition =  new Vector2 (transform.position.x, transform.position.y);

		Vector2 direction = endPoint.localPosition.normalized;

		float rayLength = Mathf.Abs(endPoint.localPosition.y);

		RaycastHit2D hit2d = Physics2D.Raycast(originPosition, direction, 20*rayLength, layerMask);

		#if UNITY_EDITOR

		Color color;

		if(hit2d != null && hit2d.collider != null) {

			color = Color.green;
		}else{

			color = Color.red;
		}

		Debug.DrawLine(originPosition, //Inicio
			originPosition + direction*rayLength*20, //fim
			color, 0f, false); //cor

		#endif

		if (hit2d != null  && hit2d.collider != null && hit2d.collider.gameObject != null) {
			GameObject enemy1 = hit2d.collider.gameObject;
			//enemy1.GetComponent<Animator> ().SetTrigger ("Enemy1Death");
			enemy1.GetComponent<EnemyScroll> ().shoted = true;
		}

		return hit2d;
	}


	RaycastHit2D RaycastAgainstLayer(string layerName, Transform endPoint){
		// layer 00000000000000000000000000001001

		int layer = LayerMask.NameToLayer (layerName); //camada 1, camada 2, 3..
		int layerMask = 1 << layer; // camada 2 -> 100, camada 4 -> 10000

		// eg. camadas 2, 4 = (1 << 2) + (1 << 4) = 100 + 10000 = 10100

		Vector2 originPosition = new Vector2 (0f, 0f);


		if (isFacingLeft) {
			originPosition = new Vector2 (shotBeginRight.position.x, shotBeginRight.position.y);
		} else {
			originPosition = new Vector2 (shotBeginLeft.position.x, shotBeginLeft.position.y);
		}



		Vector2 direction = endPoint.localPosition.normalized;

		float rayLength = Mathf.Abs(endPoint.localPosition.y);

		RaycastHit2D hit2d = Physics2D.Raycast(originPosition, direction, 50*rayLength, layerMask);

		#if UNITY_EDITOR

		Color color;

		if(hit2d != null && hit2d.collider != null) {

			color = Color.green;
		}else{

			color = Color.red;
		}

		Debug.DrawLine(originPosition, //Inicio
			originPosition + direction*rayLength*50, //fim
			color, 0f, false); //cor

		#endif

		if (hit2d != null  && hit2d.collider != null && hit2d.collider.gameObject != null) {
			
			GameObject enemy1 = hit2d.collider.gameObject;
			enemy1.GetComponent<Animator> ().SetTrigger ("Enemy1Death");

			if(enemy1.GetComponent<EnemyScroll> ().shoted == false){
				scoreManager.AddScore (1);
			}

			enemy1.GetComponent<EnemyScroll> ().shoted = true;



		}

		return hit2d;
	}

}

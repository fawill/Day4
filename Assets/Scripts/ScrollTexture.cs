using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollTexture : MonoBehaviour {

	[Header ("Scene References")]
	public Material material;

	[Header("Velocity")]
	public Vector2 scrollVelocity;

	private Vector2 scrollOffSet;

	void Awake(){

	}

	// Use this for initialization
	void Start () {

		scrollOffSet = Vector2.zero;

		material.mainTextureOffset = scrollOffSet;
		//material.SetTextureOffSet ("_MainTex", scrollOffSet);
	
	}
	
	// Update is called once per frame
	void Update () {
		scrollOffSet += scrollVelocity * Time.deltaTime;
		material.mainTextureOffset = scrollOffSet;
	}


	void LateUpdate(){

	}

	void FixedUpdate(){


	}

}

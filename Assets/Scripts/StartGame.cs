using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour {

	// Use this for initialization
	public void Play () {
		Application.LoadLevel("Main");

	}


	public void Reload () {
		Application.LoadLevel("Menu");

		Debug.Log ("aqui");

	}

}

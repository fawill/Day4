using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	private Text text;

	private int score;

	// Use this for initialization
	void Start () {

		text = GetComponent<Text> ();
		score = 0;

		text.text = "" + score;

	}
	
	public void AddScore(int points){

		score += points;
		text.text = "" + score;

	}



}

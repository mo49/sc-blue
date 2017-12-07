using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour {

	ScoreManager scoreManager;
	Text ResultStateText;
	Text ResultScoreText;

	int playerScore;
	int keeperScore;

	void Awake() {
		scoreManager = ScoreManager.Instance;

		ResultStateText = GameObject.Find("ResultStateText").GetComponent<Text>();
		ResultScoreText = GameObject.Find("ResultScoreText").GetComponent<Text>();
	}

	void DrawResult() {
		playerScore = scoreManager.getPlayerScore();
		keeperScore = scoreManager.getKeeperScore();
		DrawState();
		DrawScore();
	}

	void DrawState() {
		if(playerScore > keeperScore){
			ResultStateText.text = "You Win!";
			ResultStateText.color = new Color(1f,0f,0f);
		}
		if(playerScore < keeperScore){
			ResultStateText.text = "You Lose...";
			ResultStateText.color = new Color(0f,0f,1f);
		}
		if(playerScore == keeperScore){
			ResultStateText.text = "Draw...";
			ResultStateText.color = new Color(0f,0f,1f);
		}
	}

	void DrawScore() {
		ResultScoreText.text = playerScore.ToString() + " - " + keeperScore.ToString();
	}

	
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour {

	GameFlowManager gameFlowManager;
	ScoreManager scoreManager;
	LevelManager levelManager;
	ZoneManager zoneManager;

	Text LevelText;
	Text TurnStateText;
	Text PlayerScoreText;
	Text KeeperScoreText;

	int scoreRate;
	int currentPlayerScore;
	int currentKeeperScore;
	[SerializeField] int lastAttackRate = 4;

	void Awake() {
		gameFlowManager = GameFlowManager.Instance;
		scoreManager = ScoreManager.Instance;
		levelManager = LevelManager.Instance;
		zoneManager = ZoneManager.Instance;

		LevelText = GameObject.Find("LevelText").GetComponent<Text>();
		TurnStateText = GameObject.Find("TurnStateText").GetComponent<Text>();
		PlayerScoreText = GameObject.Find("PlayerScoreText").GetComponent<Text>();
		KeeperScoreText = GameObject.Find("KeeperScoreText").GetComponent<Text>();

		scoreManager.setScoreRateByLevel(levelManager.getLevel());
	}

	void Start() {
		DrawLevel();
		DrawTurn();
	}

	void DrawLevel() {
		int level = levelManager.getLevel();
		LevelText.text = "Level : " + levelManager.getLevelName();
	}

	void DrawTurn() {
		int leftTurn = gameFlowManager.getLeftTurn();
		TurnStateText.text = "Left Turn : " + leftTurn.ToString();
	}
	void UpdateTurn() {
		int currentTurn = gameFlowManager.getCurrentTurn();
		currentTurn++;
		gameFlowManager.setCurrentTurn(currentTurn);
		DrawTurn();
	}

	void UpdateScore(string type) {
		switch(type) {
			case "player":
				scoreRate = scoreManager.getPlayerScoreRate();
				CheckLastAttack();
				UpdatePlayerScore();
				break;
			case "keeper":
				scoreRate = scoreManager.getKeeperScoreRate();
				CheckLastAttack();
				UpdateKeeperScore();
				break;
		}
	}
	void CheckLastAttack() {
		Debug.Log("gameFlowManager.IsLastAttack()" + gameFlowManager.IsLastAttack());
		if(gameFlowManager.IsLastAttack()){
			scoreRate *= lastAttackRate;
		}
	}
	void UpdatePlayerScore() {
		currentPlayerScore = scoreManager.getPlayerScore();
		currentPlayerScore += scoreRate;
		scoreManager.setPlayerScore(currentPlayerScore);
		PlayerScoreText.text = "Player Score\n" + currentPlayerScore;
	}
	void UpdateKeeperScore() {
		currentKeeperScore = scoreManager.getKeeperScore();
		currentKeeperScore += scoreRate;
		scoreManager.setKeeperScore(currentKeeperScore);
		KeeperScoreText.text = "Keeper Score\n" + currentKeeperScore;
	}

}

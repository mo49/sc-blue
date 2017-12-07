﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour {

	[SerializeField] GameObject Goal;
	[SerializeField] GameObject ExplosionPrefab;
	[SerializeField] GameObject BalloonPrefab;
	[SerializeField] GameObject FireworkPrefab;
	GameObject Result;
	GameObject ScoreBoard;

	Text ShootResultText;

	[SerializeField] int clearScore = 10;
	int currentPlayerScore;
	int currentKeeperScore;

	LevelManager levelManager;
	ScoreManager scoreManager;
	ZoneManager zoneManager;
	GameFlowManager gameFlowManager;

	bool isResulted = false;

	void Awake() {
		ScoreBoard = GameObject.Find("ScoreBoardGUI");		
		Result = GameObject.Find("Result");

		Goal = Instantiate(Goal) as GameObject;

		levelManager = LevelManager.Instance;
		scoreManager = ScoreManager.Instance;
		zoneManager = ZoneManager.Instance;
		gameFlowManager = GameFlowManager.Instance;

		ShootResultText = GameObject.Find("ShootResultText").GetComponent<Text>();
		ShootResultText.text = "";

		if(zoneManager.getZoneState()){
			transform.Find("PlasmaExplosionEffect").gameObject.SetActive(true);
		} else {
			StartCoroutine("CreateBalloon");
		}

	}

	void OnTriggerEnter(Collider other) {
		Debug.Log("OnTriggerEnter : " + other.tag);
		if(other.tag == "goal"){
			// goal, missに複数回通った場合初回のみ有効
			if(isResulted){
				return;
			}
			// player score
			addPlayerScore();

			// effect
			if(zoneManager.getZoneState()) {
				Instantiate(ExplosionPrefab, transform.position, transform.rotation);
			}

			// sound
			Goal.SendMessage("audienceSound", "goal");
		}
		if(other.tag == "miss"){
			if(isResulted){
				return;
			}
			// keeper score
			addKeeperScore();

			// sound
			Goal.SendMessage("audienceSound", "miss");
		}
		if(other.tag == "balloon") {
			intoZone();
		}
		if(other.tag == "keeper"){
			Debug.Log("keeper");
			// TODO: 何かアニメーションするとよい？
			other.SendMessage("guard");
		}
	}

	IEnumerator CreateBalloon() {
		yield return new WaitForSeconds(0.5f);
		Debug.Log("transform.position.x" + transform.position.x);
		if(transform.position.x > 0){
			// right
			Instantiate(BalloonPrefab, new Vector3(7f, -2f, 28f), Quaternion.Euler(90f,0f,0f));
		} else {
			// left
			Instantiate(BalloonPrefab, new Vector3(-7f, -2f, 28f), Quaternion.Euler(90f,0f,0f));
		}
	}

	void addPlayerScore() {
		Debug.Log("goal!!!");

		ScoreBoard.SendMessage("UpdateScore", "player");

		UpdateTurn("goal");
		isResulted = true;
	}

	void addKeeperScore() {
		Debug.Log("miss...");

		ScoreBoard.SendMessage("UpdateScore", "keeper");

		UpdateTurn("miss");
		isResulted = true;
	}

	void intoZone() {
		Debug.Log("into Zone!!!");

		zoneManager.setZoneState(true);

		UpdateTurn("zone");
		isResulted = true;
	}

	void UpdateTurn(string type) {
		// 正常に処理が終わったフラグを立てる
		gameFlowManager.setEntireShootFlowState(true);

		ScoreBoard.SendMessage("UpdateTurn");

		StartCoroutine("ShowShootResult", type);

		if(gameFlowManager.getLeftTurn() <= 0){
			Debug.Log("fin");
			gameFlowManager.setGameFinishState(true);
		}
	}

	IEnumerator ShowShootResult(string type) {
		ShootResultText.enabled = true;
		switch (type) {
			case "goal":
				ShootResultText.text = "Gooooal!!!";
				ShootResultText.color = new Color(1f,0f,0f);
				break;
			case "miss":
				ShootResultText.text = "Miss...";
				ShootResultText.color = new Color(0f,0f,1f);
				break;
			case "zone":
				ShootResultText.text = "ZONE";
				ShootResultText.color = new Color(1f,0f,1f);
				break;
		}
		yield return new WaitForSeconds(3f);

		// 終了なら結果画面へ
		if(gameFlowManager.getGameFinishState()) {
			ShootResultText.text = "";
			yield return new WaitForSeconds(2f);
			Result.SendMessage("DrawResult");
			// 高得点演出
			if(scoreManager.getPlayerScore() >= 20) {
				Firework();
			}
			Remove();
			yield break;
		}

		// 次のコーナーキックへ処理を渡す
		gameFlowManager.setCornerKickState(true);

		Remove();
	}

	void Firework() {
		for (int i = 0; i < 5; i++){
			Instantiate(
				FireworkPrefab,
				new Vector3(Random.Range(-20f,20f), 0f, Random.Range(30f,45f)),
				Quaternion.Euler(-90f,0f,0f)
			);
			//firework.GetComponent<ParticleSystem>().Play();
		}
	}

	void Remove() {
		// いろいろ破棄
		ShootResultText.text = "";
		Destroy(GameObject.Find("Explosion(Clone)"));
		Destroy(GameObject.Find("Eff_Heal_1_oneShot(Clone)"));
		Destroy(GameObject.Find("Balloon(Clone)"));
		Destroy(gameObject);
	}
}
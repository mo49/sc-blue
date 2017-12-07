using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	static ScoreManager m_Instance;
	public static int playerScore = 0;
	public static int keeperScore = 0;

	public static int scoreRate = 1;
	public static int zoneRate = 1;
	public static int levelRate = 1;

	public static ScoreManager Instance {
		get {
			if(m_Instance == null){
				GameObject obj = new GameObject ("ScoreManager");
				m_Instance = obj.AddComponent<ScoreManager> ();
			}
			return m_Instance;
		}
	}
		
	public void setPlayerScore(int num) {
		playerScore = num;
		Debug.Log("add playerScore : " + playerScore);
	}
	public int getPlayerScore() {
		return playerScore;
	}
	public void setKeeperScore(int num) {
		keeperScore = num;
		Debug.Log("add keeperScore : " + keeperScore);
	}
	public int getKeeperScore() {
		return keeperScore;
	}

	public void setScoreRateByZone(int rate) {
		zoneRate = rate;
	}
	public int getScoreRateByZone() {
		return zoneRate;
	}

	public void setScoreRateByLevel(int rate) {
		levelRate = rate;
	}
	public int getScoreRateByLevel() {
		return levelRate;
	}

	public int getPlayerScoreRate() {
		return getScoreRateByLevel() * getScoreRateByZone();
	}
	public int getKeeperScoreRate() {
		return getScoreRateByLevel();
	}

}

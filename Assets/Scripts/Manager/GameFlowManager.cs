using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ゲームのフローにおけるフラグ・ターン数を管理する
 */

public class GameFlowManager : MonoBehaviour {

	private const int TURN_NUM = 5;

	static GameFlowManager m_Instance;
	public static int currentTurn = 0;
	public static bool canKickIn = true;
	public static bool isEntireShootFlow = false;
	public static bool isCornerKickAuto = false;
	public static bool isGameFinished = false;

	public static GameFlowManager Instance {
		get {
			if(m_Instance == null){
				GameObject obj = new GameObject ("GameFlowManager");
				m_Instance = obj.AddComponent<GameFlowManager> ();
			}
			return m_Instance;
		}
	}

	public int getLeftTurn() {
		// 残りのターン数
		return TURN_NUM - currentTurn;
	}
	public int getCurrentTurn() {
		return currentTurn;
	}	
	public void setCurrentTurn(int num) {
		currentTurn = num;
		Debug.Log("set currentTurn : " + currentTurn);
	}

	// コーナーキックをうてるかどうか
	public bool getCornerKickState() {
		return canKickIn;
	}
	public void setCornerKickState(bool boolean){
		canKickIn = boolean;
	}

	// コーナーキックが手動 or 自動
	public bool getCornerKickAutoState() {
		return isCornerKickAuto;
	}
	public void setCornerKickAutoState(bool boolean){
		isCornerKickAuto = boolean;
	}

	// コーナーキックから goal or miss までの流れが完了
	public bool getEntireShootFlowState() {
		return isEntireShootFlow;
	}
	public void setEntireShootFlowState(bool boolean){
		isEntireShootFlow = boolean;
	}

	// ゲームが終了しているかどうか
	public bool getGameFinishState() {
		return isGameFinished;
	}
	public void setGameFinishState(bool boolean){
		isGameFinished = boolean;
	}

	// ラストターン
	public bool IsLastAttack() {
		Debug.Log("getCurrentTurn()" + getCurrentTurn());
		return (TURN_NUM == getCurrentTurn() + 1);
	}

}

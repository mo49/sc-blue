﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	LevelManager levelManager;
	[SerializeField] GameObject KeeperPrefab;
	int keeperNum = 1;
	int firingAngle = 45;
	float cameraDistance;
	bool goalMoving = false;

	GameObject Goal;

	void Start() {
		Goal = GameObject.Find("Goal");

		levelManager = LevelManager.Instance;
		int currentLevel = levelManager.getLevel ();
		setDifficulityByLevel (currentLevel);
	}

	void setDifficulityByLevel(int level) {
		switch(level) {
		case 1:
			firingAngle = 45;
			cameraDistance = 20f;
			keeperNum = 1;
			goalMoving = false;
			break;
		case 2:
			firingAngle = 40;
			cameraDistance = 19f;
			keeperNum = 2;
			goalMoving = false;
			break;
		case 3:
			firingAngle = 35;
			cameraDistance = 18f;
			keeperNum = 3;
			goalMoving = false;
			break;
		case 4:
			firingAngle = 30;
			cameraDistance = 15f;
			keeperNum = 3;
			goalMoving = true;
			break;
		}

		GameObject.Find("CornerKick").GetComponent<CornerKick> ().firingAngle = firingAngle;
		GameObject.Find ("Fove Rig").transform.position = new Vector3 (0, 1.4f, cameraDistance);
		Goal.GetComponent<GoalMove>().enabled = goalMoving;

		for (int i = 0; i < keeperNum; i++) {
			createKeeper(i+1);
		}
	}

	void createKeeper(int index) {
		float x = 0f;
		switch(index){
			case 1: 
				x = 0f; 
				break;
			case 2: 
				x = -1.5f; 
				break;
			case 3:
				x = 1.5f; 
				break;
		}
		Vector3 vel = new Vector3(x, 0f, 30f);
		var keeperInstance = Instantiate(KeeperPrefab, vel, Quaternion.Euler(0f,180f,0f));

		// goalの子として追加
		keeperInstance.transform.parent = Goal.transform;
	}
}

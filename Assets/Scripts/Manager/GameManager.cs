using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	LevelManager levelManager;
	[SerializeField] GameObject KeeperPrefab;
	int keeperNum = 1;
	int firingAngle;
	float cameraDistance;

	void Start() {
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
			break;
		case 2:
			firingAngle = 30;
			cameraDistance = 18f;
			keeperNum = 2;
			break;
		case 3:
			firingAngle = 15;
			cameraDistance = 15f;
			keeperNum = 3;
			break;
		}

		GameObject.Find("CornerKick").GetComponent<CornerKick> ().firingAngle = firingAngle;
		GameObject.Find ("Fove Rig").transform.position = new Vector3 (0, 1.4f, cameraDistance);

		for (int i = 0; i < level; i++) {
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
		Instantiate(KeeperPrefab, vel, Quaternion.Euler(0f,180f,0f));
	}
}

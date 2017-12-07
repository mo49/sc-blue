using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour {

	LevelManager levelManager;

	void Start() {
		levelManager = LevelManager.Instance;
	}
	public void LevelOnClick(int num) {
		SceneNavigator.Instance.Change("Main");
		levelManager.setLevel(num);
	}
}

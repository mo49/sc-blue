using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	static LevelManager m_Instance;
	public static int level = 1;
	public static string name;

	public static LevelManager Instance {
		get {
			if(m_Instance == null){
				GameObject obj = new GameObject ("LevelManager");
				m_Instance = obj.AddComponent<LevelManager> ();
			}
			return m_Instance;
		}
	}
		
	public void setLevel(int num) {
		level = num;
		Debug.Log("set level : " + level);
	}
	public int getLevel() {
		return level;
	}
	public string getLevelName() {
		switch(getLevel()) {
			case 1: name = "Easy"; break;
			case 2: name = "Normal"; break;
			case 3: name = "Hard"; break;
			case 4: name = "Super"; break;
		}
		return name;
	}

}

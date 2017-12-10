using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keeper : MonoBehaviour {

	GameObject m_Ball;

	void Awake() {

	}

	void Update() {
		if(m_Ball == null){
			return;
		}
		transform.LookAt(m_Ball.transform);
		Debug.Log(m_Ball.transform);
	}

	public void SetBallTransform(GameObject m_Ball) {
		Debug.Log(m_Ball);
	}

	void MoveToBall() {
		
	}

}

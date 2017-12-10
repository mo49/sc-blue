using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalMove : MonoBehaviour {
	[SerializeField] float speed = 0.5f;

	[SerializeField] float moveRange = 10f;

	float time;

	void Update() {
		Vector3 pos = Camera.main.transform.position;
		float vx = pos.x + Mathf.Cos(time * speed) * moveRange;
		float vz = pos.z + Mathf.Abs( Mathf.Sin(time * speed) * moveRange ); // プレイヤーの背後に回り込ませない
		transform.position = new Vector3(
			vx,
			0f,
			vz
		);
		Vector3 cameraPosition = new Vector3(
			0f,
			Camera.main.transform.position.y,
			Camera.main.transform.position.z
		);
		transform.LookAt(cameraPosition);

		time += Time.deltaTime;
	}
}

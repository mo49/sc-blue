using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fove;

public class PlayerController : MonoBehaviour {

	[SerializeField] float power = 20f;

	SFVR_Vec3 accel;

	GameFlowManager gameFlowManager;

	void Awake() {
		gameFlowManager = GameFlowManager.Instance;
	}

	void Update () {
		accel = FoveInterface.GetLastPose().acceleration;
	}

	void OnTriggerEnter(Collider other){
		gameFlowManager.setEntireShootFlowState(false);

		Rigidbody rb = other.GetComponent<Rigidbody> ();
		//Vector3 vel = (other.transform.position - transform.position).normalized * power;
		rb.useGravity = true;
		rb.AddForce (
			transform.forward * power * Mathf.Clamp(Mathf.Abs(accel.z), 1f, 2f),
			ForceMode.VelocityChange
		);

		if(other.tag == "ball") {
			StartCoroutine("AutoShowResult", other);
		}
	}

	IEnumerator AutoShowResult(Collider other) {
		yield return new WaitForSeconds(5);

		if(gameFlowManager.getEntireShootFlowState()){
			yield break;
		}

		// どこにも当たらなかった場合
		other.SendMessage("addKeeperScore");
	}
}
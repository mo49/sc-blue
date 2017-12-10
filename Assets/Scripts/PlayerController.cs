using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fove;

public class PlayerController : MonoBehaviour {

	[SerializeField] float power = 20f;
	[SerializeField] float zonePower = 30f;

	[SerializeField] float deactivateKeeperPercent = 0.8f;

	SFVR_Vec3 accel;

	GameFlowManager gameFlowManager;
	ZoneManager zoneManager;

	[SerializeField] AudioClip HeadingAudio;
	AudioSource audio;

	void Awake() {
		gameFlowManager = GameFlowManager.Instance;
		zoneManager = ZoneManager.Instance;
		audio = GetComponent<AudioSource>();
	}

	void Update () {
		accel = FoveInterface.GetLastPose().acceleration;
	}

	void OnTriggerEnter(Collider other){
		gameFlowManager.setEntireShootFlowState(false);

		ActivateKeeper();

		if(zoneManager.getZoneState())
			power = zonePower;

		Rigidbody rb = other.GetComponent<Rigidbody> ();
		//Vector3 vel = (other.transform.position - transform.position).normalized * power;
		rb.useGravity = true;
		rb.AddForce (
			transform.forward * power * Mathf.Clamp(Mathf.Abs(accel.z), 1f, 20f),
			ForceMode.VelocityChange
		);

		if(other.tag == "ball") {
			audio.PlayOneShot(HeadingAudio, 0.3f);
			if(zoneManager.getZoneState())
				DeactivateKeeper();
		}
	}

	void ActivateKeeper() {
		GameObject[] keepers = GameObject.FindGameObjectsWithTag("keeper");
		foreach (var keeper in keepers) {
			keeper.GetComponent<CapsuleCollider>().enabled = true;
		}
	}
	void DeactivateKeeper() {
		GameObject[] keepers = GameObject.FindGameObjectsWithTag("keeper");
		foreach (var keeper in keepers) {
			Debug.Log(deactivateKeeperPercent - Random.value);
			if(deactivateKeeperPercent - Random.value > 0f){
				keeper.GetComponent<CapsuleCollider>().enabled = false;
			}
		}
	}

}
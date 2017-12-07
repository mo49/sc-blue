using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerKick : MonoBehaviour {

	[SerializeField] bool isAuto;
	[SerializeField] Transform m_Target;
	[SerializeField] Transform m_KickerTransform;
	[SerializeField] GameObject m_ProjectilePrefab;
	public float delayShoot = 1f;
	public float gravity = 9.8f;
	public float firingAngle = 45.0f;

	GameFlowManager gameFlowManager;

	AudioSource audio;
	public AudioClip KickinAudio;

	void Awake() {
		m_KickerTransform = transform;
		gameFlowManager = GameFlowManager.Instance;
		gameFlowManager.setCornerKickAutoState(isAuto);
		audio = GetComponent<AudioSource>();
	}

	void Update () {
		if(!gameFlowManager.getCornerKickState()){
			return;
		}
		if(gameFlowManager.getCornerKickAutoState()){
			Invoke("RandomShoot", Random.Range(3f,8f));
			gameFlowManager.setCornerKickState(false);
		} else {
			if(Input.GetKeyDown(KeyCode.R)) {
				RightShoot();
				gameFlowManager.setCornerKickState(false);
			}
			if(Input.GetKeyDown(KeyCode.L)) {
				LeftShoot();
				gameFlowManager.setCornerKickState(false);
			}
		}
	}

	void RightShoot() {
		m_KickerTransform.position = new Vector3(17f, 0, 32f);
		StartCoroutine ("Shoot");
	}
	void LeftShoot() {
		m_KickerTransform.position = new Vector3(-17f, 0, 32f);
		StartCoroutine ("Shoot");
	}

	void RandomShoot() {
		if(Random.Range(0f,1f) > 0.5f){
			RightShoot();
		} else {
			LeftShoot();
		}
	}

	IEnumerator Shoot() {
		audio.PlayOneShot(KickinAudio, 1f);

		Transform m_ProjectileTransform = Instantiate (
			m_ProjectilePrefab,
			m_KickerTransform.position + new Vector3 (0f,0f,0f),
			Quaternion.identity
		).transform;

		yield return new WaitForSeconds(delayShoot);

		float target_distance = Vector3.Distance (m_ProjectileTransform.position, m_Target.position);
		float projectile_velocity = target_distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

		float vx = Mathf.Sqrt (projectile_velocity) * Mathf.Cos (firingAngle * Mathf.Deg2Rad);
		float vy = Mathf.Sqrt (projectile_velocity) * Mathf.Sin (firingAngle * Mathf.Deg2Rad);

		float flight_duration = target_distance / vx;

		m_ProjectileTransform.rotation = Quaternion.LookRotation (m_Target.position - m_ProjectileTransform.position);

		float elapsed_time = 0;

		while(elapsed_time < flight_duration){
			m_ProjectileTransform.Translate (
				0f,
				(vy - (gravity * elapsed_time)) * Time.deltaTime,
				vx * Time.deltaTime
			);
			elapsed_time += Time.deltaTime;
			yield return null;
		}
	}
}

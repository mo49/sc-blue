using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour {

	[SerializeField] GameObject AuraEffectPrefab;
	[SerializeField] GameObject BalloonEffectPrefab;
	[SerializeField] GameObject DustStormPrefab;
	[SerializeField] GameObject TorchsPrefab;
	[SerializeField] int ZoneRate = 2;

	public AudioClip BalloonBreakAudio;
	public AudioClip BalloonGlitterAudio;
	AudioSource audio;

	public Material NightSkyMaterial;

	ScoreManager scoreManager;

	void Awake() {
		scoreManager = ScoreManager.Instance;
		audio = GetComponent<AudioSource>();
	}

	void OnTriggerEnter(Collider other) {
		if(other.tag == "ball"){
			StartCoroutine("BreakBalloon");
		}
	}

	IEnumerator BreakBalloon() {
		scoreManager.setScoreRateByZone(ZoneRate);
		Instantiate(BalloonEffectPrefab, transform.position, transform.rotation);
		Instantiate(DustStormPrefab, Vector3.zero, Quaternion.identity);			
		Instantiate(AuraEffectPrefab, Camera.main.transform.position, Quaternion.identity);			
		Instantiate(TorchsPrefab,  Vector3.zero, Quaternion.identity);			
		ChangeSkybox();

		audio.PlayOneShot(BalloonBreakAudio, 1f);

		// 隠す！
		transform.position = new Vector3(transform.position.x, -25f, transform.position.z);

		yield return new WaitForSeconds(1f);

		audio.PlayOneShot(BalloonGlitterAudio, 1f);

		yield return new WaitForSeconds(10f);

		Destroy(gameObject);
	}

	void ChangeSkybox() {
		RenderSettings.skybox = NightSkyMaterial;
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Goal : MonoBehaviour {

	public AudioClip AudienceGoal;
	public AudioClip AudienceMiss;
	AudioSource audio;

	void Awake() {
		audio = GetComponent<AudioSource>();
	}
		
	void audienceSound(string type) {
		Debug.Log("audience audience  sound");
		switch(type) {
			case "goal":
				audio.PlayOneShot(AudienceGoal, 1.0f);
				break;
			case "miss":
				audio.PlayOneShot(AudienceMiss, 1.0f);
				break;
		}
	}

}
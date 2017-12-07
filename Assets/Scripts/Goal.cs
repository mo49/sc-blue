using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Goal : MonoBehaviour {

	public AudioClip AudienceGoalAudio;
	public AudioClip AudienceMissAudio;
	public AudioClip BallBombAudio;
	AudioSource audio;

	void Awake() {
		audio = GetComponent<AudioSource>();
	}

	void ballBambSound() {
		audio.PlayOneShot(BallBombAudio, 1.0f);
	}
		
	void audienceSound(string type) {
		switch(type) {
			case "goal":
				audio.PlayOneShot(AudienceGoalAudio, 1.0f);
				break;
			case "miss":
				audio.PlayOneShot(AudienceMissAudio, 1.0f);
				break;
		}
	}

}
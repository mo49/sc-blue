using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public AudioClip FireworksAudio;
	public AudioClip EndWhistleAudio;

	AudioSource audio;

	void Awake() {
		audio = GetComponent<AudioSource>();
	}

	public void PlayFireworksSound() {
		audio.PlayOneShot(FireworksAudio, 1f);
	}

	public void PlayEndWhistleSound() {
		audio.PlayOneShot(EndWhistleAudio, 1f);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public AudioClip FireworksAudio;

	AudioSource audio;

	void Awake() {
		audio = GetComponent<AudioSource>();
	}

	void FireworksSound() {
		audio.PlayOneShot(FireworksAudio, 1f);
	}
}

using UnityEngine;
using System.Collections;

public class playSound : MonoBehaviour {

	private AudioSource audio{get{ return GetComponent<AudioSource> ();}}

	void Awake(){
		DontDestroyOnLoad (gameObject);
	}
	
	public void PlaySound(){
		if (audio != null) {
			audio.Play ();
		}
	}
}

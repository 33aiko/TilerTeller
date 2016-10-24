using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {

	private AudioSource audio{get{return GetComponent<AudioSource> ();}}


	public void startGame(){
		Application.LoadLevel (3);

	}



	public void continueGame(){
		Application.LoadLevel (4);
	}

	public void playSound(){
		AudioSource audio = GameObject.Find ("PlayButtonSound").GetComponent<AudioSource>();
		if (audio != null) {
			audio.Play ();
		}
	}
		
}

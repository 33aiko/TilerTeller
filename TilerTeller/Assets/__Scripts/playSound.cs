using UnityEngine;
using System.Collections;

public class playSound : MonoBehaviour {

	public string wwiseEvent;

	//Commented to test Wwise Audio Engine Instead
	//private AudioSource audio{get{ return GetComponent<AudioSource> ();}}

	void Awake(){
		
		DontDestroyOnLoad (gameObject);
	}
	
	public void PlaySound(){
		//Commented to test Wwise Audio Engine Instead
		/*if (audio != null) {
			audio.Play ();
		}
		*/
//		AkSoundEngine.PostEvent (wwiseEvent,this.gameObject);
	}
}

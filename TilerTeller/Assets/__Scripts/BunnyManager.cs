using UnityEngine;
using System.Collections;

public class BunnyManager : MonoBehaviour {

	public GameObject whiteBunny;
	public GameObject whiteBunnyRed;
	public GameObject brownBunny;
	public float duration;

	public float timeInterval = 2;




	// Use this for initialization
	void Start () {
		
//		InvokeRepeating("SpawnBunny",0,timeInterval);
		StartCoroutine(m_Spawn());
	}
	
	// Update is called once per frame
	void Update () {
//		StartCoroutine (SpawnBunny (timeInterval));
	}

	IEnumerator m_Spawn(){
		int i = 0;
		while (true) {
			if (timeInterval > 3) {
				timeInterval = timeInterval - 0.1f * i;
			}
			yield return new WaitForSeconds (timeInterval);
			SpawnBunny ( duration - i*0.1f);
			i++;
		}
	}

	void SpawnBunny( float duration ){
		
		int bunnyType = Random.Range(0,6);
	
		if (bunnyType < 1) {
			GameObject brownBunnyClone = (GameObject)Instantiate (brownBunny, brownBunny.transform.position, brownBunny.transform.rotation);
			brownBunnyClone.GetComponent<BunnyMov> ().duration = duration;
		}
		if (bunnyType >= 1 && bunnyType < 3) {
			GameObject whiteBunnyClone = (GameObject)Instantiate (whiteBunny, whiteBunny.transform.position, whiteBunny.transform.rotation);
			whiteBunnyClone.GetComponent<BunnyMov> ().duration = duration;
		}
		if (bunnyType >= 3) {
			GameObject whiteBunnyRedClone = (GameObject)Instantiate (whiteBunnyRed, whiteBunnyRed.transform.position, whiteBunnyRed.transform.rotation);
			whiteBunnyRedClone.GetComponent<BunnyMov> ().duration = duration;
		}

//		yield return new WaitForSeconds (timeInterval);

	
	}

}	

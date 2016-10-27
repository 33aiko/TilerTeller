using UnityEngine;
using System.Collections;

public class BunnyManager : MonoBehaviour {

	public GameObject whiteBunny;
	public GameObject whiteBunnyRed;


	// Use this for initialization
	void Start () {
		Vector3 position = whiteBunny.transform.position;
		Quaternion rotation = whiteBunny.transform.rotation;

		GameObject whiteBunnyClone = (GameObject)Instantiate (whiteBunny, position, rotation);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

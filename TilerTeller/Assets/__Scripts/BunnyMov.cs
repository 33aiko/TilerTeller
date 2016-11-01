using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class BunnyMov : MonoBehaviour {

	public bool isRed = false;
	public Transform endMarker;
	public float duration = 1.0f;
	public Sprite[] lights;

	private Sequence bunnyMov;
	private	GameObject bunnyOpenEye;
	private GameObject bunnyCry;
	private GameObject bunnyBlackEye;
	private GameObject Light;


	private bool isClicked = false;

	private float openEyePos;


	void Start () {
		Light = GameObject.Find ("Light");
	
		openEyePos = (float)(1.5 - Random.Range (0, 1f));

		if (isRed) {
			bunnyOpenEye = transform.GetChild (2).gameObject;
			bunnyCry = transform.GetChild (1).gameObject;
			bunnyBlackEye = transform.GetChild (0).gameObject;
		}
		else{
			bunnyOpenEye = transform.GetChild (0).gameObject;
			bunnyCry = transform.GetChild (1).gameObject;
		}

		bunnyOpenEye.SetActive (false);
		bunnyCry.SetActive (false);
		if (bunnyBlackEye != null) {
			bunnyBlackEye.SetActive (false);
		}


		transform.DOMoveX (-8.55f, duration * 3).SetEase (Ease.Linear);

//		Debug.Log (openEyePos);
	}


	

	void Update () {

//		Debug.Log (transform.position.x);
		if (transform.position.x < openEyePos) {
			bunnyOpenEye.SetActive (true);
		}

		if (transform.position.x < 1.8 && transform.position.x > -2) {

			Vector3 rotateTo= Light.transform.rotation.eulerAngles + new Vector3 (0, 0, 180);
			Light.transform.DORotate (rotateTo, 3f);

			if (Input.GetMouseButtonDown (0)) {
				
				isClicked = true;
				if (isRed) {
					StartCoroutine (LightControl (1));
					bunnyBlackEye.SetActive (true);
				} else {
					StartCoroutine (LightControl (2));
					bunnyCry.SetActive (true);
				}
			}
		}

		if (transform.position.x < -2.2 && isRed && !isClicked) {
			bunnyCry.SetActive (true);
		}

		if (transform.position.x < -8.5f) {
			Destroy (gameObject);
		}
	}

	IEnumerator LightControl( int lightType){
		
		if (lights[lightType] != null) {
			Light.GetComponent<SpriteRenderer> ().sprite = lights[lightType]; }

		yield return new WaitForSeconds (2);
		Light.GetComponent<SpriteRenderer> ().sprite = lights[0];
	}


}

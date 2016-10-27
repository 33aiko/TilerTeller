using UnityEngine;
using System.Collections;
using DG.Tweening;

public class BunnyMov : MonoBehaviour {

	public bool isRed = false;
	public Transform endMarker;
	public float duration = 1.0f;

	private Sequence bunnyMov;
	private	GameObject bunnyOpenEye;
	private GameObject bunnyCry;
	private GameObject bunnyBlackEye;


	void Start () {
		

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

		StartCoroutine (startMove());


	}

	IEnumerator startMove(){
		bunnyMov = DOTween.Sequence ();
		bunnyMov.Append(transform.DOMoveX (1.84f, duration).SetEase(Ease.InOutSine));
		bunnyMov.Append(transform.DOMoveX (-2.36f, (duration * 1.2f)).SetEase (Ease.Linear));
		bunnyMov.Append(transform.DOMoveX(-8.31f,duration).SetEase(Ease.InOutSine));

		float timeOpen= duration + Random.Range(0,duration/2);

		yield return new WaitForSeconds (timeOpen);
	
		bunnyOpenEye.SetActive (true);

	}
	

	void Update () {
		if (transform.position.x > 1.84 && transform.position.x < -2.36) {
			if (Input.GetMouseButtonDown (0)) {
				if (isRed) {
					bunnyBlackEye.SetActive (true);
				} else {
					Debug.Log ("what?!");
					bunnyCry.SetActive (true);
				}
			}
		}

		
	}
}

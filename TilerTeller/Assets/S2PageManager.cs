using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class S2PageManager : MonoBehaviour {

	public GameObject[] pages;

	private int currentPage;
	private int bookLength;

	private GameObject lastBtn;
	private GameObject nextBtn;




	void Start () {

		lastBtn = GameObject.Find ("/Canvas/last");
		nextBtn = GameObject.Find ("/Canvas/next");

		bookLength = pages.Length;
		currentPage = 0;

	}



	void Update () {

		lastBtn.SetActive (true);
		nextBtn.SetActive (true);

		for (int i = 0; i < bookLength; i++) {
			if (i == currentPage && pages [i] != null) {
				pages [i].SetActive (true);
				if (pages [i].GetComponent<AudioSource> () != null && pages [i].GetComponent<AudioSource> ().isPlaying != true) {
					pages [i].GetComponent<AudioSource> ().Play ();
				}
			} else {
				pages [i].SetActive (false);
			}
		}

		if (currentPage == 0) {
			lastBtn.SetActive (false);
		}

		if (currentPage >= 1) {
			nextBtn.SetActive (false);
		}
	}


	public void turnNextPage(){
		gameObject.GetComponent<AudioSource> ().Play ();

		currentPage++;
	}

	public void turnLastPage(){
		gameObject.GetComponent<AudioSource> ().Play ();
		currentPage--;
	}

}

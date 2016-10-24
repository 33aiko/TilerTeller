using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
//using Giverspace;


public class pageManager : MonoBehaviour {


	public GameObject[] pages;
	public GameObject hintPage;
	public ArduinoManager arduino;
	public Animator hintDoor;
//	public MetricManager metric;


	private int bookLength;  //total number of pages
	private int pageCount;  //How many pages have been read
	private int currentPage;  //Current Page Number
	private bool isWaiting; //State: waiting for solving puzzle

	private int[] pageRead; //store the Page Numbers that has been read

	private GameObject lastBtn;
	private GameObject nextBtn;

	private int puzzleNum;  //Puzzle Number that has been solved
	private int lastPuzzleNum;

	private bool startAnim;

	private AudioSource[] hintSound;

	private float last_start_time;


	void Start () {

//		DateTime departure = new DateTime(2010, 6, 12, 18, 32, 0);
//		DateTime arrival = new DateTime(2010, 6, 13, 22, 47, 0);
//		TimeSpan travelTime = arrival - departure;  
//		Debug.Log("travelTime: " + travelTime );  

//		metric.AddToLevelAndTimeMetric ("Level1", 1.0f);
//		Log.Metrics.Message ("just a test!!!");
		lastBtn = GameObject.Find("/Canvas/last");
		nextBtn = GameObject.Find ("/Canvas/next");
		
		bookLength = pages.Length;
		pageCount = 0;
		currentPage = 0;
		isWaiting = false;

		pageRead = new int[bookLength];
		pageRead [0] = 0;

		puzzleNum = lastPuzzleNum = 0;

		startAnim = false;

		hintSound = hintPage.GetComponents<AudioSource> ();

		last_start_time = Time.time;

	}


	void Update () {

		lastBtn.SetActive (true);
		nextBtn.SetActive (true);

		if (arduino.isConnected) {
			puzzleNum = arduino.getPuzzleNum ();
		}



		/* waiting for players to solve puzzles */
		if (isWaiting) {
			/* Disabled buttons */
			nextBtn.SetActive (false);

			if (puzzleNum != lastPuzzleNum && puzzleNum != 0) {
				Debug.Log ("Open");
				startAnim = true;	
				hintDoor.SetTrigger ("Open");
				if (!hintSound[0].isPlaying) {
					hintSound[0].Play ();
				}
			}

			if (Input.GetKeyDown (KeyCode.Alpha1)) {
				startAnim = true;
				puzzleNum = 1;
			} 

			if (Input.GetKeyDown (KeyCode.Alpha2)) {
				startAnim = true;
				puzzleNum = 2;
			} 
			if (Input.GetKeyDown (KeyCode.Alpha3)) {
				startAnim = true;
				puzzleNum = 3;
			}

			if (startAnim) {
				
				if (hintDoor.GetCurrentAnimatorStateInfo (0).IsName ("Finished")) {
					Debug.Log ("Hello!");
					startAnim = false;
					gotoPage (puzzleNum);
					hintDoor.SetTrigger ("Close");

					string levelName = "Story1-Puzzle-Page" + currentPage;
					if (GameObject.Find ("MetricManager") != null) {
						MetricManager.Instance.AddToLevelAndTimeMetric (levelName, (Time.time - last_start_time));
					}
//					metric.AddToLevelAndTimeMetric (levelName, (Time.time - last_start_time));
					last_start_time = Time.time;
				}
			}


		}

		/* Display the current page */
		for (int i = 0; i < bookLength; i++) {
			if (i == currentPage && pages [i] != null && isWaiting != true) {
				pages [i].SetActive (true);
				if (pages [i].GetComponent<AudioSource> () != null && pages [i].GetComponent<AudioSource> ().isPlaying != true) {
					pages [i].GetComponent<AudioSource> ().Play ();}
			} else {
				pages[i].SetActive(false);
			}
		}
		if (currentPage == 0) {
			lastBtn.SetActive (false);
		}
		if (currentPage == bookLength - 1) {
			nextBtn.SetActive (false);
		}

	}

	/* When NextPage button is clicked */
	public void turnNextPage(){

		gameObject.GetComponent<AudioSource> ().Play ();

		string levelName = "Story1-Page" + currentPage;
		if (GameObject.Find ("MetricManager") != null) {
			MetricManager.Instance.AddToLevelAndTimeMetric (levelName, (Time.time - last_start_time));
		}
//		metric.AddToLevelAndTimeMetric (levelName, (Time.time - last_start_time));

		if (pageCount == bookLength - 2) {
			currentPage = bookLength - 1;
			pageRead [bookLength - 1] = currentPage;
			for (int j = 0; j < bookLength; j++) {
				Debug.Log (pageRead [j]);
			}

		} else if (pageRead [pageCount + 1] != 0) {
			currentPage = pageRead [pageCount + 1];
			pageCount++;
			return;
		}

		else {
			showHintPage ();
		}

		pageCount++;
		
	}

	public void turnLastPage(){
		gameObject.GetComponent<AudioSource> ().Play ();

		if (currentPage == 0) {
			return;
		} else {
			pageCount--;
			currentPage = pageRead [pageCount];
			hideHintPage ();
			isWaiting = false;
		}
		if (GameObject.Find ("MetricManager") != null) {
			MetricManager.Instance.AddToLevelAndTimeMetric ("Go back to last Page!", 0);
		}
//		metric.AddToLevelAndTimeMetric ("Go back to last Page!", 0);
		last_start_time = Time.time;
	}

	void showHintPage(){
		
		if (hintPage != null) {
			hintPage.SetActive (true);
			hintSound [1].Play ();
			Debug.Log ("next page");
			isWaiting = true;
		}

		last_start_time = Time.time;
	}

	void hideHintPage(){
		hintPage.SetActive (false);
	}

	/* Go to certain page */
	void gotoPage(int page){
			pageRead [pageCount] = page;
			currentPage = page;
			hideHintPage ();
			isWaiting = false;
			lastPuzzleNum = puzzleNum;
	}

}

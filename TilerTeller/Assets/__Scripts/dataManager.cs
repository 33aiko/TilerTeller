using UnityEngine;
using System.Collections;
using System;


public class dataManager : MonoBehaviour {

	public bleManager ble;

	[SerializeField] private int puzzleSolved;

	int[] buttonCounter = { 0, 0, 0, 0 };
	int[] buttonState = { 0, 0, 0, 0, 0 }; // 0,1,2,3 for star scene, 4 for bunny scene
	int[] lastButtonState = { 0, 0, 0, 0, 0}; // 0,1,2,3 for star scene, 4 for bunny scene


	bool bunnyIsPressed = false;


	void Update () {
		string s = ble.getdataReceived ();
		if (s != null) {
			string[] values = s.Split (new[]{ ',' }, StringSplitOptions.RemoveEmptyEntries);

			if (values [0] == "S2") {
				/* 0,1,2,3 are for star scene */
				for (int i = 0; i < 5; i++) {
					buttonState [i] = int.Parse (values [i + 1]);
					if (buttonState [i] != lastButtonState [i]) {
						if (buttonState [i] == 1) {
							if (i < 4) {
								buttonCounter [i]++;
							} else {
								bunnyIsPressed = true;
							}
						}
					}
					lastButtonState [i] = buttonState [i];
				}
				


			}

			if (values [0] == "S1") {

				int[] valueNum = new int[12];
				for (int i = 0; i < 12; i++) {
					//						Debug.Log(i+": "+ int.Parse(values[i+1]));
					valueNum [i] = int.Parse (values [i + 1]);
				}
				if (valueNum [0] < 3 && valueNum [1] < 3 && valueNum [2] < 3 && valueNum [3] < 3) {
					puzzleSolved = 3;
				} else if (valueNum [4] < 3 && valueNum [5] < 3 && valueNum [6] < 3 && valueNum [7] < 3) {
					puzzleSolved = 2;
				} else if (valueNum [8] < 3 && valueNum [9] < 3 && valueNum [10] < 3 && valueNum [11] < 3) {
					puzzleSolved = 1;
				} else
					puzzleSolved = 0;

			}

		}
	}

	public int[] getButtonCounter(){
		return buttonCounter;
	}

	public int getPuzzleNum(){
		return puzzleSolved;
	}

	public bool getBunnyIsPressed(){
		return bunnyIsPressed;
	}
	public void setBunnyIsPressed(){
		bunnyIsPressed = false;
	}
}

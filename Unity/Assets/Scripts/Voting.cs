using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class Voting : MonoBehaviour {
	[DllImport ("UniWii")]
	private static extern void wiimote_start();
	[DllImport ("UniWii")]
	private static extern void wiimote_stop();
	
	[DllImport ("UniWii")]
	private static extern int wiimote_count();
	[DllImport ("UniWii")]
	private static extern bool wiimote_available( int which );
	[DllImport ("UniWii")]
	private static extern bool wiimote_isIRenabled( int which );
	[DllImport ("UniWii")]	
	private static extern bool wiimote_enableIR( int which );
	
	[DllImport ("UniWii")]
	private static extern void wiimote_rumble( int which, float duration);
	[DllImport ("UniWii")]
	private static extern double wiimote_getBatteryLevel( int which );
	
	[DllImport ("UniWii")]
	private static extern byte wiimote_getAccX(int which);
	[DllImport ("UniWii")]
	private static extern byte wiimote_getAccY(int which);
	[DllImport ("UniWii")]
	private static extern byte wiimote_getAccZ(int which);
	
	[DllImport ("UniWii")]
	private static extern float wiimote_getIrX(int which);
	[DllImport ("UniWii")]
	private static extern float wiimote_getIrY(int which);
	[DllImport ("UniWii")]
	private static extern float wiimote_getRoll(int which);
	[DllImport ("UniWii")]
	private static extern float wiimote_getPitch(int which);
	[DllImport ("UniWii")]
	private static extern float wiimote_getYaw(int which);
	
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButtonA(int which);
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButtonB(int which);
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButtonUp(int which);
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButtonLeft(int which);
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButtonRight(int which);
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButtonDown(int which);
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButton1(int which);
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButton2(int which);
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButtonNunchuckC(int which);
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButtonNunchuckZ(int which);
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButtonPlus(int which);
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButtonMinus(int which);
	
	[DllImport ("UniWii")]	
	private static extern bool wiimote_setLed( int which );

	public SortedList<Player, int> votes;

	public GameObject mainLogic;

	public int numOfCandidates;

	private String display;
	
	// Use this for initialization
	void Start () {
		wiimote_start();
		
		Debug.Log ("VOTE!");
		mainLogic = GameObject.Find("MainLogic");
		numOfCandidates = mainLogic.GetComponent<MainLogic>().alivePlayers.Count;
		List<Player> candidates = mainLogic.GetComponent<MainLogic>().alivePlayers;
		votes = new SortedList<Player, int>(numOfCandidates);

		//foreach (Player candidate in candidates) {
			votes.Add(candidates[0], 0);
		//}
	}
	
	// Update is called once per frame
	void Update () {
		//while (!mainLogic.GetComponent<MainLogic>().RoundOver()) {
			int c = wiimote_count();
			for (int i = 0; i < c; i++) {
				if (ValidVoter(i)) {
					Debug.Log("Candidate " + i + " can vote!");
					DueProcess(i);
				}
			}
		//}

		//Judgement();
	}

	bool ValidVoter (int id) {
		Debug.Log("Candidate " + id + " is a valid voter!");
		foreach (KeyValuePair<Player, int> candidate in votes) {
			if (candidate.Key.wiimoteID == id) {
				return true;
			}
		}

		return false;
	}

	void DueProcess (int id) {

		if (wiimote_getButtonUp(id)) {

		}
		else if (wiimote_getButtonLeft(id)) {

		}
		else if (wiimote_getButtonDown(id)) {

		}
		else if (wiimote_getButtonRight(id)) {

		}
	}

	void Judgement () {
		Debug.Log("JUDGEMENT DAY");
		int majorityVotes = 0;

		foreach (KeyValuePair<Player, int> candidate in votes) {
			if (candidate.Value > majorityVotes) {
				majorityVotes = candidate.Value;
			}
		}

		int index = votes.IndexOfValue(majorityVotes);
		Player condemned = votes.Keys[index];

		for (int i = 0; i < numOfCandidates; i++) {
			int currentID = mainLogic.GetComponent<MainLogic>().alivePlayers[i].wiimoteID;
			if (currentID == condemned.wiimoteID) {
				mainLogic.GetComponent<MainLogic>().alivePlayers[i].DecreaseCurrentHealth();
			}
		}
	}

	void OnGUI () {
		int c = wiimote_count();
		if (c>0) {
			display = "";
			for (int i=0; i<=c-1; i++) {
				display += "Wiimote " + i + " found!\n";
			}
		}
		else display = "Press the '1' and '2' buttons on your Wii Remote.";
		
		GUI.Label( new Rect(10,Screen.height-100, 500, 100), display);
	}

	void OnApplicationQuit () {
		wiimote_stop();
	}
}

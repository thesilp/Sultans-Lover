using UnityEngine;
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
	
	// Use this for initialization
	void Start () {
		Debug.Log ("VOTE!");
		mainLogic = GameObject.Find("MainLogic");
		int numOfCandidates = mainLogic.GetComponent<MainLogic>().alivePlayers.Count;
		votes = new SortedList<Player, int>();

		for (int i = 0; i < numOfCandidates; i++) {
			votes.Add(mainLogic.GetComponent<MainLogic>().alivePlayers[i], 0);
		}
	}
	
	// Update is called once per frame
	void Update () {
		while (!mainLogic.GetComponent<MainLogic>().RoundOver()) {
			int c = wiimote_count();
			for (int i = 0; i < c; i++) {
				if (ValidVoter(i)) {
					Debug.Log("Candidate " + i + " can vote!");
					DueProcess(i);
				}
			}
		}

		Judgement();
	}

	bool ValidVoter (int id) {
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
		int majorityVotes = 0;

		foreach (KeyValuePair<Player, int> candidate in votes) {

		}

	}
}

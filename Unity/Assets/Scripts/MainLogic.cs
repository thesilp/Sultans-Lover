using UnityEngine;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;


public class MainLogic : MonoBehaviour {
	
	[DllImport ("UniWii")]
	private static extern void wiimote_start();
	
	[DllImport ("UniWii")]
	private static extern void wiimote_stop();
	
	[DllImport ("UniWii")]
	private static extern int wiimote_count();
	
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
	private static extern bool wiimote_getButtonPlus(int which);
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButtonMinus(int which);
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButtonHome(int which);
	[DllImport ("UniWii")]
	private static extern byte wiimote_getNunchuckStickX(int which);
	[DllImport ("UniWii")]
	private static extern byte wiimote_getNunchuckStickY(int which);
	[DllImport ("UniWii")]
	private static extern byte wiimote_getNunchuckAccX(int which);
	[DllImport ("UniWii")]
	private static extern byte wiimote_getNunchuckAccZ(int which);
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButtonNunchuckC(int which);
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButtonNunchuckZ(int which);

	public List<Player> players;

	private float currentGameTime = 0f;
	private float maxGameTime = 100f;
	
	private int startMaxHealth = 3;
	private int startMaxVotes = 1;



	// Use this for initialization
	void Start () {
	
		CreatePlayers();
	
	}


	
	// Update is called once per frame
	void Update () {

		/*wiimoteCount = wiimote_count();
		/*		if (isExpansion.Length != wiimoteCount)*
		isExpansion = new bool[wiimoteCount];
		
		/* check for wiimote count *
		if (wiimoteCount>0) {
			/* execute for every wiimote connected *
			for (int i=0; i<=wiimoteCount-1; i++) {
			}
		}
		*/
		currentGameTime += Time.deltaTime;

		if (!GameOver()) {
			/*
			 * =)
			 */ 
		}
		else {
			throw new Exception("Game is over!");
		}



	}



	private bool GameOver() {
		return currentGameTime == maxGameTime;
	}



	private void CreatePlayers() {

		players = new List<Player>();

		/* Go through and instantiate a player for each connected Wiimote. */
		int playerCount = wiimote_count();
		for (int id = 0; id < playerCount; ++id) {

			Player player = new Player();
			player.wiimoteID = id;

			player.maxHealth = startMaxHealth;
			player.currentHealth = startMaxHealth;

			player.maxVotes = startMaxVotes;
			
		}

	}
	


}

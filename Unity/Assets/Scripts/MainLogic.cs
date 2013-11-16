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


	public List<Player> allPlayers;
	public List<Player> alivePlayers;
	public List<Player> deadPlayers;


	private float remainingGameTime;
	private float maxGameTime = 200.0f;

	private float baseRoundTime = 20.0f; // The base round time. This is used to eventually restore the current round time if events change it.
	public float currentRoundTime; // What the round time is currently set at. Events can change this.
	public float remainingRoundTime;

	private int startMaxHealth = 3;
	private int startMaxVotes = 1;

	private int startMaxKillers = 1;
	private int remainingKillers;

	private GameEvent currentEvent;



	// Use this for initialization
	void Start () {
		currentRoundTime = baseRoundTime; 

		remainingGameTime = maxGameTime;
		remainingRoundTime = baseRoundTime;

		remainingKillers = startMaxKillers;

		CreatePlayers();

		currentEvent = GetNextEvent();
		currentEvent.Start();

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
		remainingGameTime = Mathf.Max(remainingGameTime-Time.deltaTime, 0.0f);
		remainingRoundTime = Mathf.Max(remainingRoundTime-Time.deltaTime, 0.0f);

//		Debug.Log("remainingGameTime: " + remainingGameTime);
//		Debug.Log("remainingRoundTime: " + remainingRoundTime);

		UpdatePlayers();

		if (!GameOver()) {

			if (!RoundOver()) {
				// Continue voting process.
				currentEvent.Update();
			}
			else {
				currentEvent.Destroy();
				currentEvent = GetNextEvent();
				currentEvent.Start();

				remainingRoundTime = currentRoundTime;
			}
		}
		else {

			throw new Exception(
				"Game is over!" + 
				"\n\tOnlyKillersRemaining? " + OnlyKillersRemaining() + 
			    "\n\tNoKillersRemaining? " + NoKillersRemaining() + 
				"\n\tNumKillersRemaining: " + remainingKillers + 
				"\n\tNumAlive: " + alivePlayers.Count + 
				"\n\tNumDead: " + deadPlayers.Count + 
				"\n\tNumTotal: " + allPlayers.Count 
			);
		}


	}



	private bool GameOver() {
		return (remainingGameTime <= 0.0f) || OnlyKillersRemaining() || NoKillersRemaining();
	
	}



	private bool RoundOver() {
		return remainingRoundTime <= 0.0f;
	}



	GameEvent GetNextEvent() {

		string [] possibleEventNames = {
			"DecreaseCurrentHealthEvent",
			"IncreaseCurrentHealthEvent",
			"IncreaseMaxHealthEvent",
			"LengthenRoundTimeEvent",
			"ShortenRoundTimeEvent",
		};

		int nextEventIndex = UnityEngine.Random.Range(0, possibleEventNames.Length-1);
		string nextEventName = possibleEventNames[nextEventIndex];

		System.Object [] parameters = {this};

		/* Dynamically create instance of a GameEvent from the string name of its type. */
		return (GameEvent)Activator.CreateInstance(Type.GetType(nextEventName), parameters);
	}


	
	bool NoKillersRemaining() {
		for (int i = 0; i < alivePlayers.Count; ++i) {
			if (alivePlayers[i].IsKiller()) {
				return false;
			}
		}
		return true;
	}



	bool OnlyKillersRemaining() {
		for (int i = 0; i < alivePlayers.Count; ++i) {
			if (!alivePlayers[i].IsKiller()) {
				return false;
			}
		}
		return true;
	}



	public void WriteMessage(string messageToWrite) {
		Debug.Log(messageToWrite);
	}

	

	private void CreatePlayers() {
		
		allPlayers = new List<Player>();
		alivePlayers = new List<Player>();
		deadPlayers = new List<Player>();
		
		/* Go through and instantiate a player for each connected Wiimote. */
		//int playerCount = wiimote_count();
		int playerCount = 5;
		for (int id = 0; id < playerCount; ++id) {
			
			Player player = new Player();
			player.gameObject = new GameObject();
			player.gameObject.name = "Player" + id + " GameObject";
			player.wiimoteID = id;
			
			player.maxHealth = startMaxHealth;
			player.currentHealth = startMaxHealth;
			
			player.maxVotes = startMaxVotes;
			player.spriteName = "girl1";

			SpriteRenderer renderer = (SpriteRenderer)player.gameObject.AddComponent("SpriteRenderer");
			Sprite newSprite = Resources.Load<Sprite>("Sprites/Characters/" + player.spriteName);

			renderer.sprite = newSprite;


			// For debugging, temporarily make the first few players killers depending on the total number of killers allowed for this game
			if (id <= remainingKillers) {
				player.roles.Add(Player.PlayerRoles.KILLER);
			}


			
			allPlayers.Add(player);
			alivePlayers.Add(player);

			Debug.Log("alivePlayers.Count: " + alivePlayers.Count);
		}
		
	}



	void UpdatePlayers() {
		
		for (int i = 0; i < alivePlayers.Count; ++i) {
			Player currentPlayer = alivePlayers[i];
			
			if (currentPlayer.IsDead()) {
				
				alivePlayers.Remove(currentPlayer);
				deadPlayers.Add (currentPlayer);
				
				/* If you're a killer... */
				if (currentPlayer.IsKiller()) {
					--remainingKillers;
				}
			}
			
		}
		
	}



	void OnGUI () {
		// Make a background box
		GUI.Box(new Rect(10,10,200,200), 
	        "Stats:" + 
		    "\n\tremainingGameTime " + remainingGameTime +
		    "\n\tremainingRoundTime? " + remainingRoundTime +
	        "\n\tOnlyKillersRemaining? " + OnlyKillersRemaining() + 
	        "\n\tNoKillersRemaining? " + NoKillersRemaining() + 
	        "\n\tNumKillersRemaining: " + remainingKillers + 
	        "\n\tNumAlive: " + alivePlayers.Count + 
	        "\n\tNumDead: " + deadPlayers.Count + 
	        "\n\tNumTotal: " + allPlayers.Count 
		);
	}
}

using UnityEngine;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;


public class GUITest : MonoBehaviour {
	
	public List<Player> allPlayers;
	public List<Player> alivePlayers;
	public List<Player> deadPlayers;
	
	
	private float remainingGameTime;
	private float maxGameTime = 200.0f;
	
	private float baseRoundTime = 10.0f; // The base round time. This is used to eventually restore the current round time if events change it.
	public float currentRoundTime; // What the round time is currently set at. Events can change this.
	public float remainingRoundTime;
	
	private int startMaxHealth = 3;
	private int startMaxVotes = 1;
	
	private int startMaxKillers = 1;
	private int remainingKillers;
	
	private GameEvent currentEvent;
	
	public Camera mainCamera;

	
	// Use this for initialization
	void Start () {
		mainCamera = GameObject.FindWithTag("MainCamera").camera;
		
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
	
	
	
	public bool GameOver() {
		return (remainingGameTime <= 0.0f) || OnlyKillersRemaining() || NoKillersRemaining();
		
	}
	
	
	
	public bool RoundOver() {
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

			Debug.Log("id: " + id);

			GameObject newPlayerGameObject = (GameObject)Resources.Load("Players/player_girl1");
			newPlayerGameObject.name = "Player " + id;

			Player player = newPlayerGameObject.GetComponent<Player>();
			player.wiimoteID = id;
			
			player.maxHealth = startMaxHealth;
			player.currentHealth = startMaxHealth;
			
			player.maxVotes = startMaxVotes;
			

			
			// For debugging, temporarily make the first few players killers depending on the total number of killers allowed for this game
			if (id <= remainingKillers) {
				player.roles.Add(Player.PlayerRoles.KILLER);
			}
			
			player.transform.position = mainCamera.ScreenToWorldPoint(
				new Vector3(
				((float)id/playerCount)*Screen.width + (Screen.width/(2.0f*playerCount)), 
				0.5f*Screen.height, 
				mainCamera.nearClipPlane
				)
			);
			player.transform.localScale = new Vector3(0.02f, 0.02f, 1.0f);
			
			allPlayers.Add(player);
			alivePlayers.Add(player);
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

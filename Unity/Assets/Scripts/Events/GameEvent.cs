using UnityEngine;
using System.Collections.Generic;

public class GameEvent  {

	protected MainLogic mainLogic;

	protected List<Player> allPlayers;
	protected List<Player> alivePlayers;
	protected List<Player> deadPlayers;


	public GameEvent(MainLogic mainLogic) {
		this.mainLogic = mainLogic;

		allPlayers = mainLogic.allPlayers;
		alivePlayers = mainLogic.alivePlayers;
		deadPlayers = mainLogic.deadPlayers;
	}


	public virtual void Start() {
		Debug.Log("Starting new event: '" + this.GetType().Name + "'");
	}


	public virtual void Update() {
		;
	}


	public virtual void Destroy() {
		Debug.Log("Destroying event: '" + this.GetType().Name + "'");
	}


}

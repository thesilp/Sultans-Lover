using UnityEngine;
using System.Collections.Generic;

public class GameEvent  {

	protected MainLogic mainLogic;
	protected List<Player> players;

	public GameEvent(MainLogic mainLogic) {
		this.mainLogic = mainLogic;
		players = mainLogic.players;
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

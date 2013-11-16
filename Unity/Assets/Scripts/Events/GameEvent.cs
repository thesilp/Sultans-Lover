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
		;
	}


	public virtual void Update() {
		;
	}


}

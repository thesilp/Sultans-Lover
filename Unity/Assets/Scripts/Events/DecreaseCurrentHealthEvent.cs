using UnityEngine;
using System.Collections.Generic;

public class DecreaseCurrentHealthEvent : GameEvent {


	public DecreaseCurrentHealthEvent(MainLogic mainLogic) : base(mainLogic) {
		;
	}


	public override void Start() {
		for (int i = 0; i < players.Count; ++i) {
			players[i].DecreaseCurrentHealth();
		}
	}

}

using UnityEngine;
using System.Collections.Generic;

public class IncreaseCurrentHealthEvent : GameEvent {


	public IncreaseCurrentHealthEvent(MainLogic mainLogic) : base(mainLogic) {
		;
	}


	public override void Start() {
		base.Start();

		for (int i = 0; i < players.Count; ++i) {
			players[i].IncreaseCurrentHealth();
		}
	}

}

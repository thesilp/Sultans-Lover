using UnityEngine;
using System.Collections.Generic;

public class IncreaseCurrentHealthEvent : GameEvent {


	public IncreaseCurrentHealthEvent(MainLogic mainLogic) : base(mainLogic) {
		;
	}


	public override void Start() {
		base.Start();

		for (int i = 0; i < alivePlayers.Count; ++i) {
			alivePlayers[i].IncreaseCurrentHealth();
		}

		SendMessage("All players are feeling rather healthy today! Everyone gets a heart.");
	}

}

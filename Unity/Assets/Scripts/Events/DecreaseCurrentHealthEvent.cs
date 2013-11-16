using UnityEngine;
using System.Collections.Generic;

public class DecreaseCurrentHealthEvent : GameEvent {


	public DecreaseCurrentHealthEvent(MainLogic mainLogic) : base(mainLogic) {
		;
	}


	public override void Start() {
		base.Start();

		for (int i = 0; i < alivePlayers.Count; ++i) {
			alivePlayers[i].DecreaseCurrentHealth();
		}

		SendMessage("All players fell down the stairs! Everyone's hearts decreased by one!");
	}

}

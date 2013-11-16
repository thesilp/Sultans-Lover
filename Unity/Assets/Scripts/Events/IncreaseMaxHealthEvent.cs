using UnityEngine;
using System.Collections.Generic;

public class IncreaseMaxHealthEvent : GameEvent {


	public IncreaseMaxHealthEvent(MainLogic mainLogic) : base(mainLogic) {
		;
	}


	public override void Start() {
		base.Start();

		for (int i = 0; i < alivePlayers.Count; ++i) {
			alivePlayers[i].IncreaseMaxHealth();
		}
	}

}

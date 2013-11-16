using UnityEngine;
using System.Collections.Generic;

public class LengthenRoundTimeEvent : GameEvent {


	private float originalRoundTime;
	public float roundTimeModifier = 15.0f;


	public LengthenRoundTimeEvent(MainLogic mainLogic) : base(mainLogic) {
		;
	}


	public override void Start() {
		base.Start();
		mainLogic.currentRoundTime += roundTimeModifier;

		SendMessage("All players woke up earlier! Round time is longer today!");
	}



	public override void Destroy() {
		base.Destroy();
		mainLogic.currentRoundTime = originalRoundTime;
	}

}

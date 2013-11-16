using UnityEngine;
using System.Collections.Generic;

public class ShortenRoundTimeEvent : GameEvent {


	private float originalRoundTime;
	public float roundTimeModifier = 15.0f;


	public ShortenRoundTimeEvent(MainLogic mainLogic) : base(mainLogic) {
		;
	}


	public override void Start() {
		base.Start();

		mainLogic.currentRoundTime -= roundTimeModifier;

		SendMessage("All players woke up late! Round time is shorter today!");
	}



	public override void Destroy() {
		base.Destroy();

		mainLogic.currentRoundTime = originalRoundTime;
	}

}

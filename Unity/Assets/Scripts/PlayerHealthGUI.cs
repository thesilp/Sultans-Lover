using UnityEngine;
using System.Collections.Generic;

public class PlayerHealthGUI : MonoBehaviour {

	private Player player;


	// Use this for initialization
	void Start () {
		player = transform.parent.gameObject.GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateHearts();
	}


	private void UpdateHearts() {
			
		Sprite currentSprite = Resources.Load<Sprite>("Sprites/GUI/" + player.maxHealth + "_hearts_" + player.currentHealth + "_filled");
		Debug.Log(currentSprite);
		this.GetComponent<SpriteRenderer>().sprite = currentSprite;

	}


}

using UnityEngine;
using System.Collections.Generic;

public class PlayerHealthGUI : MonoBehaviour {

	private Player player;

	private int filledHearts;
	private int emptyHearts;

	private List<GameObject> heartSprites;

	public Vector3 distanceFromPlayer;

	// Use this for initialization
	void Start () {
		player = (Player)GetComponent("Player");
		heartSprites = new List<GameObject>();
		this.transform.localPosition = distanceFromPlayer;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateHearts();
	}


	private void UpdateHearts() {
		filledHearts = player.currentHealth;
		emptyHearts = player.maxHealth - player.currentHealth;
	
		heartSprites.Clear();

		for (int i = 0; i < filledHearts; ++i) {
			GameObject newSpriteGameObject = NewHeart("filled");
			newSpriteGameObject.name = "Filled Heart";
			newSpriteGameObject.transform.parent = this.transform;
			newSpriteGameObject.transform.localScale = new Vector3(0.2f, 0.2f, 0.1f);

			heartSprites.Add( newSpriteGameObject );
		}
		for (int i = 0; i < emptyHearts; ++i) {
			GameObject newSpriteGameObject = NewHeart("empty");
			newSpriteGameObject.name = "Empty Heart";
			newSpriteGameObject.transform.parent = this.transform;
			newSpriteGameObject.transform.localScale = new Vector3(0.2f, 0.2f, 0.1f);

			heartSprites.Add( newSpriteGameObject );
		}


	}


	void OnGUI() {

	}

//	/* Load sprite. */
//	player.spriteName = "girl1";
//	SpriteRenderer renderer = (SpriteRenderer)player.gameObject.AddComponent("SpriteRenderer");
//	Sprite newSprite = Resources.Load<Sprite>("Sprites/Characters/" + player.spriteName);
//	renderer.sprite = newSprite;




	/* Builds a new GameObject with a heart sprite.
	 * Type should be 'filled' or 'empty'. 
	 */
	GameObject NewHeart(string type) {
		GameObject newSpriteGameObject = new GameObject();
		SpriteRenderer renderer = (SpriteRenderer)newSpriteGameObject.AddComponent("SpriteRenderer");
		Sprite newSprite = Resources.Load<Sprite>("Sprites/GUI/heart_" + type);
		renderer.sprite = newSprite;
		return newSpriteGameObject;
	}

}

using UnityEngine;
using System.Collections.Generic;

public class PlayerHealthGUI : MonoBehaviour {

	private Player player;

	private int filledHearts;
	private int emptyHearts;

	private List<GameObject> heartSprites;

	public Vector3 distanceFromPlayer = new Vector3(-1.0f, -4.0f, 0.0f);

	// Use this for initialization
	void Start () {
		player = transform.parent.gameObject.GetComponent<Player>();
		heartSprites = new List<GameObject>();
		this.transform.localPosition = distanceFromPlayer;

		UpdateHearts();
	}
	
	// Update is called once per frame
	void Update () {
//		UpdateHearts();
		DrawHearts();
	}


	private void UpdateHearts() {
		filledHearts = player.currentHealth;
		emptyHearts = player.maxHealth - player.currentHealth;
	
//		for (int i = 0; i < heartSprites.Count; ++i) {
//			GameObject.Destroy(heartSprites[i]);
//		}
//		heartSprites.Clear();

		for (int i = 0; i < filledHearts; ++i) {
			GameObject newSpriteGameObject = NewHeart("filled");
			newSpriteGameObject.name = "Filled Heart";
			newSpriteGameObject.transform.parent = this.transform;
			newSpriteGameObject.transform.localScale = new Vector3(.2f, .2f, 1f);
			newSpriteGameObject.transform.localPosition = new Vector3(0f, 0f, 0f);

			heartSprites.Add( newSpriteGameObject );
		}
		for (int i = 0; i < emptyHearts; ++i) {
			GameObject newSpriteGameObject = NewHeart("empty");
			newSpriteGameObject.name = "Empty Heart";
			newSpriteGameObject.transform.parent = this.transform;
			newSpriteGameObject.transform.localScale = new Vector3(.2f, .2f, 1f);
			newSpriteGameObject.transform.localPosition = new Vector3(0f, 0f, 0f);

			heartSprites.Add( newSpriteGameObject );
		}


	}


	void DrawHearts() {
		for (int i = 0; i < heartSprites.Count; ++i) {
			GameObject currentSpriteGameObject = heartSprites[i];
			Sprite currentSprite = ((SpriteRenderer)currentSpriteGameObject.renderer).sprite;
			Transform parent = this.transform.parent;

			currentSpriteGameObject.transform.localPosition = new Vector3(
				0.42f*i*currentSprite.bounds.extents.x,
				0, 
				0
			);
		}
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

﻿using UnityEngine;
using System.Collections;

public class Player {

	public int maxHealth;
	public int currentHealth;

	public int maxVotes;
	public int wiimoteID;

	// Sprite


	public void SetMaxHealth(int newMax) {
		maxHealth = newMax;
	}


	public void IncreaseCurrentHealth() {
		currentHealth = Mathf.Min(currentHealth+1, maxHealth);
	}


	public void DecreaseCurrentHealth() {
		currentHealth = Mathf.Max(currentHealth-1, 0);
	}
	

	public void IncreaseMaxHealth() {
		maxHealth = Mathf.Min(maxHealth+1, maxHealth);
		IncreaseCurrentHealth();
	}
	
	
	public void DecreaseMaxHealth() {
		maxHealth = Mathf.Max(maxHealth-1, 0);
		currentHealth = Mathf.Min(currentHealth, maxHealth);
	}

	
	public void SetMaxVotes(int newMax) {
		maxVotes = newMax;
	}

	



}
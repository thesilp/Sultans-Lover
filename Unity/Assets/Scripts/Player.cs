using UnityEngine;
using System.Collections.Generic;

public class Player {

	public int maxHealth;
	public int currentHealth;

	public int maxVotes;
	public int currentVotes;
	public int wiimoteID;

	public enum PlayerRoles {
		NONE, 
		KILLER,
		HEALER,
		CAPTAIN
	}

	public List<PlayerRoles> roles; 

	// Sprite


	public void SetCurrentHealth(int newCurrent) {
		currentHealth = newCurrent;
	}


	public void IncreaseCurrentHealth() {
		currentHealth = Mathf.Min(currentHealth+1, maxHealth);
	}


	public void DecreaseCurrentHealth() {
		currentHealth = Mathf.Max(currentHealth-1, 0);
	}


	public void SetMaxHealth(int newMax) {
		maxHealth = newMax;
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


	public bool IsDead() {
		return currentHealth == 0f;
	}


	public bool IsKiller() {
		for (int i = 0; i < roles.Count; ++i) {
			if (roles[i] == PlayerRoles.KILLER) {
				return true;
			}
		}
		return false;
	}
	



}

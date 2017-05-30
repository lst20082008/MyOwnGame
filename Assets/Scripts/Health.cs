using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public const int maxHealth = 100;
    public int currentHealth = maxHealth;
    public RectTransform healthBar;

    // Use this for initialization
    void Start () {
		
	}

    public void TakeDamage(GameObject playerFrom, int amount)
    {
        currentHealth -= amount;
        NetworkManager n = NetworkManager.instance.GetComponent<NetworkManager>();
        n.CommandHealthChange(playerFrom, this.gameObject, amount);
    }

    public void OnChangeHealth()
    {
        healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
        Debug.Log(healthBar.name);
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            currentHealth = 0;
        }
    }
}

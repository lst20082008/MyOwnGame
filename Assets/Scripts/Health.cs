using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    public const int maxHealth = 100;
    public int currentHealth = maxHealth;
    public RectTransform healthBar;
    private PlayerController pc;
    private Image healthState;
    private AudioSource audioSource;

    // Use this for initialization
    void Start () {
        pc = this.gameObject.GetComponent<PlayerController>();
        if (pc.isLocalPlayer == true)
        {
            audioSource = gameObject.GetComponent<AudioSource>();
            healthState = GameObject.Find("healthState").GetComponent<Image>();
            healthState.color = new Color(0,1f,0);
        }
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
        if (pc.isLocalPlayer == true)
        {
            audioSource.Play();
            if (currentHealth < 20)
                healthState.color = new Color(1f, 1f, 1f);
            else if (currentHealth < 50)
                healthState.color = new Color(1f, 0, 0);
            else if (currentHealth < 80)
                healthState.color = new Color(1f,0.5f,0);
        }
    }
}

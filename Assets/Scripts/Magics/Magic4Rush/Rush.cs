using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rush : MonoBehaviour {
    public float speed;
    public float latestTime;
    public GameObject P;
    public int damage;
	public float movement;
	public float coefficient;

    private float timer;

    private void Start()
    {
        timer = 0f;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= latestTime)
            Destroy(gameObject);
        transform.Translate(speed*Vector3.forward);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Health h = other.gameObject.GetComponent<Health>();
			PlayerController m = P.GetComponent<PlayerController>();
			damage = damage * (int)(m.movement * coefficient);
			h.TakeDamage(P, damage);
        }
    }
}

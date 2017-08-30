using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic9 : MonoBehaviour {
	public float movement;
	public GameObject P;
	public float coldtime;
	private float time;
	public int damage;
	public float timeCount;


	void Start () {
		PlayerController m = P.GetComponent<PlayerController>();
		m.movement = m.movement * 15.0f;
		time = 0f;
	}

	private void Update()
	{
		time += Time.deltaTime;
		timeCount += Time.deltaTime;
		if (time <= coldtime) {
			if (timeCount >= 2.0f) {
				timeCount = 0f;
				Health h = P.GetComponent<Health> ();
				h.TakeDamage (P, damage);
			}
			}
		else 
		{
			P.GetComponent<PlayerController>().movement /= 15.0f;
			time = 0f;
			Destroy(this.gameObject);
		}
	}
}

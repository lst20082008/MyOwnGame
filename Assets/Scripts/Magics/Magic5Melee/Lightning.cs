using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour {
	public float delayTime;
	public float force;
	public int damage;
	public float radius;
	public GameObject P;
	public bool LocalPlayer = false;

	private float timer = 0f;

	void Start()
	{
		Debug.Log("进入Lightning"+P);
	}

	void Update()
	{
		timer += Time.deltaTime;
		if (timer >= delayTime) {
			Kaminari ();
		}
	}

	void Kaminari()
	{
		Collider[] cols = Physics.OverlapSphere(transform.position,radius);
		if (cols.Length > 0)
		{
			for (int i = 0; i < cols.Length; i++) 
			{
				Rigidbody r = cols [i].GetComponent<Rigidbody> ();
					if (r != null)
						r.AddExplosionForce (force, transform.position, radius);
					Health h = cols [i].GetComponent<Health> ();
					if (h != null && r.gameObject != P)
						h.TakeDamage (P, damage);	
			}
		}
		Destroy (this.gameObject);
	}
}

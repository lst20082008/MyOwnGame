using UnityEngine;
using System.Collections;

public class FireBoom : MonoBehaviour {
	public float delayTime;
	public float force;
	public float damage;
	public float radius;
	public GameObject particular;

	private float timer = 0f;

	void Update()
	{
		timer += Time.deltaTime;
		if (timer >= delayTime) {
			Boom ();
		}
	}

	void Boom()
	{
		Collider[] cols = Physics.OverlapSphere(transform.position,2f);
		if (cols.Length > 0)
		{
			for (int i = 0; i < cols.Length; i++) 
			{
				Rigidbody r = cols [i].GetComponent<Rigidbody> ();
				if (r != null)
					r.AddExplosionForce (force, transform.position,radius);
                /*
				Health h = cols [i].GetComponent<Health> ();
				if (h != null)
					h.DoDamage (damage);	
                */
			}
		}
		Instantiate (particular, transform.position, transform.rotation);
		Destroy (this.gameObject);
	}
}

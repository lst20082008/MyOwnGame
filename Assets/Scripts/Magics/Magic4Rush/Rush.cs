using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rush : MonoBehaviour {
	public float delayTime = 0f;
	public float moveTime = 0f;
	private float timera = 0f;

	public GameObject P;
	public GameObject ShockWave;
	public float speed;
	public int damage;
	public float force;
	public float radius;
	public GameObject t;

	void Update () {
		timera += Time.deltaTime;
		if (timera >= delayTime) {
			RushWave ();
		}
	}
	
		void RushWave() {
		t.transform.position += t.transform.forward*5;
		Instantiate (ShockWave,t.transform.position,transform.rotation);
		GameObject s = P.GetComponent<GameObject> ();
		for (int j = 0; j <moveTime ; j++) {
			s.transform.Translate (Vector3.forward * Time.deltaTime * speed);
			Collider[] cols = Physics.OverlapSphere (transform.position,radius);
			if (cols.Length > 0) {
				for (int i = 0; i < cols.Length; i++) {
					Rigidbody r = cols [i].GetComponent<Rigidbody> ();
					if (r != null)
						r.AddExplosionForce (force, transform.position,radius);
					Health h = cols [i].GetComponent<Health> ();
					if (h != null)
						h.TakeDamage (P, damage);
				}
			}
		}
			Destroy (this.gameObject);
		}
}

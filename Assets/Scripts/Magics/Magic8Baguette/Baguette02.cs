using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baguette02 : MonoBehaviour {
	public GameObject P;
	public float timer = 0f;
	public float speed = 1f;
	public float freezeTime = 0.3f;
	public float rotateTime = 0.52f;
	public int damage = 10;
	public float playerSpeed = 5.0f;

	void Start () {
		this.transform.Rotate (91.61899f, 0.829f, -25.435f);
	}

	void Update () {
		timer += Time.deltaTime;
		if (timer>= freezeTime && timer <= rotateTime) {
			transform.position += P.transform.right * speed * 6.0f * Time.deltaTime;
			transform.position += P.transform.forward * speed * 16.0f * Time.deltaTime;
			if (transform.eulerAngles.z >= 0)
				transform.Rotate (Vector3.forward * Time.deltaTime * 80);
		}

		if (timer > rotateTime) {
			Destroy (gameObject);
		}
	}


	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Weapon"))
			damage = 0;
		else if (other.gameObject.CompareTag ("Player")) {
			if (other.gameObject != P) {
				other.GetComponent<Health> ().TakeDamage (P, damage);
				if (damage != 0)
					other.GetComponent<Rigidbody> ().AddForce (P.transform.forward * 1500);
			}

		}
	}
}

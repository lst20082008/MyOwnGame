using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baguette01 : MonoBehaviour {
	public GameObject P;
	public float timer = 0f;
	public float speed = 2f;
	public float rotateTime = 0.22f;
	public int damage = 10;

	void Start () {
		this.transform.Rotate (-110.123f, -15.26801f, 28.8009f);
	}

	void Update () {
		timer += Time.deltaTime;
		if (timer <= rotateTime) {
			transform.position += -P.transform.right * speed * 3.23f * Time.deltaTime;
			transform.position += P.transform.up * speed * 0.73207086f * Time.deltaTime;
			transform.position += P.transform.forward * speed * 0.8f * Time.deltaTime;
			if (transform.eulerAngles.y <= -15.26801)
				transform.Rotate (-Vector3.up * Time.deltaTime * 470 * speed);
			if (transform.eulerAngles.z >= 28.80099)
				transform.Rotate (-Vector3.forward * Time.deltaTime * 470 * speed);
			if (transform.eulerAngles.x <= -110.123)
				transform.Rotate (-Vector3.right * Time.deltaTime * 470 * speed);
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
					other.GetComponent<Rigidbody> ().AddForce (P.transform.forward * 1000 + P.transform.up * 1000);
			}
			
		}
	}
}

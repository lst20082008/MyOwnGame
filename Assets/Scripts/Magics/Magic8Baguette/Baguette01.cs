using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baguette01 : MonoBehaviour {
	public GameObject P;
	public float timer = 0f;
	public float speed;
	public float rotateTime;


	void Update () {
		timer += Time.deltaTime;
		if (timer <= rotateTime) {
			transform.position += -GameObject.FindGameObjectWithTag("MainCamera").transform.right * speed *  3.23f * Time.deltaTime;
			transform.position += GameObject.FindGameObjectWithTag("MainCamera").transform.up * speed * 0.73207086f * Time.deltaTime;
			transform.position += GameObject.FindGameObjectWithTag("MainCamera").transform.forward * speed * 0.8f * Time.deltaTime;

			if (transform.eulerAngles.y <= -15.26801)
				transform.Rotate (-Vector3.up * Time.deltaTime * 470);
			if (transform.eulerAngles.z >= 28.80099)
				transform.Rotate (-Vector3.forward * Time.deltaTime * 470);
			if (transform.eulerAngles.x <= -110.123)
				transform.Rotate (-Vector3.right * Time.deltaTime * 470);
		}
			
		if (timer > rotateTime) {
			Destroy (gameObject);
		}
	}
}

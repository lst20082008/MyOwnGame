using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoseRotate : MonoBehaviour {
	public float timer = 0f;
	public float rotateTime ;
	public float frozeTime;

	void Update () {
		timer += Time.deltaTime;
		if (timer < rotateTime && timer > frozeTime) {
			transform.Rotate (Vector3.forward * Time.deltaTime * 470);
		} else if (timer > rotateTime+0.3f) {
			Destroy (gameObject);
		}
	}
}

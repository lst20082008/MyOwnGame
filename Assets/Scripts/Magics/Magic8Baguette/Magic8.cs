using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic8 : MonoBehaviour {
	public GameObject P;
	public GameObject baguetteFriend01;
	public GameObject baguetteFriend02;
	public GameObject baguetteTest;
	public float distence;
	public GameObject rightHand;
	public GameObject leftHand;
	public Vector3 force;


	void Start () {
		rightHand = P.transform.Find ("Righthand").gameObject;
		leftHand = P.transform.Find ("Lefthand").gameObject;
		int rand = ((int)Random.Range (0, 2));
		Vector3 position = this.transform.position + this.transform.forward * distence;
		if (rand == 0) {
			Instantiate (baguetteFriend01, rightHand.transform).AddComponent<Baguette01> ().P = P;
		    Destroy (gameObject);
		}
		if (rand == 1) {
				force = P.transform.TransformVector (force);
				P.GetComponent<Rigidbody> ().AddForce (force);
			Instantiate (baguetteFriend02, leftHand.transform).AddComponent<Baguette02> ().P = P;
			Destroy (gameObject);
		}
	}

}

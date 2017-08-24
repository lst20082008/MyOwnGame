using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic8 : MonoBehaviour {
	public GameObject P;
	public GameObject baguetteF1;
	public float distence;
	public GameObject rightHand;


	void Start () {
		rightHand = P.transform.Find ("Righthand").gameObject;
		int rand = ((int)Random.Range (0, 5));
		rand = 0;
		Vector3 position = this.transform.position + this.transform.forward * distence;
		if (rand == 0) {
			Instantiate (baguetteF1, rightHand.transform).GetComponent<Baguette01> ().P = P;
			Destroy (gameObject);
		}
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic5 : MonoBehaviour {
	public GameObject lightning;
	public float distence;
	public GameObject P;

	void Start()
	{
		Vector3 position = this.transform.position + this.transform.forward*distence;
		Instantiate(lightning, position, this.transform.rotation).GetComponent<FireBoom>().P = P;
		Destroy (this.gameObject);
	}

}
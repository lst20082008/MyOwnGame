using UnityEngine;
using System.Collections;

public class Magic1 : MonoBehaviour {
	public GameObject ball;
	public float distence;

	void Awake()
	{
		Vector3 position = this.transform.position + this.transform.forward*distence;
		Instantiate (ball, position,this.transform.rotation);
		Destroy (this.gameObject);
	}

}

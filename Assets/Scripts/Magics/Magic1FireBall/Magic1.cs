using UnityEngine;
using System.Collections;

public class Magic1 : MonoBehaviour {
	public GameObject ball;
	public float distence;
    public GameObject P;

	void Start()
	{
		Vector3 position = this.transform.position + this.transform.forward*distence;
        Instantiate(ball, position, this.transform.rotation).GetComponent<FireBoom>().P = P;
		Destroy (this.gameObject);
	}

}

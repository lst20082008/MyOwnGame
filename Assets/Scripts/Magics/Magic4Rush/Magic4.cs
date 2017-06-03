using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic4 : MonoBehaviour {
    public GameObject shockwave;
	public GameObject stickle;
	public float distenceA;
	public float distenceB;
	public float distenceC;
	public float useTime;
	public GameObject P;
	private float timer = 0f;

	void Start () {
        timer = 0f;
		Vector3 position = this.transform.position + this.transform.forward*distenceA + this.transform.right*distenceB + this.transform.up*distenceC;
		Instantiate (stickle, position, this.transform.rotation);

	}

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= useTime)
        {
            Instantiate(shockwave,transform.position,transform.rotation).GetComponent<Rush>().P = P;
            Destroy(gameObject);
        }
    }


}


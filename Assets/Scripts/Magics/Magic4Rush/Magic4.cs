using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic4 : MonoBehaviour {
    public GameObject shockwave;
	public GameObject stickle;
	public float distanceA;
	public float distanceB;
	public float distanceC;
	public float distanceD;
	public float useTime;
	public GameObject P;
	private float timer = 0f;

	void Start () {
        timer = 0f;
		Vector3 position = this.transform.position + this.transform.forward*distanceA + this.transform.right*distanceB + this.transform.up*distanceC;
		Quaternion rotation = Quaternion.Euler(392+transform.eulerAngles.x,90 + transform.eulerAngles.y, -180 + transform.eulerAngles.z);
		Instantiate (stickle, position, rotation);

	}

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= useTime)
        {
			Instantiate(shockwave,transform.position+transform.forward*distanceD,transform.rotation).GetComponent<Rush>().P = P;
            Destroy(gameObject);
        }
    }


}


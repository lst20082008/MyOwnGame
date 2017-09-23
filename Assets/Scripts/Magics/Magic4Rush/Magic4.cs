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
    public GameObject rightHand;
    public Vector3 force;
	private float timer = 0f;

	void Start () {
        rightHand = P.transform.Find("Righthand").gameObject;
        timer = 0f;
		Vector3 position = this.transform.position + this.transform.forward*distanceA + this.transform.right*distanceB + this.transform.up*distanceC;
		Quaternion rotation = Quaternion.Euler(392+transform.eulerAngles.x,90 + transform.eulerAngles.y, -180 + transform.eulerAngles.z);
		Instantiate (stickle, rightHand.transform);
        //P.GetComponent<Rigidbody>().AddForce(force);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= useTime)
        {
			Instantiate(shockwave,P.transform.position+P.transform.forward*distanceD,P.GetComponent<PlayerController>().cmrRotation).GetComponent<Rush>().P = P;
            Destroy(gameObject);
        }
    }


}


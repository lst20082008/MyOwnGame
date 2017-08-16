using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic7 : MonoBehaviour {
    public GameObject P;
    public GameObject claw;
	// Use this for initialization
	void Start () {
        Instantiate(claw, GameObject.FindGameObjectWithTag("MainCamera").transform).GetComponent<Claw>().P = P;
        Destroy(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

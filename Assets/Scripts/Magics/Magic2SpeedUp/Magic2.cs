using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic2 : MonoBehaviour {
	public float movement;
	public GameObject P;


	// Use this for initialization
	void Start () {
        PlayerController m = P.GetComponent<PlayerController>();
		m.movement = m.movement * 2.0f;
	}

}

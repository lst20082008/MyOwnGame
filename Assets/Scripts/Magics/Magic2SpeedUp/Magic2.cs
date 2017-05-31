using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic2 : MonoBehaviour {
	public float movement;
	public GameObject P;
    public float coldtime;
    private float time;


	// Use this for initialization
	void Start () {
        PlayerController m = P.GetComponent<PlayerController>();
		m.movement = m.movement * 2.0f;
        time = 0f;
	}

    private void Update()
    {
        time += Time.deltaTime;
        if (time >= coldtime)
        {
            P.GetComponent<PlayerController>().movement /= 2.0f;
            time = 0f;
            Destroy(this.gameObject);
        }
    }

}

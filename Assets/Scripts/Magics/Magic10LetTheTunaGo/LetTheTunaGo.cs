﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetTheTunaGo : MonoBehaviour {
	public GameObject P;
	public Vector3 force = new Vector3(0, 200, 0);
	public int damage = 20;
	private Vector3 turn;


	void Start()
	{
		turn = new Vector3(0f, 0f, Random.Range(0, 35));
	}

	void Update()
	{
		transform.Rotate(turn*Time.deltaTime);
		transform.Translate(transform.forward * -5f * Time.deltaTime * Random.Range(0,10));
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
			other.GetComponent<Health>().TakeDamage(P, damage);
	}
}
	
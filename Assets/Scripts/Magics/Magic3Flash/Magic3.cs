using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic3 : MonoBehaviour {
public GameObject Pl;
public float position;


// Use this for initialization
void Start () {
        PlayerController t = Pl.GetComponent<PlayerController>();
     t.transform.Translate(Vector3.forward * 10);
    //t.transform.position += t.transform.forward *10;
 }

}
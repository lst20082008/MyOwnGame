using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claw : MonoBehaviour {
    public GameObject P;
    public float force1;
    public float force2;
    public float backTime;
    private Rigidbody rb;
    private bool touch;
    private float time;
    private LineRenderer lr;
    private float disTime;
	// Use this for initialization
	void Start () {
        this.transform.SetParent(null);
        touch = false;
        time = 0f;
        disTime = 0f;
        lr = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody>();
        rb.AddForce(force1*this.transform.TransformVector(Vector3.forward));
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            Destroy(rb);
            touch = true;
        }
    }

    // Update is called once per frame
    void Update () {
        disTime += Time.deltaTime;
        if (disTime >= 2f && !touch)
            Destroy(this.gameObject);
        if (touch) {
            time += Time.deltaTime;
        }
        if (time >= backTime)
        {
            Vector3 forceVector = this.transform.position - P.transform.position;
            P.GetComponent<Rigidbody>().AddForce(force2 * forceVector.normalized);
            Destroy(this.gameObject);
        }
        Vector3[] line = { transform.position, P.transform.position };
        lr.SetPositions(line);
	}
}

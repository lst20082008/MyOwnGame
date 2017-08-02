using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomShakalaka : MonoBehaviour {
    public float radius;
    public float force;
    public Vector3 force2;
    public int damage;
    public GameObject P = null;
    public GameObject hitPositionGameobject;
    public GameObject particular;
    public float height;
    private float deltaHeight;

	void Start () {
        P = transform.parent.parent.gameObject;
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            deltaHeight = height - P.transform.position.y;
            if (deltaHeight < 0)
                deltaHeight = 0;
            damage = (int)(damage * (deltaHeight / 9));
            Debug.Log(damage);
            Collider[] cols = Physics.OverlapSphere(hitPositionGameobject.transform.position, radius);
            if (cols.Length > 0)
            {
                for (int i = 0; i < cols.Length; i++)
                {
                    Rigidbody r = cols[i].GetComponent<Rigidbody>();
                    if (r != null && r.gameObject != P)
                        r.AddExplosionForce(force, hitPositionGameobject.transform.position, radius);
                    Health h = cols[i].GetComponent<Health>();
                    if (h != null && r.gameObject != P)
                        h.TakeDamage(P, damage);
                }
            }
            Instantiate(particular, hitPositionGameobject.transform.position, transform.rotation);
            force2 = P.transform.TransformVector(force2);
            P.GetComponent<Rigidbody>().AddForce(force2);
            Destroy(gameObject);
        }
    }
}

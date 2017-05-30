using UnityEngine;
using System.Collections;

public class FireBoom : MonoBehaviour {
	public float delayTime;
	public float force;
	public int damage;
	public float radius;
	public GameObject particular;
    public GameObject P;

	private float timer = 0f;

    void Start()
    {
        Debug.Log("进入FireBoom"+P);
    }

    void Update()
	{
		timer += Time.deltaTime;
		if (timer >= delayTime) {
			Boom ();
		}
	}

	void Boom()
	{
		Collider[] cols = Physics.OverlapSphere(transform.position,2f);
		if (cols.Length > 0)
		{
			for (int i = 0; i < cols.Length; i++) 
			{
				Rigidbody r = cols [i].GetComponent<Rigidbody> ();
				if (r != null)
					r.AddExplosionForce (force, transform.position,radius);
				Health h = cols [i].GetComponent<Health> ();
				if (h != null)
					h.TakeDamage(P,damage);	
			}
		}
		Instantiate (particular, transform.position, transform.rotation);
		Destroy (this.gameObject);
	}
}

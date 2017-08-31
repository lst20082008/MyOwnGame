using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic10 : MonoBehaviour {
	public GameObject P;
	public GameObject afterDark;
	public GameObject tuna;
	public float distance;
	private bool already;
	private float nowtime;
	private List <GameObject> fishes = new List <GameObject> ();
	public GameObject center;
	public float anotherDistance;

	void Start () {
		already = false;
		nowtime = 0f;
		Instantiate (afterDark, new Vector3(0,25,0),new Quaternion(0,0,0,0));
		fishes.Add (Instantiate (tuna, P.transform.Find ("Center").transform.forward * distance + P.transform.Find ("Center").transform.position, Quaternion.Euler((P.transform.rotation.eulerAngles + new Vector3(90,0,0)))));
			
	}

	void Update ()
	{
		nowtime += Time.deltaTime;
		if (nowtime >= 1 && already == false) {
			for (int i = -2; i <= 2; i++) {
				for (int j = -2; j <= 2; j++) {
					if (i == 0 && j == 0)
						continue;
					else {
						Vector3 p = fishes [0].transform.position + fishes [0].transform.forward * i * anotherDistance + fishes [0].transform.right * j * anotherDistance;
						fishes.Add (Instantiate (tuna, p, fishes [0].transform.rotation));
					}
				}
			}
			already = true;
		}
		if (nowtime >= 2) {
			foreach (GameObject fish in fishes) {
				fish.AddComponent<LetTheTunaGo> ().P = P;
			}
			Destroy (this.gameObject);
		}
	}

}

			

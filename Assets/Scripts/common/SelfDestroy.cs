using UnityEngine;
using System.Collections;

public class SelfDestroy : MonoBehaviour {
	public float time;
	private float nowTime;

	void Update () {
		nowTime += Time.deltaTime;
		if (nowTime >= time)
			Destroy (this.gameObject);
	}
}

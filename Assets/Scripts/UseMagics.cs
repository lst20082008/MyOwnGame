using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UseMagics : MonoBehaviour 
{
	public GameObject[] magics;
	public Image image;
	public Texture2D fire;
	public Texture2D ice;
	public Texture2D thunder;
	public Texture2D wind;
	public Image[] cd;

	private Sprite[] s = new Sprite[4];
	private int count=0;
	private int[] get = new int[2];
	private float[] timer = new float[16];
	private float[] coldTimer = {4f,8f,20f,5f,0.7f,5f,5f,0.65f,30f,0.65f,0.65f,0.65f,0.65f,0.65f,0.65f,0.65f};
	private int j;
    private PlayerController pc;

    void Awake()
    {
        cd = new Image[16];
        pc = this.gameObject.GetComponent<PlayerController>();
		image = GameObject.Find ("Image").GetComponent<Image> ();
		s[0] = Sprite.Create (fire, new Rect (0f, 0f, fire.width, fire.height), Vector2.zero);
		s[1] = Sprite.Create (ice, new Rect (0f, 0f, fire.width, fire.height), Vector2.zero);
		s[2] = Sprite.Create (thunder, new Rect (0f, 0f, fire.width, fire.height), Vector2.zero);
		s[3] = Sprite.Create (wind, new Rect (0f, 0f, fire.width, fire.height), Vector2.zero);
        for (int i = 0; i <= 15; i++)
        {
            j = i + 1;
            string name = "Cd_" + j.ToString();
            cd[i] = GameObject.Find(name).GetComponent<Image>();
        }
	}

	void Update()
	{
        if (!pc.isLocalPlayer)
        { return; }
        for (j =0;j<16;j++)
		{
			if(timer[j] <= 0f)
				cd[j].fillAmount = 0f;
			else
			{
				timer[j] -= Time.deltaTime;
				cd[j].fillAmount = timer[j]/coldTimer[j];
			}
		}
		if (Input.GetButtonDown ("Fire")) 
		{
			get[count] = 1;
			count++;
		}
		if (Input.GetButtonDown ("Ice")) 
		{
			get[count] = 2;
			count++;
		}
		if (Input.GetButtonDown ("Thunder")) 
		{
			get[count] = 3;
			count++;
		}
		if (Input.GetButtonDown ("Wind")) 
		{
			get[count] = 4;
			count++;
		}
		if (count == 1)
			Tubiaochuxian ();
		if (count == 2)
		{
			count = 0;
			int i = get [0] * 10 + get [1];
			DoMagic (i);
			Tubiaoxiaoshi ();
		}
	}

	public void DoMagic(int i)
	{
		switch (i)
		{
		case(11):
                CheckMagic(0);break;
		case(12):
                CheckMagic(1);break;
		case(13):
                CheckMagic(2);break;
		case(14):
                CheckMagic(3);break;
		case(21):
                CheckMagic(4);break;
		case(22):
                CheckMagic(5);break;
		case(23):
                CheckMagic(6);break;
		case(24):
                CheckMagic(7);break;
		case(31):
                CheckMagic(8);break;
		case(32):
                CheckMagic(9); break;
		case(33):
                CheckMagic(10);break;
		case(34):
                CheckMagic(11);break;
		case(41):
                CheckMagic(12);break;
		case(42):
                CheckMagic(13);break;
		case(43):
                CheckMagic(14);break;
		case(44):
                CheckMagic(15);break;
		}
	}

	void Tubiaochuxian()
	{
		image.sprite = s[get[0]-1];
		image.color = new Color (255f,255f,255f,255f);
	}

	void Tubiaoxiaoshi()
	{
		image.color = new Color (255f,255f,255f,0f);
	}

	public void MagicBoom(int i)
	{
        Debug.Log("进入发射"+i);
		if (i == 1) {
			Instantiate (magics [i], this.transform.position, this.transform.rotation).GetComponent<Magic2> ().P = gameObject;
		} else if (i == 2) {
			Instantiate (magics [i], this.transform.position, this.transform.rotation).GetComponent<Magic3> ().Pl = gameObject;
		} else if (i == 0) {
			Instantiate (magics [i], this.transform.position, this.transform.rotation).GetComponent<Magic1> ().P = gameObject;
		} else if (i == 3) {
			Instantiate (magics [i], this.transform.position, this.transform.rotation).GetComponent<Magic4> ().P = gameObject;
		} else if (i == 4) {
			Instantiate (magics [i], this.transform.position, this.transform.rotation).GetComponent<Magic5> ().P = gameObject;
		} else if (i == 5) {
			Instantiate (magics [i], this.transform.position, this.transform.rotation).GetComponent<Magic6> ().P = gameObject;
		} else if (i == 6) {
			Instantiate (magics [i], this.transform.position, this.transform.rotation).GetComponent<Magic7> ().P = gameObject;
		} else if (i == 7) {
			Instantiate (magics [i], this.transform.position, this.transform.rotation).GetComponent<Magic8> ().P = gameObject;
		} else if (i == 8) {
			Instantiate (magics [i], this.transform.position, this.transform.rotation).GetComponent<Magic9> ().P = gameObject;
		}
			else
        {
            Instantiate(magics[i], this.transform.position, this.transform.rotation);
        }
	}

    void CheckMagic(int i)
    {
        if (timer[i] <= 0)
        {
            NetworkManager.instance.GetComponent<NetworkManager>().CommandUseMagic(i);
            timer[i] = coldTimer[i];
        }
    }
}

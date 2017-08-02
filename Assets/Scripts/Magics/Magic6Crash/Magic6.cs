using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic6 : MonoBehaviour {
    public GameObject P;
    public Vector3 force;
    public GameObject hammer;
    public float rotateSpeed;
    public float rotateTime;
    public float height;
    public GameObject addForce;
    private GameObject leftHand;
    private float sumRotation;
    private bool itsHighNoon;
    private GameObject nowHammer;
    private float rotateTime1;
    

    void Start()
    {
        height = P.transform.position.y;
        rotateTime1 = 0;
        sumRotation = 0;
        itsHighNoon = true;
        P.GetComponent<PlayerController>().canRotate = false;
        force = P.transform.TransformVector(force);
        P.GetComponent<Rigidbody>().AddForce(force);
        leftHand = P.transform.Find("Lefthand").gameObject;
        nowHammer = Instantiate(hammer,leftHand.transform);

    }

    void Update()
    {
        if (P.transform.position.y > height)
            height = P.transform.position.y;
        rotateTime1 += Time.deltaTime;
        if(rotateTime1>=rotateTime)
        {
            if (sumRotation >= 1440)
                itsHighNoon = false;
            if (itsHighNoon)
            {
                P.transform.Rotate(new Vector3(rotateSpeed * Time.deltaTime, 0, 0));
                sumRotation += rotateSpeed * Time.deltaTime;
                nowHammer.transform.Rotate(new Vector3(0, 190 * Time.deltaTime, 0));
                if (sumRotation >= 1440)
                {
                    P.GetComponent<Rigidbody>().AddForce(new Vector3(0, -2500, 0));
                    nowHammer.GetComponent<SphereCollider>().isTrigger = true;
                    nowHammer.GetComponent<BoomShakalaka>().height = height;
                    Instantiate(addForce, P.transform);
                }
            }
            else
            {
                P.GetComponent<PlayerController>().canRotate = true;
                Destroy(gameObject);
            }
        }
    }
}

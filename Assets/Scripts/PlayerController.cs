using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    //方向灵敏度  
    public float sensitivityX = 10F;
    public float sensitivityY = 10F;
    //上下最大角(Y角)  
    public float minimumY = -60F;
    public float maximumY = 60F;
    public GameObject cmr;
    float rotationY = 0F;
	public float movement = 3.0f;

    public bool isLocalPlayer = false;
    public string playerName;
    public GameObject cameraPosition;


    private Vector3 currentPosition;
    private Vector3 oldPosition;
    private Quaternion oldRotation;
    private Quaternion currentRotation;

	// Use this for initialization
	void Start ()
    {
        oldPosition = transform.position;
        currentPosition = oldPosition;
        oldRotation = transform.rotation;
        currentRotation = oldRotation;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!isLocalPlayer)
            return;
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * movement;
        var y = Input.GetAxis("Vertical") * Time.deltaTime * movement;

        //根据鼠移的快慢(增量), 得相机左右旋的角度(理X)  
        float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
        //根据鼠移的快慢(增量), 得相机上下旋的角度(理Y)  
        rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        //角度限制. rotationY小于min,返回min. 大于max,返回max. 否则返回value   
        rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
        //体置一下相机角度  
        cmr.transform.localEulerAngles = new Vector3(-rotationY, 0, 0);
        transform.localEulerAngles = new Vector3(0, rotationX, 0);

        transform.Translate(x, 0, y);
        currentPosition = transform.position;
        currentRotation = transform.rotation;
        if (currentPosition != oldPosition)
        {
            //TODO Network
            NetworkManager.instance.GetComponent<NetworkManager>().CommandMove(transform.position);
            oldPosition = currentPosition;
        }

        if (currentRotation != oldRotation)
        {
            //TODO Network
            NetworkManager.instance.GetComponent<NetworkManager>().CommandTurn(transform.rotation);
            oldRotation = currentRotation;
        }
    }

}

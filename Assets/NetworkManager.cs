using SocketIO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour {

    public static NetworkManager instance;
    public Canvas canvas;
    public SocketIOComponent socket;
    public InputField playerNameInput;
    public GameObject player;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        //subscribe to all the various websocket events
        /*
        Just show you how to write
        socket.On("other player connected", OnOtherPlayerConnected);
        socket.On("play", OnPlay);
        socket.On("player move", OnPlayerMove);
        socket.On("player turn", OnPlayerTurn);
        */
        socket.On("register",OnRegister);
    }

    public void BtmRegister()
    {
        StartCoroutine(Register());
        Debug.Log("按钮点击，协程执行");
    }

    #region Commands

    IEnumerator ConnectToServer()
    {
        yield return new WaitForSeconds(0.5f);
        socket.Emit("player connect");
        yield return new WaitForSeconds(1f);
        string playerName = playerNameInput.text;
        //of course we don't have PlayerJSON now
        //PlayerJSON playerJSON = new PlayerJSON(playerName, playerSpawnPoints, enemySpawnPoints);
        //string data = JsonUtility.ToJson(playerJSON);
        //socket.Emit("play", new JSONObject(data));
        canvas.gameObject.SetActive(false);
    }
    
    IEnumerator Register()
    {
        yield return new WaitForSeconds(0.5f);
        RegisterSendJSON registerSendJSON = new RegisterSendJSON(playerNameInput.text);
        string data = JsonUtility.ToJson(registerSendJSON);
        socket.Emit("register",new JSONObject(data));
        Debug.Log("信息发送完毕:"+data);
    }
    
    #endregion

    #region Listening

    void OnRegister(SocketIOEvent socketIOEvent)
    {
        Debug.Log("进入OnRegister");
        RegisterJSON registerJSON = RegisterJSON.CreateFromJSON(socketIOEvent.data.ToString());
        Debug.Log(registerJSON.state);
    }

    #endregion

    #region JSONClasses

    [Serializable]
    public class RegisterSendJSON
    {
        public string name;

        public RegisterSendJSON(string _name)
        {
            name = _name;
        }
    }

    [Serializable]
    public class RegisterJSON
    {
        public string state;

        public static RegisterJSON CreateFromJSON(string data)
        {
            return JsonUtility.FromJson<RegisterJSON>(data);
        }
    }

    #endregion
}

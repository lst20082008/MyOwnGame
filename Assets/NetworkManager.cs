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
        socket.On("log in",OnLogIn);
    }

    public void BtmRegister()
    {
        StartCoroutine(Register());
    }

    public void BtmLogIn()
    {
        StartCoroutine(ConnectToServer());
    }
    #region Commands

    IEnumerator ConnectToServer()
    {
        yield return new WaitForSeconds(0.5f);
        string playerName = playerNameInput.text;
        LogInSendJSON logInSendJSON = new LogInSendJSON(playerName);
        string data = JsonUtility.ToJson(logInSendJSON);
        socket.Emit("log in",new JSONObject(data));
    }
    
    IEnumerator Register()
    {
        yield return new WaitForSeconds(0.5f);
        RegisterSendJSON registerSendJSON = new RegisterSendJSON(playerNameInput.text);
        string data = JsonUtility.ToJson(registerSendJSON);
        socket.Emit("register",new JSONObject(data));
    }
    
    #endregion

    #region Listening

    void OnRegister(SocketIOEvent socketIOEvent)
    {
        RegisterJSON registerJSON = RegisterJSON.CreateFromJSON(socketIOEvent.data.ToString());
        if (registerJSON.state == "error")
            playerNameInput.text = "账号已被注册";
        else
            playerNameInput.text = "账号成功注册";
    }

    void OnLogIn(SocketIOEvent socketIOEvent)
    {
        LogInJSON logInJSON = LogInJSON.CreateFromJSON(socketIOEvent.data.ToString());
        if (logInJSON.state == "ok")
            canvas.gameObject.SetActive(false);
        else
            playerNameInput.text = "无此账号!";
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

    [Serializable]
    public class LogInSendJSON
    {
        public string name;

        public LogInSendJSON(string _name)
        {
            name = _name;
        }
    }

    [Serializable]
    public class LogInJSON
    {
        public string state;
        public string name;
        public List<int> item;

        public static LogInJSON CreateFromJSON(string data)
        {
            return JsonUtility.FromJson<LogInJSON>(data);
        }
    }

    #endregion
}

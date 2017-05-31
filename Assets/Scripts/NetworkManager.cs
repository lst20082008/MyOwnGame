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
    public GameObject spawnPoint;
    public GameObject mainCamera;
    public Canvas magicCanvas;
    public Text test1;
    public Text test2;
    public Text test3;
    public Text test4;

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
        socket.On("other player connected",OnOtherPlayerConnected);
        socket.On("play",OnPlay);
        socket.On("player move", OnPlayerMove);
        socket.On("player turn", OnPlayerTurn);
        socket.On("other player disconnected", OnOtherPlayerDisconnect);
        socket.On("use magic", OnUseMagic);
        socket.On("health", OnHealth);
    }

    public void BtmRegister()
    {
        StartCoroutine(Register());
        Debug.Log("按下注册按钮");
    }

    public void BtmLogIn()
    {
        StartCoroutine(ConnectToServer());
        Debug.Log("按下登陆按钮");
    }

    public void AddText(string addString)
    {
        test1.text = test2.text;
        test2.text = test3.text;
        test3.text = test4.text;
        test4.text = addString;
    }

    #region Commands

    IEnumerator ConnectToServer()
    {
        yield return new WaitForSeconds(0.5f);
        string playerName = playerNameInput.text;
        LogInSendJSON logInSendJSON = new LogInSendJSON(playerName);
        string data = JsonUtility.ToJson(logInSendJSON);
        socket.Emit("log in",new JSONObject(data));
        Debug.Log("发送登陆信息"+data);
    }
    
    IEnumerator Register()
    {
        yield return new WaitForSeconds(0.5f);
        RegisterSendJSON registerSendJSON = new RegisterSendJSON(playerNameInput.text);
        string data = JsonUtility.ToJson(registerSendJSON);
        socket.Emit("register",new JSONObject(data));
        Debug.Log("发送注册信息" + data);
    }

    IEnumerator WaitASecond()
    {
        yield return new WaitForSeconds(1f);
    }

    public void CommandPlayerConnect()
    {
        socket.Emit("player connect");
        StartCoroutine(WaitASecond());
        PlaySendJSON playSendJSON = new PlaySendJSON(playerNameInput.text,spawnPoint);
        string data = JsonUtility.ToJson(playSendJSON);
        socket.Emit("play",new JSONObject(data));
    }

    public void CommandMove(Vector3 vec3)
    {
        string data = JsonUtility.ToJson(new PositionJSON(vec3));
        socket.Emit("player move", new JSONObject(data));
    }

    public void CommandTurn(Quaternion quar)
    {
        string data = JsonUtility.ToJson(new RotationJSON(quar));
        socket.Emit("player turn", new JSONObject(data));
    }

    public void CommandUseMagic(int i)
    {
        string data = JsonUtility.ToJson(new UseMaigcSendJSON(i));
        socket.Emit("use magic",new JSONObject(data));
    }

    public void CommandHealthChange(GameObject playerFrom, GameObject playerTo, int healthChange)
    {
        Debug.Log("发送Health指令"+playerFrom.name+"血量减少"+healthChange);
        HealthChangeJSON healthChangeJSON = new HealthChangeJSON(playerTo.name, healthChange, playerFrom.name);
        socket.Emit("health", new JSONObject(JsonUtility.ToJson(healthChangeJSON)));
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
        {
            canvas.gameObject.SetActive(false);
            magicCanvas.gameObject.SetActive(true);
            CommandPlayerConnect();
        }
        else
            playerNameInput.text = "无此账号!";
    }

    void OnOtherPlayerConnected(SocketIOEvent socketIOEvent)
    {
        PlayerJSON playerJSON = PlayerJSON.CreateFromJSON(socketIOEvent.data.ToString());
        Vector3 position = new Vector3(playerJSON.position[0], playerJSON.position[1], playerJSON.position[2]);
        Quaternion rotation = Quaternion.Euler(playerJSON.rotation[0], playerJSON.rotation[1], playerJSON.rotation[2]);
        GameObject o = GameObject.Find(playerJSON.name) as GameObject;
        if (o != null)
        {
            return;
        }
        GameObject p = Instantiate(player, position, rotation) as GameObject;
        p.name = playerJSON.name;
        PlayerController pc = p.GetComponent<PlayerController>();
        pc.playerName = playerJSON.name;
        pc.isLocalPlayer = false;
        Transform t = p.transform.Find("Healthbar Canvas");
        Transform t1 = t.transform.Find("Player Name");
        Text playerName = t1.GetComponent<Text>();
        playerName.text = playerJSON.name;
        //改变p的名字,并且告诉对象不是localplayer等等，并且初始化血量
        Health h = p.GetComponent<Health>();
        h.currentHealth = playerJSON.health;
        h.OnChangeHealth();
        AddText(playerJSON.name+"加入游戏");
    }

    void OnPlay(SocketIOEvent socketIOEvent)
    {
        PlayerJSON playerJSON = PlayerJSON.CreateFromJSON(socketIOEvent.data.ToString());
        Vector3 position = new Vector3(playerJSON.position[0], playerJSON.position[1], playerJSON.position[2]);
        Quaternion rotation = Quaternion.Euler(playerJSON.rotation[0], playerJSON.rotation[1], playerJSON.rotation[2]);
        GameObject o = GameObject.Find(playerJSON.name) as GameObject;
        if (o != null)
        {
            return;
        }
        GameObject p = Instantiate(player, position, rotation) as GameObject;
        p.name = playerJSON.name;
        PlayerController pc = p.GetComponent<PlayerController>();
        pc.playerName = playerJSON.name;
        pc.isLocalPlayer = true;
        mainCamera.transform.position = pc.cameraPosition.transform.position;
        mainCamera.transform.rotation = pc.cameraPosition.transform.rotation;
        mainCamera.transform.SetParent(p.transform);
        pc.cmr = mainCamera;
        Transform t = p.transform.Find("Healthbar Canvas");
        Transform t1 = t.transform.Find("Player Name");
        Text playerName = t1.GetComponent<Text>();
        playerName.text = playerJSON.name;
        //改变p的名字等等，并且初始化血量
        AddText("你加入游戏");
    }

    void OnPlayerMove(SocketIOEvent socketIOEvent)
    {
        string data = socketIOEvent.data.ToString();
        PlayerJSON playerJSON = PlayerJSON.CreateFromJSON(data);
        Vector3 position = new Vector3(playerJSON.position[0], playerJSON.position[1], playerJSON.position[2]);
        //if it's the current player exit
        if (playerJSON.name == playerNameInput.text)
            return;
        GameObject p = GameObject.Find(playerJSON.name) as GameObject;
        if (p != null)
            p.transform.position = position;
    }

    void OnPlayerTurn(SocketIOEvent socketIOEvent)
    {
        string data = socketIOEvent.data.ToString();
        PlayerJSON playerJSON = PlayerJSON.CreateFromJSON(data);
        Quaternion rotation = Quaternion.Euler(playerJSON.rotation[0], playerJSON.rotation[1], playerJSON.rotation[2]);
        //if it's the current player exit
        if (playerJSON.name == playerNameInput.text)
            return;
        GameObject p = GameObject.Find(playerJSON.name) as GameObject;
        if (p != null)
            p.transform.rotation = rotation;
    }
    void OnUseMagic(SocketIOEvent socketIOEvent)
    {
        string data = socketIOEvent.data.ToString();
        UseMaigcJSON useMagicJSON = UseMaigcJSON.CreateFromJSON(data);
        GameObject p = GameObject.Find(useMagicJSON.name) as GameObject;
        if (p != null)
        {
            p.GetComponent<UseMagics>().MagicBoom(useMagicJSON.i);
        }
    }

    void OnHealth(SocketIOEvent socketIOEvent)
    {
        //get the name of player whose health change
        string data = socketIOEvent.data.ToString();
        UserHealthJSON userHealthJSON = UserHealthJSON.CreateFromJSON(data);
        Debug.Log(userHealthJSON.name+"的血量变为"+userHealthJSON.health);
        GameObject p = GameObject.Find(userHealthJSON.name);
        Health h = p.GetComponent<Health>();
        h.currentHealth = userHealthJSON.health;
        if (h.currentHealth <= 0)
        {
            AddText("玩家"+userHealthJSON.name+"死亡");
        }
        h.OnChangeHealth();
    }

    void OnOtherPlayerDisconnect(SocketIOEvent socketIOEvent)
    {
        print("user disconnected");
        string data = socketIOEvent.data.ToString();
        PlayerJSON playerJSON = PlayerJSON.CreateFromJSON(data);
        Destroy(GameObject.Find(playerJSON.name));
        AddText(playerJSON.name + "离开游戏");
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

    [Serializable]
    public class PlayerJSON
    {
        public string name;
        public float[] position;
        public float[] rotation;
        public int health;

        public static PlayerJSON CreateFromJSON(string data)
        {
            return JsonUtility.FromJson<PlayerJSON>(data);
        }
    }

    [Serializable]
    public class PlaySendJSON
    {
        public string name;
        public float[] spawnPointPosition;
        public float[] spawnPointRotation;

        public PlaySendJSON(string _name,GameObject _spawnPoint)
        {
            name = _name;
            spawnPointPosition = new float[] {
                _spawnPoint.transform.position.x,
                _spawnPoint.transform.position.y,
                _spawnPoint.transform.position.z
            };
            spawnPointRotation = new float[] {
                _spawnPoint.transform.eulerAngles.x,
                _spawnPoint.transform.eulerAngles.y,
                _spawnPoint.transform.eulerAngles.z
            };
        }
    }

    [Serializable]
    public class PositionJSON
    {
        public float[] position;

        public PositionJSON(Vector3 _position)
        {
            position = new float[] { _position.x, _position.y, _position.z };
        }
    }

    [Serializable]
    public class RotationJSON
    {
        public float[] rotation;

        public RotationJSON(Quaternion _rotation)
        {
            rotation = new float[] { _rotation.eulerAngles.x, _rotation.eulerAngles.y, _rotation.eulerAngles.z };
        }
    }

    [Serializable]
    public class UseMaigcSendJSON
    {
        public int i;

        public UseMaigcSendJSON(int _i)
        {
            i = _i;
        }
    }

    [Serializable]
    public class UseMaigcJSON
    {
        public string name;
        public int i;

        public static UseMaigcJSON CreateFromJSON(string data)
        {
            return JsonUtility.FromJson<UseMaigcJSON>(data);
        }
    }

    [Serializable]
    public class HealthChangeJSON
    {
        public string name;
        public int healthChange;
        public string from;

        public HealthChangeJSON(string _name, int _healthChange, string _from)
        {
            name = _name;
            healthChange = _healthChange;
            from = _from;
        }
    }

    [Serializable]
    public class UserHealthJSON
    {
        public string name;
        public int health;

        public static UserHealthJSON CreateFromJSON(string data)
        {
            return JsonUtility.FromJson<UserHealthJSON>(data);
        }
    }

    #endregion
}

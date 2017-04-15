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
    }

    public void JoinGame()
    {
        StartCoroutine(ConnectToServer());
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

    #endregion

    #region Listening

    void OnPlay(SocketIOEvent socketIOEvent)
    {

    }

    #endregion

    #region JSONClasses

    [Serializable]
    public class PlayerJSON
    {
        public string name;

        public PlayerJSON(string _name)
        {
            name = _name;
        }
    }

    #endregion
}

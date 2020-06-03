using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [Header("Login UI")]
    public InputField playerNameInputField;
    public GameObject ui_LoginGameObject;


    [Header("Lobby UI")]
    public GameObject ui_LobbyGameObject;
    public GameObject ui_3dGameObject;


    [Header("Connection Status UI")]
    public GameObject ui_connectionStatusGameObject;
    public Text connectionStatusText;
    public bool showConnectionStatus = false;


    #region UNITY Methods
    // Start is called before the first frame update
    void Start()
    {
        ui_LobbyGameObject.SetActive(false);
        ui_3dGameObject.SetActive(false);
        ui_connectionStatusGameObject.SetActive(false);

        ui_LoginGameObject.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        if (showConnectionStatus)
        {
            connectionStatusText.text = "Connection Status: " + PhotonNetwork.NetworkClientState;
        }

    }
    #endregion 

    #region UI Callback Methods
    public void onEnterGameButtonClicked()
    {
        string playerName = playerNameInputField.text;

        if (!string.IsNullOrEmpty(playerName))
        {
            ui_LobbyGameObject.SetActive(false);
            ui_3dGameObject.SetActive(false);
            ui_LoginGameObject.SetActive(false);

            showConnectionStatus = true;
            ui_connectionStatusGameObject.SetActive(true);

            if (!PhotonNetwork.IsConnected)
            {
                PhotonNetwork.LocalPlayer.NickName = playerName;
                PhotonNetwork.ConnectUsingSettings();
            }
        }
        else
        {
            Debug.Log("Player Name Is Invalid");
        }
    }

    public void onQuickMatchButtonClicked()
    {
        //SceneManager.LoadScene("Scene_Loading");
        SceneLoader.Instance.LoadScene("Scene_PlayerSelection");
    }

    #endregion

    #region Photon Callback Methods
    public override void OnConnected()
    {
        //    base.OnConnected();
        Debug.Log("connected");
    }

    public override void OnConnectedToMaster()
    {
        // base.OnConnectedToMaster();
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + "connected to photon");

        ui_LobbyGameObject.SetActive(true);
        ui_3dGameObject.SetActive(true);

        ui_LoginGameObject.SetActive(false);
        ui_connectionStatusGameObject.SetActive(false);
    }

    #endregion
}

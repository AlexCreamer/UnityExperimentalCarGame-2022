using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;
using TMPro;

public class MyNetworkPlayer : NetworkBehaviour
{
    [SerializeField] public int ConnectionID;
    [SerializeField] public int PlayerIdNumber;
    [SyncVar] public ulong PlayerSteamID;
    [SyncVar(hook = nameof(PlayerNameUpdate))] public string PlayerName;
    [SyncVar(hook = nameof(PlayerReadyUpdate))] public bool Ready;

    private MyNetworkManager manager;
    private MyNetworkManager Manager
    {
        get
        {
            if (manager != null)
            {
                return manager;
            }
            return manager = MyNetworkManager.singleton as MyNetworkManager;
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);

    }

    private void PlayerReadyUpdate(bool oldValue, bool newValue)
    {
        if(isServer)
        {
            this.Ready = newValue;
        }
        if(isClient)
        {
            LobbyController.Instance.UpdatePlayerList();
        }
    }


    [Command] 
    private void CmdSetPlayerReady()
    {
        this.PlayerReadyUpdate(this.Ready, !this.Ready);
    }

    public void ChangeReady()
    {
        if (hasAuthority)
        {
            CmdSetPlayerReady();
        }
    }

    [SerializeField] private TMP_Text displayNameText = null;
    [SerializeField] public Renderer displayColorRenderer = null;

    #region Server

    [SyncVar(hook = nameof(HandleDisplayNameUpdated))]
    [SerializeField]
    private string displayName = "Missing Name";

    [SyncVar(hook = nameof(HandleDisplayColorUpdated))]
    [SerializeField]
    public Color displayColor = Color.yellow;

    [Server]
    public void SetDisplayName(string newDisplayName)
    {
        displayName = newDisplayName;
    }

    [Server]
    public void SetDisplayColor(Color newDisplayColor)
    {
        displayColor = newDisplayColor;
    }

    [Command]
    private void CmdSetDisplayName(string newDisplayName)
    {
        SetDisplayName(newDisplayName);
    }

    #endregion

    #region Client

    private void HandleDisplayNameUpdated(string oldName, string newName)
    {
        displayNameText.text = newName;
    }

    private void HandleDisplayColorUpdated(Color oldColor, Color newColor)
    {
        /*
        GameObject player = transform.Find("Player").gameObject;
        GameObject carBody = transform.Find("Car Body").gameObject;
        carBody.GetComponent<Renderer>().material.SetColor("_BaseColor", newColor);
        */

        displayColorRenderer.material.SetColor("_Color", newColor);
    }

    [ContextMenu("SetMyName")]
    public void SetMyName()
    {
        CmdSetDisplayName("My New Name");
    }

    #endregion Client

    #region Client or Server -- Unknown
    public override void OnStartAuthority()
    {
        CmdSetPlayerName(SteamFriends.GetPersonaName().ToString());
        gameObject.name = "LocalGamePlayer";
        LobbyController.Instance.FindLocalPlayer();
        LobbyController.Instance.UpdateLobbyName();
    }

    public override void OnStartClient()
    {
        Manager.GamePlayers.Add(this);
        LobbyController.Instance.UpdateLobbyName();
        LobbyController.Instance.UpdatePlayerList();
    }

    public override void OnStopClient()
    {
        Manager.GamePlayers.Remove(this);
        LobbyController.Instance.UpdatePlayerList();
    }

    [Command]
    private void CmdSetPlayerName(string PlayerName)
    {
        this.PlayerNameUpdate(this.PlayerName, PlayerName);
    }

    private void PlayerNameUpdate(string oldValue, string newValue)
    {
        if (isServer) // Host
        {
            this.PlayerName = newValue;
        }

        if (isClient) // Client
        {
            LobbyController.Instance.UpdatePlayerList();
        }
    }

    public void CanStartGame(string SceneName)
    {
        if(hasAuthority)
        {
            CmdCanStartGame(SceneName);
        }
    }
    
    [Command]
    public void CmdCanStartGame(string SceneName)
    {
        manager.StartGame(SceneName);
    }

    #endregion Client or Server -- Unknown

}
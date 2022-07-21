using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class MyNetworkPlayer : NetworkBehaviour
{   
    [SerializeField] private TMP_Text displayNameText = null;
    [SerializeField] private Renderer displayColorRenderer = null;

    #region Server

    [SyncVar(hook = nameof(HandleDisplayNameUpdated))]
    [SerializeField]
    private string displayName = "Missing Name";

    [SyncVar(hook = nameof(HandleDisplayColorUpdated))]
    [SerializeField]
    private Color displayColor = Color.yellow;

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

}
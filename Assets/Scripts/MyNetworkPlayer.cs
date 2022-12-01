using Mirror;
using TMPro;
using UnityEngine;

public class MyNetworkPlayer : NetworkBehaviour
{
    [SyncVar(hook = nameof(HandleDisplayName))] 
    [SerializeField] 
    public string displayName = "MissingName";

    public static string nickName=null;

    //[SyncVar(hook=nameof(HandleDisplayColor))]
    //[SerializeField]
    //private Color displayColor = Color.black;
    
    [Header("@References")]
    [SerializeField] private TextMeshProUGUI displayNameText = null;

    //[SerializeField] private Renderer meshRenderer = null;


    #region Server

    [Server]
    public void SetDisplayName(string newDisplayName)
    {
        displayName = newDisplayName;
    }

    [Server]
    public void SetDisplayColor(Color newDisplayColor)
    {
        //displayColor = newDisplayColor;
    }

    [Command]
    private void CmdSetDisplayName(string newDisplayName)
    {
        // Validate First
        
        // Then Set

        RpcLogNewDisplayName(newDisplayName);
        
        SetDisplayName(newDisplayName);
    }

    #endregion

    #region Client

    private void HandleDisplayColor(Color oldColor, Color newColor)
    {
        //meshRenderer.material.color = newColor;
    }
    
    public void HandleDisplayName(string oldName, string newName)
    {
        displayNameText.text = newName;
    }

    [ContextMenu("SetMyName")]
    private void SetMyName()
    {
        CmdSetDisplayName("My New Name");
    }
    
    [ClientRpc]
    private void RpcLogNewDisplayName(string newDisplayName)
    {
        Debug.Log(newDisplayName);
    }

    #endregion
}
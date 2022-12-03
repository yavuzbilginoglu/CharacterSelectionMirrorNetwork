using Mirror;
using System;
using System.Collections.Generic;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;


public class MyNetworkManager : NetworkManager
{

    //public static int PickedSkinId;
    public static event Action OnClientConnected;
    public static event Action OnClientDisconnected;
    //public static SkinHolder skin;
    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        base.OnServerAddPlayer(conn);
        if (conn.identity.gameObject.GetComponent<MyNetworkPlayer>() != null)
        {
            print("connection:" + conn);
            var connectedPlayer = conn.identity.GetComponent<MyNetworkPlayer>();
            connectedPlayer.SetDisplayName(CharacterSelect.nickname);
        }
        else
        {
            print("null");
        }

        //connectedPlayer.SetDisplayColor(new Color(Random.Range(0F,1F), Random.Range(0F,1F),Random.Range(0F,1F),1F));

        //connectedPlayer.SetDisplayName($"Player {numPlayers}");

        //print(PickedSkinId);
        //skin=conn.identity.GetComponent<SkinHolder>();
        //skin.PickSkin(0);
    }

    public override void OnStartClient()
    {
        //InputManager.Add(ActionMapNames.Player);//Hareketi kitle
        //InputManager.Remove(ActionMapNames.Player);
        //InputManager.Controls.Player.Look.Enable();
    }
}


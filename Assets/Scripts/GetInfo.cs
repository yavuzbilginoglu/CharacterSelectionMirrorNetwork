using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetInfo : NetworkBehaviour
{
    public static string nickname;

    public void Login(TextMeshProUGUI nick)
    {
        nickname = nick.text;
        print(nickname);
        MyNetworkPlayer.nickName=nick.text;
        SceneManager.LoadScene("Game");
    }
}

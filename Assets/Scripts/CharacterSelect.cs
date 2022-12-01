using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;

    public class CharacterSelect : NetworkBehaviour
    {
        [SerializeField] private GameObject characterSelectDisplay = default;
        [SerializeField] private Transform characterPreviewParent = default;
        [SerializeField] private TMP_Text characterNameText = default;
        [SerializeField] private float turnSpeed = 90f;
        [SerializeField] private Character[] characters = default;
        public static string nickname = MyNetworkPlayer.nickName;

    private int currentCharacterIndex = 0;
        private List<GameObject> characterInstances = new List<GameObject>();

        public override void OnStartClient()
        {
            if (characterPreviewParent.childCount == 0)
            {
                foreach (var character in characters)
                {
                    GameObject characterInstance =
                        Instantiate(character.CharacterPreviewPrefab, characterPreviewParent);

                    characterInstance.SetActive(false);

                    characterInstances.Add(characterInstance);
                }
            }

            characterInstances[currentCharacterIndex].SetActive(true);
            characterNameText.text = nickname;//characters[currentCharacterIndex].CharacterName;

            

            characterSelectDisplay.SetActive(true);
        }

        private void Update()
        {
            characterPreviewParent.RotateAround(
                characterPreviewParent.position,
                characterPreviewParent.up,
                turnSpeed * Time.deltaTime);
        }

        public void Select()
        {
            CmdSelect(currentCharacterIndex);
            //print(currentCharacterIndex);
            //MyNetworkManager.skin.PickSkin(currentCharacterIndex);
        
            characterSelectDisplay.SetActive(false);
            InputManager.Remove(ActionMapNames.Player);
            InputManager.Controls.Player.Look.Enable();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        [Command(requiresAuthority = false)]
        public void CmdSelect(int characterIndex, NetworkConnectionToClient sender=null)
        {
            GameObject characterInstance = Instantiate(characters[characterIndex].GameplayCharacterPrefab);
        //characterInstance.GetComponentInChildren<TextMeshProUGUI>().text= nickname;
        print(GetInfo.nickname);
        NetworkServer.Spawn(characterInstance, sender);
        var connectedPlayer = characterInstance.GetComponent<MyNetworkPlayer>();
        connectedPlayer.displayName= GetInfo.nickname;
        connectedPlayer.SetDisplayName(GetInfo.nickname);
        
        }

        public void Right()
        {
            characterInstances[currentCharacterIndex].SetActive(false);

            currentCharacterIndex = (currentCharacterIndex + 1) % characterInstances.Count;

            characterInstances[currentCharacterIndex].SetActive(true);
            //characterNameText.text = characters[currentCharacterIndex].CharacterName;
        }

        public void Left()
        {
            characterInstances[currentCharacterIndex].SetActive(false);

            currentCharacterIndex--;
            if (currentCharacterIndex < 0)
            {
                currentCharacterIndex += characterInstances.Count;
            }

            characterInstances[currentCharacterIndex].SetActive(true);
            //characterNameText.text = characters[currentCharacterIndex].CharacterName;
        }
    }

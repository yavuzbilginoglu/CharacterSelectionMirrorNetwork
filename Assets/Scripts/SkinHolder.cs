using Mirror;
using UnityEngine;

public class SkinHolder : MonoBehaviour
{
    public static SkinHolder Instance;
    public GameObject[] HoldedSkins;

    private void Start()
    {
        print(HoldedSkins);
    }
    public void PickSkin(int id)
    {
        var animator = HoldedSkins[id].gameObject.GetComponent<Animator>();
        //gameObject.GetComponent<NetworkAnimator>().animator = animator;
        //gameObject.GetComponent<PlayerMovementController>().animator = animator;
        for (int i = 0; i < HoldedSkins.Length; i++)
        {
            if (id==i)
            {
                HoldedSkins[i].SetActive(true);
            }
            else
            {
                HoldedSkins[i].SetActive(false);
            }
        }

        
    }
}

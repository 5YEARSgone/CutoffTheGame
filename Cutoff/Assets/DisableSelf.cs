using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableSelf : MonoBehaviour
{

    public DismemberUI disU;
    public Player player;

    void Disable()
    {
        GetComponent<Animator>().enabled = false;
    }


    public void UpdateAndEndRemote()
    {
        player.bodyInfo.UpdateBody();
        player.UpdateBodyImage();
        player.inMenu = false;
    }
    
    public void TempSetBodyRot()
    {
        player.bodyTilt.transform.localPosition = new Vector3(0, 0, 0);
        player.bodyTilt.localRotation = Quaternion.Euler(0, 0, 0);
    }

}

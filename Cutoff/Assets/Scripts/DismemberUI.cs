using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DismemberUI : MonoBehaviour
{
    public Player player;
    public Animator an;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void ButtonFootLeft()
    {
        player.bodyInfo.legL = false;
        an.enabled = true;
        an.SetTrigger("chop");
        gameObject.SetActive(false);
    }
    public void ButtonFootRight()
    {
        player.bodyInfo.legR = false;
        an.enabled = true;
        an.SetTrigger("chop");
        gameObject.SetActive(false);
    }

    public void ButtonEyeLeft()
    {
        player.bodyInfo.eyeL = false;
        an.enabled = true;
        an.SetTrigger("leftEye");
        gameObject.SetActive(false);
    }

    public void ButtonEyeRight()
    {
        player.bodyInfo.eyeR = false;
        an.enabled = true;
        an.SetTrigger("rightEye");
        gameObject.SetActive(false);
    }

    public void ButtonHandRight()
    {
        player.bodyInfo.handR = false;
        an.enabled = true;
        an.SetTrigger("chop");
        gameObject.SetActive(false);
    }

    public void ButtonHandLeft()
    {
        player.bodyInfo.handL = false;
        an.enabled = true;
        an.SetTrigger("chop");
        gameObject.SetActive(false);
    }

    public void UpdateAndEnd()
    {
        player.bodyInfo.UpdateBody();
        player.UpdateBodyImage();
        player.inMenu = false;
    }
}

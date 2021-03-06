﻿using UnityEngine;
using System.Collections;

public class OrderItemCommandButton : MonoBehaviour {

    public static ItemType type;

    void OnClick()
    {
        //Debug.Log(type);
        if (type == null)
            return;

        string msg;
        switch (ItemManager.OrderItem(type))
        {
            case 0: //success
                Sound.component.GetComponent<AudioSource>().clip = Sound.component.clip[0];
                break;
            case 1:
                Sound.component.GetComponent<AudioSource>().clip = Sound.component.clip[1];
                msg = "no money";
                Debug.Log(msg);

                StartCoroutine(MessageManager.messageBox.printMessage(msg));

                break;
            case 2:
                Sound.component.GetComponent<AudioSource>().clip = Sound.component.clip[1];
                msg = "no space";
                Debug.Log(msg);

                StartCoroutine(MessageManager.messageBox.printMessage(msg));

                break;
        }
        Sound.component.GetComponent<AudioSource>().Play();
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerKeyHolder : MonoBehaviour
{
    //Lista com todas as chaves ja pegas pelo player
    [HideInInspector]
    public List<keyConfig> keysHolding = new List<keyConfig>();

    public AudioSource keySound;

    //checa se alguma chave tem a mesma senha da porta
    private void checkKeyCode(doorConfig door)
    {
        if (door != null)
        {
            if (!door.openDoor)
            {
                for (int i = 0; i < keysHolding.Count; i++)
                {
                    if (keysHolding[i].keyPassword == door.doorPassword)
                    {
                        door.openDoor = true;
                        keysHolding.RemoveAt(i);
                        return;
                    }
                }
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Se for uma chave guardar na lista de chaves pegas pelo player
        if (other.gameObject.CompareTag("key"))
        {
            keyConfig key = other.gameObject.GetComponent<keyConfig>();
            if (key != null)
            {
                keySound.Play();
                keysHolding.Add(key);
                other.gameObject.SetActive(false);
            }
        } //Se for uma porta checa se alguma chave do player abre ela
        else if (other.gameObject.CompareTag("door"))
        {
            doorConfig door = other.gameObject.GetComponent<doorConfig>();
            checkKeyCode(door);
        }
    }
}

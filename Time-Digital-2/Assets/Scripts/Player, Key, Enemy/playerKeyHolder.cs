﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerKeyHolder : MonoBehaviour
{
    //Lista com todas as chaves ja pegas pelo player
    private List<keyConfig> keysHolding = new List<keyConfig>();

    //checa se alguma chave tem a mesma senha da porta
    private void checkKeyCode(doorConfig door)
    {
        if (door != null)
        {
            for (int i = 0; i < keysHolding.Count; i++)
            {
                if (keysHolding[i].keyPassword == door.doorPassword)
                {
                    door.openDoor = true;
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
                keysHolding.Add(key);
                Destroy(other.gameObject);
            }
        } //Se for uma porta checa se alguma chave do player abre ela
        else if (other.gameObject.CompareTag("door"))
        {
            doorConfig door = other.gameObject.GetComponent<doorConfig>();
            checkKeyCode(door);
        }
    }
}
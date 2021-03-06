﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySounds : MonoBehaviour
{
    public GameObject formigaAudioListGameObject;
    private AudioSource [] formigaAudioList;
    private bool isPlayingAny = false;
    private bool isWaitingToPlay = false;

    public float maxDuration;

    public AudioSource perseguicao;
    private bool isPlayingPerseguicao = false;

    private EnemyAI eAI;

    // Start is called before the first frame update
    void Start()
    {
        int qntChildren = transform.childCount;
        formigaAudioList = new AudioSource[qntChildren];

        for (int i = 0; i < qntChildren; ++i)
        {
            formigaAudioList[i] = formigaAudioListGameObject.transform.GetChild(i).GetComponent<AudioSource>();
            formigaAudioList[i].loop = false;
        }

        eAI = this.GetComponent<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        isPlayingAny = false;
        foreach (AudioSource audio in formigaAudioList)
        {
            if (audio.isPlaying)
            {
                isPlayingAny = true;
                break;
            }
        }
        
        if (!isPlayingAny && !isWaitingToPlay && !Manager.current.turnOff)
        {
            int i = Random.Range(0, formigaAudioList.Length);
            if(formigaAudioList[i].isActiveAndEnabled)
                StartCoroutine(WaitForPlay(formigaAudioList[i]));
        }

        if (eAI.myState == EnemyAI.stateMachine.isAttacking)
        {
            if (!isPlayingPerseguicao)
            {
                isPlayingPerseguicao = true;
                AudioManager.sharedInstance.PlayRequest(perseguicao, AudioManager.SoundType.Perseguicao);
            }
        }
        else
        {
            if (isPlayingPerseguicao)
            {
                isPlayingPerseguicao = false;
                AudioManager.sharedInstance.StopRequest(AudioManager.SoundType.Perseguicao);
            }
        }
    }

    public IEnumerator WaitForPlay(AudioSource audio)
    {
        isWaitingToPlay = true;
        float offset = (float)Random.Range(0, maxDuration);
        yield return new WaitForSeconds(offset);
        isWaitingToPlay = false;
        audio.Play();
    }
}

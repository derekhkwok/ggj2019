﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Congret_Prefab : MonoBehaviour
{
    public static Congret_Prefab Instance;

    public GameObject enterBtn;
    public ParticleSystem victoryPS;
    public Action onEnter;
    Vector3 oriPos = new Vector3(0f, 40f, 0f);

    public void Create( Action _onEnter)
    {
        onEnter = _onEnter;
        enterBtn.SetActive(true);
        enterBtn.transform.localScale = new Vector3(0f, 0f, 0f);
        enterBtn.transform.localPosition = new Vector3(0f, 40f, 0f);

        SFXManager.instance.PlaySFX(SFXManager.SFX.victory);

        iTween.Stop(enterBtn);

        Invoke("EnableEnterBtn", 0.5f);
    }

    private void Start()
    {
        Instance = this;
        enterBtn.SetActive(false);
        enterBtn.transform.localScale = new Vector3(0f, 0f, 0f);
        enterBtn.transform.localPosition = new Vector3(0f, 40f, 0f); 
    }

    private void EnableEnterBtn()
    {
        victoryPS.Play();
        iTween.ScaleTo(enterBtn.gameObject, iTween.Hash(
            "scale", Vector3.one,
            "time", 0.5f,
            "islocal", true,
            "easetype", iTween.EaseType.easeOutBack
            ));
    }

    // Update is called once per frame
    public void Dismiss()
    {
        //iTween.MoveTo(this.gameObject, iTween.Hash(
            //"z", -15f,
            //"time", 0.5f,
            //"islocal", true,
            //"easetype", iTween.EaseType.easeInCubic,
            //"oncomplete", "TerminateMyself"
            //));
        iTween.MoveTo(enterBtn.gameObject, iTween.Hash(
            "y", -1000f,
            "time", 1.2f,
            "islocal", true,
            "easetype", iTween.EaseType.easeOutBack
            //"oncomplete", "TerminateMyself"
        ));

        Invoke("TerminateMyself", 1.2f);
    }

    public void TerminateMyself()
    {
        victoryPS.Stop();
        StageManager.instance.OnClickEndStage();
        enterBtn.SetActive(false);
        enterBtn.transform.localScale = new Vector3(0f, 0f, 0f);
        //enterBtn.transform.localPosition = oriPos;
        enterBtn.transform.localPosition = new Vector3(0f, 40f, 0f);

    }
}

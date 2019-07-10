using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetPanel : UIBase
{

    private Button btnSet;
    private Image imgBg;
    private Button btnClose;
    private Text txtAudio;
    private Toggle togAudio;
    private Text txtVolume;
    private Slider sldVolume;
    private Button btnQuit;

    void Start()
    {
        btnSet = transform.Find("btnSet").GetComponent<Button>();
        imgBg = transform.Find("imgBg").GetComponent<Image>();
        btnClose = transform.Find("btnClose").GetComponent<Button>();
        txtAudio = transform.Find("txtAudio").GetComponent<Text>();
        togAudio = transform.Find("togAudio").GetComponent<Toggle>();
        txtVolume = transform.Find("txtVolume").GetComponent<Text>();
        sldVolume = transform.Find("sldVolume").GetComponent<Slider>();
        btnQuit = transform.Find("btnQuit").GetComponent<Button>();

        btnSet.onClick.AddListener(setClick);
        btnClose.onClick.AddListener(closeClick);
        btnQuit.onClick.AddListener(quitClick);
        togAudio.onValueChanged.AddListener(audioValueChanged);
        sldVolume.onValueChanged.AddListener(volumeValueChanged);

        setObjectsActive(false);
    }

    private void setClick()
    {
        setObjectsActive(true);
    }
    private void closeClick()
    {
        setObjectsActive(false);
    }
    private void quitClick()
    {
        Application.Quit();
    }

    private void audioValueChanged(bool value)
    {
        //TODO
    }
    private void volumeValueChanged(float value)
    {
        //TODO
    }

    private void setObjectsActive(bool active)
    {
        imgBg.gameObject.SetActive(active);
        btnClose.gameObject.SetActive(active);
        txtAudio.gameObject.SetActive(active);
        togAudio.gameObject.SetActive(active);
        txtVolume.gameObject.SetActive(active);
        sldVolume.gameObject.SetActive(active);
        btnQuit.gameObject.SetActive(active);
    }
}

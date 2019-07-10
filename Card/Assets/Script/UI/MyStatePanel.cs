using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyStatePanel : StatePanel
{
    private void Awake()
    {
        Bind(UIEvent.SHOW_GRAB_BUTTON,
            UIEvent.SHOW_DEAL_BUTTON);
    }

    public override void Execute(int eventCode, object message)
    {
        base.Execute(eventCode, message);

        switch (eventCode)
        {
            case UIEvent.SHOW_GRAB_BUTTON:
                {
                    bool active = (bool)message;
                    btnGrab.gameObject.SetActive(active);
                    btnNGrab.gameObject.SetActive(active);
                    break;
                }
            case UIEvent.SHOW_DEAL_BUTTON:
                {
                    bool active = (bool)message;
                    btnDeal.gameObject.SetActive(active);
                    btnNDeal.gameObject.SetActive(active);
                    break;
                }
            default:
                break;
        }
    }

    private Button btnDeal;
    private Button btnNDeal;
    private Button btnGrab;
    private Button btnNGrab;
    private Button btnReady;

    protected override void Start()
    {
        base.Start();

        btnDeal = transform.Find("btnDeal").GetComponent<Button>();
        btnNDeal = transform.Find("btnNDeal").GetComponent<Button>();
        btnGrab = transform.Find("btnGrab").GetComponent<Button>();
        btnNGrab = transform.Find("btnNGrab").GetComponent<Button>();
        btnReady = transform.Find("btnReady").GetComponent<Button>();

        btnDeal.onClick.AddListener(dealClick);
        btnNDeal.onClick.AddListener(nDealClick);

        btnGrab.onClick.AddListener(
            delegate ()
            {
                grabClick(true);
            });
        btnNGrab.onClick.AddListener(
            delegate ()
            {
                grabClick(false);
            });

        btnReady.onClick.AddListener(readyClick);

        //默认状态
        btnGrab.gameObject.SetActive(false);
        btnNGrab.gameObject.SetActive(false);
        btnDeal.gameObject.SetActive(false);
        btnNDeal.gameObject.SetActive(false);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();

        btnDeal.onClick.RemoveAllListeners();
        btnNDeal.onClick.RemoveAllListeners();
        btnGrab.onClick.RemoveAllListeners();
        btnNGrab.onClick.RemoveAllListeners();
        btnReady.onClick.RemoveAllListeners();
    }

    protected override void readyState()
    {
        base.readyState();
        btnReady.gameObject.SetActive(false);
    }

    private void dealClick()
    {
        //向服务器发送出牌
    }

    private void nDealClick()
    {
        //发送不出牌
    }

    private void grabClick(bool result)
    {
        if (result == true)
        {
            //抢地主
        }
        else
        {
            //不抢地主
        }
    }

    private void readyClick()
    {
        //向服务器发送准备
    }
}

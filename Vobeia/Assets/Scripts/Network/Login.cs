using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KBEngine;
using System;

public class Login : MonoBehaviour {

    KBEMain kbemain;

    private void Awake()
    {
        kbemain = gameObject.GetComponent<KBEMain>();
    }

    // Use this for initialization
    void Start () {
        KBEngine.Event.registerOut("onLoginSuccessfully", this, "onLoginSuccessfully");
        KBEngine.Event.registerOut("onLoginFailed", this, "onLoginFailed");
        StartCoroutine(TryLogin());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator TryLogin()
    {
        while(true)
        {
            Debug.Log("登录中");
            try
            {
                if (!kbemain.gameapp.networkInterface().valid())
                {
                    requestLogin("Robert1", "123456");
                    break;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch
            {
                Debug.LogError("无法连接到服务器，正在尝试重新连接");
            }
            yield return new WaitForSeconds(1.0f);
        }
    }

    public void requestLogin(string name, string password)
    {
        KBEngine.Event.fireIn("login", name, password, System.Text.Encoding.UTF8.GetBytes("PC"));
    }

    public void onLoginSuccessfully(UInt64 rndUUID, Int32 eid, Account accountEntity)
    {
        Debug.Log("登录成功！uuid：" + rndUUID + "id:" + eid + "---end---");
    }

    public void onLoginFailed(UInt16 failedcode)
    {
        if (failedcode == 20)
        {
            Debug.Log("(登陆失败), err=" + KBEngineApp.app.serverErr(failedcode) + ", " + System.Text.Encoding.ASCII.GetString(KBEngineApp.app.serverdatas()));
        }
        else
        {
            Debug.Log("(登陆失败), err=" + KBEngineApp.app.serverErr(failedcode));
        }
    }
}

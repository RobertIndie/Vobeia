using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KBEngine;
using System;

public class Login : MonoBehaviour {

	// Use this for initialization
	void Start () {
        KBEngine.Event.registerOut("onLoginSuccessfully", this, "onLoginSuccessfully");
        KBEngine.Event.registerOut("onLoginFailed", this, "onLoginFailed");
        requestLogin("Robert1", "123456");
	}
	
	// Update is called once per frame
	void Update () {
		
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

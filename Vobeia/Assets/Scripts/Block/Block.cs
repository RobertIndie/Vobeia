using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 基类是一个空气方块
/// </summary>
public class Block : MonoBehaviour {
    /// <summary>
    /// 方块的状态
    /// </summary>
    public byte state = 0;
    /// <summary>
    /// 是否为实体
    /// 实体在生成场景的时候会被实例化
    /// </summary>
    public bool isEntity = true;
    public string blockName = "";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager
{
    public static BlockManager instance = new BlockManager();
    public Dictionary<byte, Type> blockDict = new Dictionary<byte, Type>();
    /// <summary>
    /// 注册方块
    /// </summary>
    /// <param name="id"></param>
    /// <param name="block"></param>
    public void Register<T>(byte id) where T:Block
    {
        blockDict.Add(id, typeof(T));
    }

    public static void RegisterBlock()
    {
        instance.Register<GrassBlock>(1);
    }
}

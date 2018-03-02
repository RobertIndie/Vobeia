using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager
{
    public static BlockManager instance = new BlockManager();
    public Dictionary<byte, Block> blockDict = new Dictionary<byte, Block>();
    public Dictionary<byte, Rect> rectDict = new Dictionary<byte, Rect>();
    /// <summary>
    /// 注册方块
    /// </summary>
    /// <param name="id"></param>
    /// <param name="block"></param>
    public void Register(byte id,Block block)
    {
        blockDict.Add(id, block);
    }

    public static void RegisterBlock()
    {
        //空气方块不需要注册，其id为0
        byte id = 0;
        instance.Register(++id, new GrassBlock());
        instance.Register(++id, new StoneBlock());
        instance.Register(++id, new SandBlock());
    }
    public Material material;
    public int textureCount = 0;//这样就不会频繁使用blockDict.Count了
    public void LoadTexture(Material material)
    {
        material.mainTexture = new Texture2D(0, 0);
        List<Texture2D> textures = new List<Texture2D>();
        textureCount = blockDict.Count;
        foreach(Block block in blockDict.Values)
        {
            string blockName = block.blockName;
            if (blockName!="")
            {
                Material blockMaterial = Resources.Load<Material>("materials/blocks/" + blockName);
                Texture2D blockTexture = blockMaterial.mainTexture as Texture2D;
                textures.Add(blockTexture);
            }
        }
        Texture2D mainTexture = ((Texture2D)material.mainTexture);
        Rect[] rects = mainTexture.PackTextures(textures.ToArray(), 0);
        for (int i = 0; i < rects.Length; i++) 
        {
            if (rects[i] == null) throw new Exception("资源导入失败:" + i);
            rectDict.Add((byte)i, rects[i]);
        }
        mainTexture.filterMode = FilterMode.Point;
        this.material = material;
    }
}

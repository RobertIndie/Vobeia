using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager
{
    public static BlockManager instance = new BlockManager();
    public Dictionary<byte, Block> blockDict = new Dictionary<byte, Block>();
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
    }
    public Material material;
    public int textureCount = 0;//这样就不会频繁使用blockDict.Count了
    public void LoadTexture(Material material)
    {
        material.mainTexture = new Texture2D(0, 0);
        List<Texture2D> textures = new List<Texture2D>();
        int width = blockDict.Count * 16;
        int height = 16;
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
        mainTexture.PackTextures(textures.ToArray(), 0);
        mainTexture.filterMode = FilterMode.Point;
        this.material = material;
    }
}

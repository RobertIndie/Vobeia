using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunksManager : MonoBehaviour {
    public static ChunksManager instance;

    public int chunkWidth = 20;
    public int chunkHeight = 20;
    public int seed = 0;
    public float viewRange = 30;

    public Chunk chunkFab;

    void Awake() {
        Cursor.visible = false;
        instance = this;
        if (seed == 0)
            seed = Random.Range(0, int.MaxValue);
    }

    void Update() {
        for (float x = transform.position.x - viewRange; x < transform.position.x + viewRange; x += chunkWidth)
        {
            for (float z = transform.position.z - viewRange; z < transform.position.z + viewRange; z += chunkWidth)
            {

                Vector3 pos = new Vector3(x, 0, z);
                pos.x = Mathf.Floor(pos.x / (float)chunkWidth) * chunkWidth;
                pos.z = Mathf.Floor(pos.z / (float)chunkWidth) * chunkWidth;

                Chunk chunk = Chunk.FindChunk(pos);
                if (chunk != null) continue;

                chunk = (Chunk)Instantiate(chunkFab, pos, Quaternion.identity);

            }
        }
    }
}

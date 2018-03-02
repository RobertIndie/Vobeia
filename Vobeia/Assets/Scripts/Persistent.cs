using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persistent : MonoBehaviour {
    public Material sourceMaterial;
    private void Awake()
    {
        DontDestroyOnLoad(this);
        BlockManager.RegisterBlock();
        BlockManager.instance.LoadTexture(sourceMaterial);
    }

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class MaterialScroll : MonoBehaviour
{
    private Material _rend;
    
    void Start()
    {
        _rend = GetComponent<MeshRenderer>().sharedMaterial;
    }

    void Update()
    {
        if (FindObjectOfType<PlayerMovement>().isDead)
        {
            _rend.SetFloat("_speedTexture", 0.0f);
        }else{
            _rend.SetFloat("_speedTexture", 0.06f);
        }
    }
}

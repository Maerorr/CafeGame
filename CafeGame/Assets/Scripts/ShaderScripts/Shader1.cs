using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shader1 : MonoBehaviour
{
    Material material;
    public Texture2D tex;
    public float cell_density;
    public float angle_offset;
    public float speed;
    public float strength;
    
    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
    }
    
}

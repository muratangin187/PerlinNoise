using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Noise : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private Texture2D _texture;
    private bool _mode = false;
    public float step = 0.005f;
    public float scale = 5f;
    private float _xoff = 0;
    private float _yoff = 0;
    void Start()
    {
        _meshRenderer = gameObject.GetComponent<MeshRenderer>();
        _texture = new Texture2D(128, 128);
        _meshRenderer.material.mainTexture = _texture;
        RandomTextureGenerator();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RandomTextureGenerator();
            _mode = false;
        }else if (Input.GetMouseButtonDown(1))
        {
            _mode = true;
        }

        _xoff = _xoff+Input.GetAxis("Horizontal");
        _yoff = _yoff+Input.GetAxis("Vertical");
        
        if (_mode)
        {
            if(Input.mouseScrollDelta.y != 0.0f)
                scale += Input.mouseScrollDelta.y > 0 ? step : -step; 
            PerlinNoiseTextureGenerator();
        }
    }

    void RandomTextureGenerator()
    {
        for (int y = 0; y < _texture.height; y++)
        {
            for (int x = 0; x < _texture.width; x++)
            {
                Color color = Random.Range(0.0f, 1.0f) > 0.5f ? Color.black : Color.white;
                _texture.SetPixel(x, y, color);
            }
        }
        _texture.Apply();
    }

    void PerlinNoiseTextureGenerator()
    {
        for (int y = 0; y < _texture.height; y++)
        {
            for (int x = 0; x < _texture.width; x++)
            {
                Color color = Mathf.PerlinNoise((x+_xoff)*scale,(y+_yoff)*scale) > 0.5f ? Color.black : Color.white;
                _texture.SetPixel(x, y, color);
            }
        }
        _texture.Apply();
    }
}

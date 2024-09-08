using System;
using UnityEngine;

public class ScreenFiter : MonoBehaviour
{
    [SerializeField] private float scale;

    private const float perfectAspect = 16f / 9f;
    
    private void Update()
    {
        float aspect = (float)Screen.width / Screen.height;

        if (aspect > perfectAspect)
        {
            aspect = perfectAspect;
        }
        
        float width = Camera.main.orthographicSize * 2.0f * aspect;
        transform.localScale = new Vector3(width / scale, width / scale, width / scale);
    }
}
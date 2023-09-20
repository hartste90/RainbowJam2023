using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerProjectileController : ProjectileController
{
    public Color[] colors = new Color[]
    {
        new Color(228, 3, 3), 
        new Color(255, 140, 0), 
        new Color(255, 237, 0), 
        new Color(0, 128, 38), 
        new Color(0, 77, 255), 
        new Color(117, 7, 135)
    };
    public SpriteRenderer spriteRenderer;
    // Red	HEX = #E40303	RGB = 228 3 3	CMYK = 0, 99, 99, 11
    // Orange	HEX = #FF8C00	RGB = 255 140 0	CMYK = 0, 45, 100, 0
    // Yellow	HEX = #FFED00	RGB = 255 237 0	CMYK = 0, 7, 100, 0
    // Green	HEX = #008026	RGB = 0 128 38	CMYK = 100, 0, 70, 50
    // Blue	HEX = #24408E	RGB = 0 77 255	CMYK = 75, 55, 0, 44
    // Purple/Violet	HEX = #732982	RGB = 117 7 135
    public void SetColor(int colorIdx)
    {
        spriteRenderer.color = colors[colorIdx%colors.Length];
    }
}

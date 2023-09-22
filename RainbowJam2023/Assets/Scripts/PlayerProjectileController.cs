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

    public void SetColor(int colorIdx)
    {
        spriteRenderer.color = colors[colorIdx%colors.Length];
    }
}

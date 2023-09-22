using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GameCompletedModalController : DialogueModalController
{
    public override void Show()
    {
        gameObject.SetActive(true);
        canvasGroup.alpha = 1;
        canvasGroup.DOFade(1f, .2f);
    }

    public override void Hide()
    {
        canvasGroup.DOFade(0, .1f).OnComplete(() => gameObject.SetActive(false));
    }
}

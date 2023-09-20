using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]

public class ModalBase : MonoBehaviour
{
    CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        canvasGroup.alpha = 1;
        canvasGroup.DOFade(1f, .2f)
            .OnComplete(()=> 
                GameManager.Instance.PauseGame());
        

    }
    public void Hide()
    {
        GameManager.Instance.UnpauseGame();
        canvasGroup.DOFade(0, .1f).OnComplete(() => gameObject.SetActive(false));
    }
}

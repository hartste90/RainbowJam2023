using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]

public class ModalBase : MonoBehaviour
{
    protected CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.DOFade(1f, .25f)
            .OnComplete(() =>
            {
                canvasGroup.interactable = true;
                GameManager.Instance.PauseGame();
            });
    }
    public virtual void Hide()
    {
        GameManager.Instance.UnpauseGame();
        canvasGroup.interactable = false;
        canvasGroup.DOFade(0, .2f).OnComplete(() => gameObject.SetActive(false));
    }
}

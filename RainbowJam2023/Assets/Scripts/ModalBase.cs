using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModalBase : MonoBehaviour
{
    public void Show()
    {
        gameObject.SetActive(true);
        GameManager.Instance.PauseGame();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        GameManager.Instance.UnpauseGame();
    }
}

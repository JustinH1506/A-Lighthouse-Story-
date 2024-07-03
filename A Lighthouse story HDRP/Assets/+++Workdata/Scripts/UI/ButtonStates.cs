using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonStates : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
{

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        MusicManager.Instance.PlayUISFX(MusicManager.Instance.buttonPress);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        MusicManager.Instance.PlayUISFX(MusicManager.Instance.buttonHover);
    }

    private void SetInteractable()
    {
        button.interactable = false;
        StartCoroutine(ResetInteractable());
    }

    private IEnumerator ResetInteractable()
    {
        yield return new WaitForSeconds(0.2f);
        button.interactable = true;
    }
}

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
    
    /// <summary>
    /// plays button press sound
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        MusicManager.Instance.PlayUISFX(MusicManager.Instance.buttonPress);
    }

    /// <summary>
    /// plays button hover sound
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        MusicManager.Instance.PlayUISFX(MusicManager.Instance.buttonHover);
    }

    /// <summary>
    /// deactivate interactable of button and set it back active after 0.2 seconds
    /// </summary>
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

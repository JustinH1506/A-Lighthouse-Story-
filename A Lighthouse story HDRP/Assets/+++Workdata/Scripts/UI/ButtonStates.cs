using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonStates : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 defaultScale = new Vector3(1, 1, 1);
    private Vector3 hoveredScale = new Vector3(1.5f, 1.5f, 1.5f);
    private Vector3 pressedScale = new Vector3(0.8f, 0.8f, 0.8f);

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.localScale = pressedScale;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localScale = defaultScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = hoveredScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = defaultScale;
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

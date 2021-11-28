using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CartelInformativo : MonoBehaviour
{
    [SerializeField] private RectTransform canvasPanelInformativo;
    [SerializeField] private TMP_Text textoInformativo;

    private void Awake()
    {
        canvasPanelInformativo.GetComponent<CanvasGroup>().alpha = 0.0f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        canvasPanelInformativo.GetComponent<CanvasGroup>().alpha = 1.0f;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        canvasPanelInformativo.GetComponent<CanvasGroup>().alpha = 0.0f;
    }

}

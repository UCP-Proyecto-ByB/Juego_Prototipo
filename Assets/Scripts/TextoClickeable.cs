using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class TextoClickeable : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int cantidadFrutas = 4;

    private Mouse currentMouse = Mouse.current;
    private TextMeshProUGUI texto;

    private void Awake()
    {
        texto = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        texto.text = $"Recolectar al menos <color=\"red\"><b>{cantidadFrutas}</b></color> frutas de <i><u><b><color=#8673A1><size=120%><link=\"fruta_A\">Clase A</link></i></u></b>";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Vector2 mousePos = currentMouse.position.ReadValue();

        if (eventData.button == PointerEventData.InputButton.Left)
        {

            int indiceLink = TMP_TextUtilities.FindIntersectingLink(texto, mousePos, null);

            if (indiceLink > -1)
            {
                //Debug.Log(mousePos);
                var idLink = texto.textInfo.linkInfo[indiceLink].GetLinkID();
                //var idLink = infoLink.GetLinkId;

                Debug.Log($"ID: {idLink}");
                // //Debug.Log($"id {idLink}");
            }


        }
    }

    private void Update()
    {
        //mobile
        // if (!Touchscreen.current.primaryTouch.press.isPressed) { return; }
        // Vector2 touchPos = Touchscreen.current.primaryTouch.position.ReadValue();

        // Debug.Log(touchPos);
        //if (!currentMouse.leftButton.isPressed) { return; }





    }
}
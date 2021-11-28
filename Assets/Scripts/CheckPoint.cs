using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private RectTransform panel;

    private bool playerDetectado = false;

    private void Start()
    {
        panel.GetComponent<CanvasGroup>().alpha = 0.0f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!playerDetectado)
        {
            GetComponent<AudioSource>().Play();
            playerDetectado = true;
            Player player = other.GetComponent<Player>();
            player.AsignarPosRespawn(transform.position, true);
            panel.GetComponent<CanvasGroup>().alpha = 1.0f;
            Invoke(nameof(Destruirse), 0.5f);
        }
    }

    private void Destruirse()
    {
        panel.GetComponent<CanvasGroup>().alpha = 0.0f;
        Destroy(gameObject);
    }
}

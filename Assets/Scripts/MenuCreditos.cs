using UnityEngine;

public class MenuCreditos : MonoBehaviour
{
    public void Regresar()
    {
        gameObject.SetActive(false);
    }

    public void Mostrar()
    {
        gameObject.SetActive(true);
    }
}
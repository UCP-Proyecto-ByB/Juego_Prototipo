using UnityEngine;

public class CanvasInfo : MonoBehaviour
{
    [SerializeField] private GameObject[] canvas;

    public void MostrarCanvas(string idPanel)
    {
        foreach (var item in canvas)
        {
            if (item.name == idPanel)
            {
                Instantiate(item);
                break;
            }
        }
    }
}
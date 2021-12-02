using UnityEngine;

public class PanelInfo : MonoBehaviour
{
    [SerializeField] private string id = "id";

    private GameObject canvasObjetivos;

    public string Id { get => id; set => id = value; }

    private void Awake()
    {
        canvasObjetivos = GameObject.Find("CanvasObjetivos");
    }

    private void OnEnable()
    {
        canvasObjetivos.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        canvasObjetivos.SetActive(true);
        Destroy(transform.parent.gameObject);
    }

    public void Cerrar()
    {
        this.gameObject.SetActive(false);
    }
}
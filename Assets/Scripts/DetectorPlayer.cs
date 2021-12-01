using UnityEngine;

public class DetectorPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        transform.GetComponentInParent<EnemigoSuicida>().Atacar(other.gameObject.transform.position);
        Destroy(gameObject);
    }
}
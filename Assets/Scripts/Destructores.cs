using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructores : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.GetComponent<Player>().Morir();
    }
}

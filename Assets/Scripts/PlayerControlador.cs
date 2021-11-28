using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlador : MonoBehaviour
{
    [SerializeField] Animator controladorAnimaciones;

    private void Update()
    {
        //controladorAnimaciones.SetFloat("horizontal", Mathf.Abs(Input.GetAxis("Horizontal")));
        //controladorAnimaciones.SetFloat("vertical", Input.GetAxis("Vertical"));
    }
}

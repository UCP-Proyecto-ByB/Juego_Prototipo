using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControladorSonidos : MonoBehaviour
{
    [System.Serializable]
    private class Sonidos
    {
        [SerializeField] private string nombreSonido;
        [SerializeField] private AudioClip audioClip;

        public string NombreSonido { get => nombreSonido; set => nombreSonido = value; }
        public AudioClip AudioClip { get => audioClip; set => audioClip = value; }
    }

    [SerializeField] private List<Sonidos> sonidos;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySaltar()
    {
        audioSource.clip = sonidos[0].AudioClip;
        audioSource.Play();
    }

    public void PlayDanio()
    {
        audioSource.clip = sonidos[1].AudioClip;
        audioSource.Play();
    }

    public void PlayMuerte()
    {
        audioSource.clip = sonidos[2].AudioClip;
        audioSource.Play();
    }
}

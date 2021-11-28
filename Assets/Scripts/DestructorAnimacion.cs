using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructorAnimacion : MonoBehaviour
{
    public void DestruirObjetoPadre()
    {
        Destroy(transform.parent.gameObject);
    }
}

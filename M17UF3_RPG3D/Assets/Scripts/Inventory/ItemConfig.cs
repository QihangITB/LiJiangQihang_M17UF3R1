using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemConfig : MonoBehaviour
{
    public GameObject particle;
    public Vector3 velocidadRotacion = new Vector3(0f, 50f, 0f);

    void Update()
    {
        if (transform.parent == null)
        {
            particle.SetActive(true);
            transform.Rotate(velocidadRotacion * Time.deltaTime);
        }
        else
        {
            particle.SetActive(false);
        }
    }
}

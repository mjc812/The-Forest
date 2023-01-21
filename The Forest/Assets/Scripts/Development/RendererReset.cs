using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RendererReset : MonoBehaviour
{
    void Start()
    {
        foreach (Transform t in transform.GetComponentsInChildren<Transform>())
        {
            if (t.gameObject.GetComponent<MeshRenderer>() != null) {
                t.gameObject.GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }
}

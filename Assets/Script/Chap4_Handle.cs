using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chap4_Handle : MonoBehaviour
{
    [SerializeField] GameObject enableGameobject1;
    [SerializeField] GameObject enableGameobject2;
    private void OnEnable()
    {
        enableGameobject1.SetActive(true);
        enableGameobject2.SetActive(true);
    }

    private void OnDisable()
    {
        enableGameobject1.SetActive(false);
        enableGameobject2.SetActive(true);
    }
}

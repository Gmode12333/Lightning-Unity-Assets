using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HandleEnableMultipleObject : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> objects = new List<GameObject>();
    

    public void Handle(int index)
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (i != index)
            {
                objects[i].SetActive(false);
            }
        }
    }
}

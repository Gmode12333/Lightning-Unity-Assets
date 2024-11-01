using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMaterials : MonoBehaviour
{
    public Material default_material;
    public Material change_material;
    
    public void ChangeMaterials()
    {
        GetComponent<MeshRenderer>().material = change_material;
    }

    public void DefaultMaterials()
    {
        GetComponent<MeshRenderer>().material = default_material;
    }
}

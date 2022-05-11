using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMaterialSetter : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer mesh;
    [SerializeField] private  List<Material> material;
    private void OnEnable()
    {
        mesh.material = material[PlayerPrefs.GetInt("SkinEquip")];
    }
}

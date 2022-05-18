using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMaterialSetter : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer mesh;
    [SerializeField] private  List<Material> material;
    [SerializeField] private GameObject fxGolden;
    private void OnEnable()
    {
        mesh.material = material[PlayerPrefs.GetInt("SkinEquip")];

        if (PlayerPrefs.GetInt("SkinEquip") == 5)
        {
            fxGolden.SetActive(true);
        }
        else
        {
            fxGolden.SetActive(false);
        }
    }
}

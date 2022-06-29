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
        Debug.Log("");

        if (PlayerPrefs.GetInt("SkinEquip") == 5)
        {
            fxGolden.SetActive(true);
            fxGolden.GetComponent<ParticleSystem>().Play();
        }
        else
        {
            fxGolden.SetActive(false);
        }
    }
}

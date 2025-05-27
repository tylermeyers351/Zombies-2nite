using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeZombie_EyesGlow : MonoBehaviour
{
    public Material[] BodyMaterials = new Material[1];

    public enum EyesGlow
    {
        No,
        Yes
    }

    public EyesGlow eyesGlow;

    void Awake()
    {
        UpdateGlow();
    }

    public void UpdateGlow()
    {
        if (eyesGlow == EyesGlow.No)
        {
            BodyMaterials[0].DisableKeyword("_EMISSION");
            BodyMaterials[0].SetFloat("_EmissiveExposureWeight", 1);
        }
        else
        {
            BodyMaterials[0].EnableKeyword("_EMISSION");
            BodyMaterials[0].SetFloat("_EmissiveExposureWeight", 0);
        }
    }
}

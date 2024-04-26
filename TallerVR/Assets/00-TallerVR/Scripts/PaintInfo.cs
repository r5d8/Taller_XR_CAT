using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintInfo : MonoBehaviour
{
    // Start is called before the first frame update
    public Material BaseMaterial;
    public Color paintColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    // public float metallic = 0.5f;
    // public float smoothness = 0.5f;
    // public Color emission = new Color(0.0f, 0.0f, 0.0f, 0.0f);
    private MeshRenderer mr;
    
    public void ChangePaintMaterial(Material mat)
    {
        Material[] mats = mr.materials;
        mats[1] = mat;
        mr.materials = mats;
    }

    public Material GetPaintMaterial()
    {
        return BaseMaterial;
    }

    public Material GetInstancePaintMaterial()
    {
        return new Material(BaseMaterial);
    }

    void Start()
    {
        mr = this.GetComponent<MeshRenderer>();
        
        if (!BaseMaterial) BaseMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        else BaseMaterial = new Material(BaseMaterial);

        BaseMaterial.color = paintColor;
        ChangePaintMaterial(BaseMaterial);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AirDraw : MonoBehaviour
{
    public InputActionReference paintButton;

    private GameObject drawing = null;
    private GameManager GM;
    private Material BaseMaterial;

    void Start()
    {
        GM = GameObject.Find("GM").GetComponent<GameManager>();

        BaseMaterial = new Material(Shader.Find("Universal Render Pipeline/Unlit"));
        BaseMaterial.color = new Color(0,0,0,1);
        paintButton.action.Enable();
        paintButton.action.performed += (ctx) => { StartDrawingLine(); };
        paintButton.action.canceled += (ctx) => { EndDrawingLine(); };
    }

    void StartDrawingLine()
    {
        //Create Game Object and component
        drawing = new GameObject();
        drawing.transform.position = this.transform.position;
        TrailRenderer drawComponent = drawing.AddComponent<TrailRenderer>();
        
        //Configure component
        drawComponent.time = 100000;
        drawComponent.widthMultiplier = 0.015f;
        drawComponent.material = new Material(BaseMaterial);
        //drawComponent.material.color = new Color(0.0f, 1.0f, 0.8f, 1.0f); //Color(r, g, b, a);

        //Add the new object as a child of the owner of this component,
        //so when it moves, the line is shown.
        drawing.transform.parent = this.transform;

    }

    void EndDrawingLine()
    {
        //Stop drawing, by releasing the child
        drawing.transform.parent = null;

        //Inform the Game Manager that a new drawing has appeared
        if (drawing.GetComponent<TrailRenderer>().positionCount > 1) 
        {
            drawing.GetComponent<TrailRenderer>().AddPosition(transform.position);
            GM.AddDrawing(drawing);
        }
        else Destroy(drawing);

        //Release the variable holding the child
        drawing = null;
    }

    void OnTriggerEnter(Collider other)
    {
        PaintInfo info = other.GetComponent<PaintInfo>();
        if (info != null)
        {
            /***********************************************************************
            * Get the material stored at PaintInfo
            ***********************************************************************/
            BaseMaterial.color = info.GetPaintMaterial().color;
            
            /***********************************************************************
            ***********************************************************************/
        }
    }
}
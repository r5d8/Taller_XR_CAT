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

        BaseMaterial = new Material((Material)Resources.Load("MAT_UnlitURP", typeof(Material)));//Shader.Find("Universal Render Pipeline/Unlit"));
        BaseMaterial.color = new Color(0,0,0,1);
        paintButton.action.Enable();
        paintButton.action.performed += (ctx) => { StartDrawingLine(); };
        paintButton.action.canceled += (ctx) => { EndDrawingLine(); };
    }

    void StartDrawingLine()
    {

    }

    void EndDrawingLine()
    {
        if (!drawing) return;
        
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
        
    }
}

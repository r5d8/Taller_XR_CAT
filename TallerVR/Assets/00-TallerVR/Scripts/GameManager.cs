using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class GameManager : MonoBehaviour
{
    public int maxPaintings = 20;
    public bool kinematicDrawings = true;
    private int elemCount = 0;
    private Queue<GameObject> drawings = new Queue<GameObject>();
    
    private int DrawLayer;

    // Start is called before the first frame update
    void Start()
    {
        DrawLayer = LayerMask.NameToLayer("Drawings");
    }

    //Pre: dr has a TrailRenderer component
    //Post: an object with a LineRenderer component is created, and it takes the shape of the input
    private GameObject MakeDrawingStatic(GameObject dr)
    {
        GameObject staticDrawing = new GameObject();
        staticDrawing.layer = DrawLayer;
        staticDrawing.transform.position = dr.transform.position;
        TrailRenderer tr = dr.GetComponent<TrailRenderer>();
        LineRenderer lr = staticDrawing.AddComponent<LineRenderer>();
        
        //Copy important attributes
        lr.widthMultiplier = tr.widthMultiplier;
        lr.material = tr.material;

        Vector3[] positions = new Vector3[tr.positionCount];
        int nbPos = tr.GetPositions(positions); 
        for (int i = 0; i < nbPos; ++i)
        {
            positions[i] -= dr.transform.position;
        }
        lr.positionCount = tr.positionCount;
        lr.SetPositions(positions);

        //Transform all points to Local space. This way, if you move the
        //object, the line rendered also moves.
        lr.useWorldSpace = false;
        
        
        
        /***********************************************************************
        * Add the creation of the collider and the addition of the Grab comp.
        ***********************************************************************/
        
        
        /***********************************************************************
        ***********************************************************************/

        return staticDrawing;
    }

    //Pre: dr has a TrailRenderer component
    //Post: creates a static drawing, and adds it to the list of drawing.
    //      If the number of drawings is greater than maxPaintings,
    //      the first painting created is erased.
    public void AddDrawing(GameObject dr)
    {
        drawings.Enqueue(MakeDrawingStatic(dr));
        Destroy(dr);

        elemCount += 1;
        if (elemCount > maxPaintings) 
        {
            GameObject tobedestroyed = drawings.Dequeue();
            Destroy(tobedestroyed);
            elemCount -= 1;
        }
    }
}

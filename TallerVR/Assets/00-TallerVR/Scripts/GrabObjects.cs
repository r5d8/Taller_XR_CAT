using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrabObjects : MonoBehaviour
{
    public InputActionReference grabButton;
    private List<GameObject> grabables = new List<GameObject>();
    private GameObject grabbedObject;

    // Start is called before the first frame update
    void Start()
    {
        grabButton.action.Enable();
        grabButton.action.performed += (ctx) =>
        {
            if (grabables.Count > 0) {
                grabbedObject = grabables[0];
                //grabbedObject.transform.SetParent(gameObject.transform);
                grabbedObject.GetComponent<Grabbable>().addMaster(gameObject);
            }
            
        };

        grabButton.action.canceled += (ctx) =>
        {
            if (grabbedObject != null) {
                //grabbedObject.transform.SetParent(null);
                grabbedObject.GetComponent<Grabbable>().removeMaster(gameObject);
                grabbedObject = null;
                
            }
            
        };
    }

    void OnTriggerEnter(Collider other) {
        if (other.GetComponent<Grabbable>()) {
            grabables.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.GetComponent<Grabbable>()) {
            grabables.Remove(other.gameObject);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    private List<GameObject> masters = new List<GameObject>();
    private List<Quaternion> rots = new List<Quaternion>();
    private List<Vector3> poss = new List<Vector3>();

    //public bool quatDouble = false;
    private int countMasters = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (countMasters > 0) {           
            for (int i = 0; i < countMasters; ++i) {
                GameObject master = masters[i];
                
                //Transform position by movement
                transform.position += master.transform.position - poss[i];
                
                //Transform position and rotation by rotation
                Quaternion deltaRot = master.transform.rotation * Quaternion.Inverse(rots[i]);
                //if (EnableDoubleRotation.doubleRotation) deltaRot = deltaRot*deltaRot;
                Vector3 relativePos = (transform.position - master.transform.position);

                transform.position += deltaRot * relativePos - relativePos;
                
                transform.rotation = deltaRot * transform.rotation;
                
                poss[i] = master.transform.position;
                rots[i] = master.transform.rotation;

            }
        }
        //Debug.Log(masters.Count);
    }

    public void addMaster(GameObject master) {
        masters.Add(master);
        rots.Add(master.transform.rotation);
        poss.Add(master.transform.position);
        countMasters++;
    }

    public void removeMaster(GameObject master) {
        int loc = 0;
        for (int i = 0; i < countMasters; ++i) {
            if (master == masters[i]) loc = i;
        }

        masters.RemoveAt(loc);
        rots.RemoveAt(loc);
        poss.RemoveAt(loc);
        countMasters--;
    }
}
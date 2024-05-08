using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeNet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        other.transform.position = new Vector3(Random.Range(-12.5f, 2.5f), 10.0f, Random.Range(-2.5f, 2.5f));
    }

    void OnCollisionEnter(Collision other)
    {
        other.transform.position = new Vector3(Random.Range(-12.5f, 2.5f), 10.0f, Random.Range(-2.5f, 2.5f));
    }
}

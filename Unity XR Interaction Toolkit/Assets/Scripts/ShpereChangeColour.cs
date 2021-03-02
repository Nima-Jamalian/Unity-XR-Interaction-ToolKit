using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShpereChangeColour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChnageColourToBlue()
    {
        GetComponent<MeshRenderer>().material.color = Color.blue;
    }
}

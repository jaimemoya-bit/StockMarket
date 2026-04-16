using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mirarcaja : MonoBehaviour
{
    public bool yaGiro;
       
    

    // Update is called once per frame
    void Update()
    {
        if (!yaGiro)
    {
        transform.Rotate(0, 90, 0);
        yaGiro = true;
    }
    }

}

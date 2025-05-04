using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class FlipperScript : MonoBehaviour
{
    public float restPosition = 0f;
    public float pressedPosition = 45f;
    public float hitStrength = 10000f;
    public float flipperDamper = 150f;

    public string inputName;


    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetAxis(inputName) == 1) {

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is adapted from: https://gamedev.stackexchange.com/questions/96878/how-to-animate-objects-with-bobbing-up-and-down-motion-in-unity
//Script to move the in-game pickups using a "bobbing" effect

public class PickupBob : MonoBehaviour {

    public float startPos;
    public float floatStrength = 0.2f;

    void Start()
    {
        this.startPos = this.transform.position.y;
    }

    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x,
            startPos + ((float)Mathf.Sin(3.5f*(Time.time))) * floatStrength, transform.position.z);
    }
}

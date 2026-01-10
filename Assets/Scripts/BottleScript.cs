using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleScript : MonoBehaviour
{
    public List<Rigidbody> bottleParts = new List<Rigidbody>();

    public void Shatter()
    {
        foreach (Rigidbody part in bottleParts)
        {
            part.isKinematic = false; 
        }
    }
}
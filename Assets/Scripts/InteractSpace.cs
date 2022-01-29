using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractSpace : MonoBehaviour {
 
    public List<Collider> colliders = new List<Collider>();

    public List<Collider> GetColliders()
    {
        // Protects missing gameobjects possibly?
        return colliders.ToList();
    }
 
    private void OnTriggerEnter (Collider other) {
        if (!colliders.Contains(other)) { colliders.Add(other); }
    }
 
    private void OnTriggerExit (Collider other) {
        colliders.Remove(other);
    }
 
}
using System;
using System.Collections;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public GameObject itemParent;
    public InteractSpace interactCollider;

    public void Start()
    {
        itemParent = transform.Find("ItemPosition").gameObject;
        interactCollider = transform.Find("InteractSpace").GetComponent<InteractSpace>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            AttemptInteract();
    }

    private void AttemptInteract()
    {
        Interactable interactScript = null;
        foreach (var collider in interactCollider.GetColliders())
        {
            if (null == collider)
                continue;
            interactScript = collider.GetComponent<Interactable>();
            if (null != interactScript)
                break;
        }
        if (null != interactScript)
            interactScript.Interact(this);
    }

    public GameObject GetHeldItem()
    {
        if (itemParent.transform.childCount > 0)
            return itemParent.transform.GetChild(0).gameObject;
        return null;
    }

}

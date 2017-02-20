using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] float reachRange;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
            {
                if (Vector3.Distance(transform.position, hit.transform.position) <= reachRange)
                {

                    IInteract interactObject = hit.transform.GetComponent<IInteract>();
                    if (interactObject != null)
                    {
                        interactObject.OnInteract(this);
                    }
                }
            }
        }
    }
}

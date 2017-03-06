using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] float reachRange;

    Transform camera;

    // Use this for initialization
    void Start()
    {
        camera = GetComponentInChildren<CameraMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

            RaycastHit hit;
            if (Physics.Raycast(transform.position, camera.forward, out hit, Mathf.Infinity))
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

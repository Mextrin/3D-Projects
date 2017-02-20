using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetInObject : MonoBehaviour, IInteract
{
    FollowCamera camera;

    [SerializeField]
    Transform center, rotation;

    [SerializeField]
    Transform allSeats;
    List<Transform> seats = new List<Transform>();

    private void Start()
    {
        camera = (FollowCamera)FindObjectOfType(typeof(FollowCamera));

        if (allSeats != null)
        {
            foreach (Transform seat in allSeats)
            {
                seats.Add(seat);
            }
        }
    }

    public void OnInteract(Interaction interactor)
    {

        //Set character position
        Destroy(interactor.GetComponent<CharacterController>());

        Transform closestSeat = transform;
        float seatDistance = Mathf.Infinity;
        for (int i = 0; i < seats.Count; i++)
        {
            float distanceToPlayer = Vector3.Distance(interactor.transform.position, seats[i].position);

            if (distanceToPlayer < seatDistance)
            {
                seatDistance = distanceToPlayer;
                closestSeat = seats[i];
            }
        }

        interactor.transform.position = closestSeat.position;
        interactor.transform.parent = closestSeat;

        //Set Camera position
        if (center == null)
        {
            //Movement
            camera.moveTarget = transform;
            camera.transform.parent = transform;
            //camera.transform.localPosition = transform.position;
        }

        else
        {
            //Rotation
            if (rotation == null)
            {
                camera.rotationTarget = center;
            }

            //Movement
            camera.moveTarget = center;
            camera.transform.parent = center.parent;
            //camera.transform.localPosition = cameraPoint.localPosition;
        }

        if (rotation != null) 
        {
            camera.rotationTarget = rotation;
        }
        else
        {
            camera.rotationTarget = transform;
        }
        
    }
}

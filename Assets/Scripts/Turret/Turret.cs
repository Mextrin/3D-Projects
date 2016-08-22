using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Turret : MonoBehaviour
{
    public float rotationSpeed;
    public List<GameObject> targets = new List<GameObject>();
    public List<float> distances = new List<float>();

    public float shotsPerSec;
    public bool stayOnTarget;

    public Shooter cannon;

    // Use this for initialization
    void Start()
    {
        cannon = GetComponentInChildren<Shooter>();
    }

    // Update is called once per frame
    void Update()
    {


        if (targets.Count > 0)
        {
            for (int i = 0; i < targets.Count; i++)
            {
                distances[i] = Vector3.Distance(transform.position, targets[i].transform.position);
            }

            if (targets.Count > 1)
            {
                if (!stayOnTarget)
                    Sort();
            }

            /* * targets[0].GetComponent<TargetMovementBasic>().speed) / shotSpeed);*/

            var targetRotation = Quaternion.LookRotation(targets[0].transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            //Vector3 relativeTargetPosition = targets[0].transform.position - transform.position;
            //Vector3 direction = transform.forward;

            //Debug.Log(Vector3.Angle(transform.position, targets[0].transform.position));
            if (Physics.Raycast(transform.position, transform.forward, GetComponents<SphereCollider>()[1].radius))
            //if (Vector3.Angle(relativeTargetPosition, direction) < 10f)
                cannon.shoot = true;
            else
                cannon.shoot = false;
        }

        else if (targets.Count == 0 && cannon.shoot)
            cannon.shoot = false;
    }

    void Sort()
    {
        int unsortedNumbers;

        do
        {
            int previousNumberPosition = 0;
            unsortedNumbers = 0;

            for (int i = 0; i < distances.Count; i++)
            {
                if (distances[i] < distances[previousNumberPosition])
                {
                    GameObject targetChange = targets[i];
                    float distanceChange = distances[i];
                    distances[i] = distances[previousNumberPosition];
                    distances[previousNumberPosition] = distanceChange;
                    targets[i] = targets[previousNumberPosition];
                    targets[previousNumberPosition] = targetChange;
                    unsortedNumbers++;
                }

                previousNumberPosition = i;
            }

        } while (unsortedNumbers != 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targets.Add(other.gameObject);
            distances.Add(0);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targets.Remove(other.gameObject);
            distances.RemoveAt(0);
            Sort();
        }

    }
}

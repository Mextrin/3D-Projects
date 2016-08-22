using UnityEngine;
using System.Collections;

public class weaponControl : MonoBehaviour
{

    public Transform spawner;

	void Update ()
    {
        transform.position = spawner.transform.position;
        transform.rotation = spawner.transform.rotation;
	}
}

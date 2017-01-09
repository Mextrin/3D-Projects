using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.gameObject.transform);
        var lookRotation = Quaternion.LookRotation(transform.position - Camera.main.gameObject.transform.position);
        transform.eulerAngles = new Vector3(0, lookRotation.eulerAngles.y - 180f, 0);
        
    }
}

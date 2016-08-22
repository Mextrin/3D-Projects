using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour
{
    public Text healthUI;

    public float startHealth;
    public float health;

    // Use this for initialization
    void Start()
    {
        health = startHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (health > 0)
            healthUI.text = health.ToString();
        else
            Destroy(gameObject);
    }
}

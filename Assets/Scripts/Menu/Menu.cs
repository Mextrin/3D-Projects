using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public bool isCurrent;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator != null)
        {
            if (animator.GetBool("isVisible") != isCurrent)
            {
                animator.SetBool("isVisible", isCurrent);
            }
        }
    }
}

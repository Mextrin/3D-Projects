using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOption : MonoBehaviour
{
    public Transform onEnter;
    public Transform onUp;
    public Transform onDown;
    public Transform onLeft;
    public Transform onRight;
    public Transform cameraPoint;
    public bool selected;

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (animator != null)
        {

            if (animator.GetBool("isSelected") != selected)
            {
                animator.SetBool("isSelected", selected);
            }
        }
    }
}

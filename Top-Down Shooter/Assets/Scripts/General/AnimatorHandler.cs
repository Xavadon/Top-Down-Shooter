using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorHandler : MonoBehaviour
{
    public Animator animator;

    public void PlayTargetAnimation(string name, bool isInteracting)
    {
        animator.SetBool("isInteracting", isInteracting);
        animator.CrossFade(name, 0.1f);
    }

    public void PlayTargetAnimation(string name)
    {
        animator.CrossFade(name, 0.1f);
    }
}

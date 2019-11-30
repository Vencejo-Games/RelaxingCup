using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffee : MonoBehaviour
{
    [SerializeField] private string type;

    [SerializeField] private AnimationClip[] animations;

    protected Animator animator;
    protected AnimatorOverrideController animatorOverrideController;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = animatorOverrideController;
    }

    // Start is called before the first frame update
    void Start()
    {
        int i = 2;
        animatorOverrideController["coffee"] = animations[i];
    }

}

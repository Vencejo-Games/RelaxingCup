using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffee : MonoBehaviour
{
    [SerializeField] public int id;
    [SerializeField] private AnimationClip[] animations;
    [SerializeField] private Sprite[] sprites;

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
        animatorOverrideController["coffee"] = animations[id];
    }

    public Sprite getSprite(int i) {
        return sprites[i];
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedalIndicatorItem : PullObject<MedalIndicatorItem>
{
    [SerializeField] private Animator animator;

    public void Play()
    {
        animator.Play("Default");

        this.OnAnimation(animator, "Show", OnAnimationShowed, 0.99f);
        animator.SetTrigger("Show");
    }

    void OnAnimationShowed()
    {
        UnpullThis();
    }
}

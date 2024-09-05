using System;
using System.Collections;
using UnityEngine;

public static class Utility
{
    public static void Invoke(this MonoBehaviour monoBehaviour, GameAction action, float delay)
    {
        monoBehaviour.StartCoroutine(Invoking(action, delay));
    }

    private static IEnumerator Invoking(GameAction action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }

    public static void OnAnimation(this MonoBehaviour monoBehaviour, Animator animator, string name, GameAction call, float targetTime)
    {
        monoBehaviour.StartCoroutine(AnimationWatch(animator, name, call, targetTime));
    }

    static IEnumerator AnimationWatch(Animator animator, string name, GameAction call, float targetTime)
    {
        bool isAnimation = false;
        while (!isAnimation)
        {
            isAnimation = animator.GetCurrentAnimatorStateInfo(0).IsName(name);
            yield return null;
        }

        isAnimation = true;
        while (isAnimation)
        {
            AnimatorStateInfo animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
            isAnimation = animatorStateInfo.IsName(name);
            if (animatorStateInfo.normalizedTime >= targetTime)
            {
                break;
            }
            yield return null;
        }

        if (call != null)
        {
            call.Invoke();
        }
    }
}
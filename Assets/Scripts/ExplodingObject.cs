using System.Collections;
using UnityEngine;

public class ExplodingObject : MonoBehaviour
{
    [SerializeField] protected Animator _animator;

    protected bool exploded = false;

    public void Explode()
    {
        if (exploded)
        {
            return;
        }

        exploded = true;

        _animator.enabled = true;
        this.OnAnimation(_animator, "Explode", OnExplodeAnimation, 0.99f);
        _animator.SetTrigger("Explode");

        AudioController.Instance.Sounds.PlayOneShot("explode");
        VibrationManager.Instance.Vibrate();
    }

    protected virtual void OnExplodeAnimation()
    {
        Destroy(gameObject);
    }
}
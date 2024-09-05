using UnityEngine;

public class AnimationEndDestroy : MonoBehaviour
{   
    public void TriggerAnimEnd()
    {
        Destroy(gameObject);
    }
}

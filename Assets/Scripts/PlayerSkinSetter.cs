using UnityEngine;

public class PlayerSkinSetter : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SkinTarget skinTarget;

    void Start()
    {
        SetSkin();

        SkinsManager.Instance.OnSkinChanged += SetSkin;
    }

    private void SetSkin()
    {
        spriteRenderer.sprite = SkinsManager.Instance.GetSelectedSprite(skinTarget);
    }

    private void OnDestroy()
    {
        SkinsManager.Instance.OnSkinChanged -= SetSkin;
    }
}

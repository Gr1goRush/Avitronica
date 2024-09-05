using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSkinsShop : MonoBehaviour
{
    public SkinTarget skinTarget;

    [SerializeField] private bool updateOnEnable = true;
    [SerializeField] private TargetSkinsShopItem itemOriginal;

    private List<TargetSkinsShopItem> items;

    void OnEnable()
    {
        if (updateOnEnable)
        {
            UpdateItems();
        }
    }

    public void UpdateItems()
    {
        SkinTargetSkinsData skinsData = SkinsManager.Instance.GetData(skinTarget);

        if(items == null)
        {
            items = new List<TargetSkinsShopItem>();
        }

        int lastItemIndex = 0;
        for (int i = 0; i < skinsData.GetCount(); i++)
        {
            TargetSkinsShopItem shopItem;
            if(i < items.Count)
            {
                shopItem = items[i];
            }
            else
            {
                shopItem = Instantiate(itemOriginal, itemOriginal.transform.parent);
                items.Add(shopItem);

                int index = i;
                shopItem.AddClickListener(() => ClickItem(index));
            }

            Sprite icon = SkinsManager.Instance.GetSprite(skinsData, i);
            shopItem.SetIcon(icon);

            SetItem(shopItem, i, skinsData);

            shopItem.gameObject.SetActive(true);
            lastItemIndex = i;
        }

        for (int i = lastItemIndex + 1; i < items.Count; i++)
        {
            items[i].gameObject.SetActive(false);
        }

        itemOriginal.gameObject.SetActive(false);
    }

    private void ClickItem(int index)
    {
        SkinTargetSkinsData skinsData = SkinsManager.Instance.GetData(skinTarget);
        if (skinsData.itemsAvailableStates[index])
        {
            SkinsManager.Instance.Select(skinTarget, index);
            skinsData.selectedItemIndex = index;

            for (int i = 0; i < skinsData.GetCount(); i++)
            {
                SetItem(items[i], i, skinsData);
            }
        }
        else
        {
            if (SkinsManager.Instance.Buy(skinTarget, index))
            {
                items[index].SetState(false);
            }
        }
    }

    private void SetItem(TargetSkinsShopItem shopItem, int index, SkinTargetSkinsData skinsData)
    {
        bool available = skinsData.itemsAvailableStates[index];
        if (available)
        {
            bool selected = skinsData.selectedItemIndex == index;
            shopItem.SetState(selected);
        }
        else
        {
            shopItem.SetPrice(skinsData.price);
        }
    }
}

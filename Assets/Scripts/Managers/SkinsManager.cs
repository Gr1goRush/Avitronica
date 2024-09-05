using UnityEngine;

public enum SkinTarget
{
    Airplane, FighterJet
}

[System.Serializable]
public struct SkinTargetSkinsData
{
    public SkinTarget target;
    public int price;
    public Sprite[] sprites;

    [HideInInspector] public int selectedItemIndex;

    [HideInInspector] public bool[] itemsAvailableStates;

    public string GetSaveKey(int skinIndex)
    {
        return "Skin_" + target.ToString() + "_" + skinIndex.ToString(); 
    }

    public string GetSelectedItemSaveKey()
    {
        return "Skin_" + target.ToString() + "_Selected";
    }

    public int GetCount()
    {
        return sprites.Length;
    }
}

public class SkinsManager : Singleton<SkinsManager>
{
    [SerializeField] private SkinTargetSkinsData[] skinsData;

    public event GameAction OnSkinChanged;

    protected override void Awake()
    {
        base.Awake();

        for (int i = 0; i < skinsData.Length; i++)
        {
            SkinTargetSkinsData targetSkinsData = skinsData[i];

            bool[] itemsAvailableStates = new bool[targetSkinsData.GetCount()];
            for (int skinIndex = 0; skinIndex < itemsAvailableStates.Length; skinIndex++)
            {
                itemsAvailableStates[skinIndex] = skinIndex == 0 || PlayerPrefs.GetInt(targetSkinsData.GetSaveKey(skinIndex), 0) == 1;
            }

            targetSkinsData.itemsAvailableStates = itemsAvailableStates;
            targetSkinsData.selectedItemIndex = PlayerPrefs.GetInt(targetSkinsData.GetSelectedItemSaveKey(), 0);

            skinsData[i] = targetSkinsData;
        }
    }

    public bool Buy(SkinTarget gameType, int skinIndex)
    {
        int dataIndex = FindDataIndex(gameType);
        SkinTargetSkinsData targetSkinsData = skinsData[dataIndex];

        if (!GameManager.Instance.HasMedals(targetSkinsData.price))
        {
            return false;
        }

        GameManager.Instance.SubtractMedals(targetSkinsData.price);

        bool[] itemsAvailableStates = targetSkinsData.itemsAvailableStates;
        itemsAvailableStates[skinIndex] = true;
        targetSkinsData.itemsAvailableStates = itemsAvailableStates;
        skinsData[dataIndex] = targetSkinsData;

        PlayerPrefs.SetInt(targetSkinsData.GetSaveKey(skinIndex), 1);

        return true;
    }

    public void Select(SkinTarget gameType, int skinIndex)
    {
        int dataIndex = FindDataIndex(gameType);
        SkinTargetSkinsData targetSkinsData = skinsData[dataIndex];

        targetSkinsData.selectedItemIndex = skinIndex;
        skinsData[dataIndex] = targetSkinsData;

        PlayerPrefs.SetInt(targetSkinsData.GetSelectedItemSaveKey(), skinIndex);

        OnSkinChanged?.Invoke();
    }

    private int FindDataIndex(SkinTarget skinTargte)
    {
        for (int i = 0; i < skinsData.Length; i++)
        {
            SkinTargetSkinsData targetSkinsData = skinsData[i];
            if(targetSkinsData.target == skinTargte)
            {
                return i;
            }
        }

        return -1;
    }

    public SkinTargetSkinsData GetData(SkinTarget skinTarget)
    {
        int dataIndex = FindDataIndex(skinTarget);
        return skinsData[dataIndex];
    }

    public Sprite GetSprite(SkinTargetSkinsData targetSkinsData, int skinIndex)
    {
        return targetSkinsData.sprites[skinIndex];
    }

    public Sprite GetSelectedSprite(SkinTarget skinTarget)
    {
        SkinTargetSkinsData targetSkinsData = GetData(skinTarget);
        return GetSprite(targetSkinsData, targetSkinsData.selectedItemIndex);
    }
}

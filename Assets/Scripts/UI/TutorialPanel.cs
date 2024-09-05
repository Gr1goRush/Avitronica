using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public struct TutorialStage
{
    [TextArea]
    public string text;
}

public class TutorialPanel : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshProUGUI _text;

    [SerializeField] private TutorialStage[] stages;

    private int stageIndex = 0;

    private void Start()
    {
        SetStage(0);    
    }

    public void Next()
    {
        stageIndex++;
        if(stageIndex >= stages.Length)
        {
            GameManager.Instance.OnTutorialShow();
            gameObject.SetActive(false);
            return;
        }

        SetStage(stageIndex);
    }

    private void SetStage(int _index)
    {
        stageIndex = _index;
        _text.text = stages[stageIndex].text;
        animator.SetInteger("Stage", stageIndex);
    }
}

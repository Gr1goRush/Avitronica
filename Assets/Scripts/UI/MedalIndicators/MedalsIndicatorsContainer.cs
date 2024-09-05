using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedalsIndicatorsContainer : MonoBehaviour
{
    [SerializeField] private MedalIndicatorItem prefab;

    private void Start()
    {
        prefab.Init();    
    }

    public void Spawn(Vector3 position)
    {
        MedalIndicatorItem newItem = prefab.Pull();
        newItem.transform.position = position;
        newItem.Play();
    }
}

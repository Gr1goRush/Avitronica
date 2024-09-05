using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIController : Singleton<GameUIController>
{
    public MovePointer MovePointer => movePointer;
    [SerializeField] private MovePointer movePointer;

    public MedalsIndicatorsContainer MedalsIndicatorsContainer => medalsIndicatorsContainer;
    [SerializeField] private MedalsIndicatorsContainer medalsIndicatorsContainer;
}

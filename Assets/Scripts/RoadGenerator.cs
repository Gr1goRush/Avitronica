using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    
    public GameObject Player;

    [SerializeField] private int GenerationRange;

    [Space(20)]
    [Header("Road Parts")]
    [SerializeField] private RoadPart[] OtherRoadParts;
    [SerializeField] private RoadPart RoadPartStart, RoadPartFinish;


    [SerializeField] private List<RoadPart> spawnedRoad;

    [SerializeField] private int OffsetRoadParts;

    
    void Start()
    {
        spawnedRoad.Add(RoadPartStart);
     
        StartGenerate();
    }

    
    void Update()
    {
        if(Player.transform.position.y > spawnedRoad[spawnedRoad.Count - OffsetRoadParts].Begin.position.y)
        {
            StartGenerate();
        }
    }


    public void StartGenerate()
    {       
        for (int i = 0; i < (OffsetRoadParts > GenerationRange? OffsetRoadParts : GenerationRange); i++)
        {
            RoadPart newRoad = Instantiate(OtherRoadParts[Random.Range(0, OtherRoadParts.Length)]);

            newRoad.name = $"Map_0{spawnedRoad.Count}";
            newRoad.transform.SetParent(this.transform);
            newRoad.transform.position = spawnedRoad[spawnedRoad.Count - 1].End.position - newRoad.Begin.position;
            
            spawnedRoad.Add(newRoad);
        }
    }


}

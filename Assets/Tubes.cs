using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tubes : MonoBehaviour
{
    [SerializeField] GameObject _tubePrefab;
    [SerializeField] int _tubesCount = 5;
    [SerializeField] int _tubesBetweenDistance = 50;
    private List<GameObject> _tubesArray;
    private void Start()
    {
        _tubesArray = new List<GameObject>();

        GameObject tempTube;
        for (int I = 0; I < _tubesCount; I++)
        {
            tempTube = Instantiate(_tubePrefab, transform);
            _tubesArray.Add(tempTube);
        }
    }
    public void PlaceTubes()
    {
        TubePair tempTubePairClass;
        for (int I = 0; I < _tubesCount; I++)
        {
            tempTubePairClass = _tubesArray[I].GetComponent<TubePair>();
            tempTubePairClass.PlaceTube(I * _tubesBetweenDistance);
        }
    }

    public void StartTubesMoving()
    {
        TubePair tempTubePairClass;
        for(int I = 0; I < _tubesCount; I++)
        {
            tempTubePairClass = _tubesArray[I].GetComponent<TubePair>();
            tempTubePairClass.MovingStart();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tubes : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TubePair _tubePairPrefab;
    [Header("Parameters")]
    [SerializeField] int _tubesCount = 5;
    [SerializeField] int _tubesBetweenDistance = 50;
    [SerializeField] float _speed = 1.5f;

    private TubePair[] _tubePairs;
    private Coroutine _movingCoroutine;

    private void Start()
    {
        _tubePairs = new TubePair[_tubesCount];

        CreateTubePairs();
    }

    public void Spawn()
    {
        for (int i = 0; i < _tubesCount; i++)
        {
            var tubePair = _tubePairs[i];
            var position = Vector3.right * i * _tubesBetweenDistance;
            tubePair.Place(position);
        }
    }
    public void StartMoving()
    {
        _movingCoroutine = StartCoroutine(Moving());
    }
    public void StopMoving()
    {
        StopCoroutine(_movingCoroutine);
    }

    private IEnumerator Moving()
    {
        while (true)
        {
            foreach(var tubePair in _tubePairs)
            {
                
                tubePair.Move(_speed);
            }
            yield return new WaitForFixedUpdate();
        }
    }
    private void CreateTubePairs()
    {
        for (int i = 0; i < _tubesCount; i++)
        {
            var tubePair = CreateTubePair();
            _tubePairs[i] = tubePair;
        }
    }
    private TubePair CreateTubePair()
    {
        return Instantiate(_tubePairPrefab, transform);
    }
}

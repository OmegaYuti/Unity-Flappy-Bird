using UnityEngine;

public class TubePair : MonoBehaviour
{
    [Header("Child tubes parameters")]
    [SerializeField] private Tube _bottomTube;
    [SerializeField] private Tube _upperTube;
    [SerializeField] private float _minTubeSize = 5;
    [SerializeField] private float _minHoleSize = 8;
    [SerializeField] private float _maxHoleSize = 12;
    [SerializeField] private int _distance = 27;

    [Header("Tubes movement parameters")]
    [SerializeField] private float _tubesStandartSpeed = 0;
    [SerializeField] private Vector3 _behindScreenTubeEndPosition;
    [SerializeField] private Vector3 _behindScreenTubeStartPosition;


    private void Start()
    {
        RandomizeHeight();
    }

    public void Place(Vector3 position)
    {
        transform.localPosition = position;

        RandomizeHeight();
    }
    public void Move(float offset)
    {
        var nextPostion = transform.localPosition + Vector3.left * offset;
        transform.localPosition = nextPostion;

        var isOutOfScreen = transform.position.x <= _behindScreenTubeEndPosition.x;
        if (isOutOfScreen)
            Place(_behindScreenTubeStartPosition);
    }
    private void RandomizeHeight()
    {
        var holeSize = Random.Range(_minHoleSize, _maxHoleSize + 1);

        var minHoleHeight = _minTubeSize + holeSize / 2;
        var maxHoleHeight = _distance - holeSize / 2 - _minTubeSize;
        var holeHeight = Random.Range(minHoleHeight, maxHoleHeight);

        var upperTubeHeight = _minTubeSize + _distance - holeHeight - holeSize / 2;
        var bottomTubeHeight = _minTubeSize + holeHeight - holeSize / 2;

        var upperTubeEdgeHeight = upperTubeHeight - _minTubeSize;
        var bottomTubeEdgeHeight = bottomTubeHeight - _minTubeSize;

        _upperTube.ChangeHeight(upperTubeHeight, upperTubeEdgeHeight);
        _bottomTube.ChangeHeight(bottomTubeHeight, bottomTubeEdgeHeight);
    }
}

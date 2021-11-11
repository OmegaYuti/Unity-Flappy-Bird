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

    [Header("Input parameters")]
    [SerializeField] private Bird _bird;
    [SerializeField] private UI _ui;

    [Header("Tubes parameters")]
    [SerializeField] private int _tubeNumber = 1;

    [Header("Tubes movement parameters")]
    [SerializeField] private float _tubesStadartSpeed = 0;
    [SerializeField] private Vector3 _behindScreenTubeEndPosition;
    [SerializeField] private Vector3 _behindScreenTubeStartPosition;

    private float tubeCurrectSpeed;
    private void Start()
    {
        _bird.StartMoving += ParametersMovingStart;
        _ui.EndDeathMessage += PlaceTubes;

        RandomTubes();
    }
    private void OnDisable()
    {
        _bird.StartMoving -= ParametersMovingStart;
        _ui.EndDeathMessage -= PlaceTubes;
    }
    private void FixedUpdate()
    {
        if (tubeCurrectSpeed == 0)
            return;
        MoveTubes();
    }

    private void ParametersMovingStart()
    {
        tubeCurrectSpeed = _tubesStadartSpeed;
    }
    private void PlaceTubes()
    {
        tubeCurrectSpeed = 0;

        var position = Vector3.right * 50 * _tubeNumber;
        transform.localPosition = position;

        RandomTubes();
    }
    private void MoveTubes()
    {
        var nextPostion = transform.localPosition + Vector3.left * tubeCurrectSpeed;
        transform.localPosition = nextPostion;

        if (transform.localPosition.x - _behindScreenTubeEndPosition.x > 0)
            return;

        RandomTubes();
        transform.localPosition = _behindScreenTubeStartPosition;
    }
    private void RandomTubes()
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

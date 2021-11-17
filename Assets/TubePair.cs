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
    [SerializeField] private float _tubesStadartSpeed = 0;
    [SerializeField] private Vector3 _behindScreenTubeEndPosition;
    [SerializeField] private Vector3 _behindScreenTubeStartPosition;

    private float tubeCurrectSpeed;
    private void Start()
    {
        RandomTube();
    }
    private void FixedUpdate()
    {
        if (tubeCurrectSpeed == 0)
            return;
        MoveTube();
    }

    public void MovingStart()
    {
        tubeCurrectSpeed = _tubesStadartSpeed;
    }
    public void PlaceTube(int distance)
    {
        tubeCurrectSpeed = 0;

        var position = Vector3.right * distance;
        transform.localPosition = position;

        RandomTube();
    }
    private void MoveTube()
    {
        var nextPostion = transform.localPosition + Vector3.left * tubeCurrectSpeed;
        transform.localPosition = nextPostion;

        if (transform.localPosition.x - _behindScreenTubeEndPosition.x > 0)
            return;

        RandomTube();
        transform.localPosition = _behindScreenTubeStartPosition;
    }
    private void RandomTube()
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

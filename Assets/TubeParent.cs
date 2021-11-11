using UnityEngine;


public class TubeParent : MonoBehaviour
{
    [Header("Child tubes parameters")]
    [SerializeField] private GameObject _bottomTube;
    [SerializeField] private GameObject _upperTube;
    [SerializeField] private float _minTubeSize = 5;
    [SerializeField] private float _standartWidth = 5;
    [SerializeField] private int _minHoleSize = 8;
    [SerializeField] private int _maxHoleSize = 12;
    [SerializeField] private int _distance = 27;

    [Header("Input parameters")]
    [SerializeField] private Bird _bird;
    [SerializeField] private UI _UI;

    [Header("Tubes parameters")]
    [SerializeField] private int _tubeNumber = 1;

    [Header("Tubes movement parameters")]
    [SerializeField] private float _tubesSpeed = 0;
    [SerializeField] private Vector3 _behindScreenTubeEndPosition;
    [SerializeField] private Vector3 _behindScreenTubeStartPosition;

    private SpriteRenderer _upperTubeSpriteRenderer;
    private SpriteRenderer _bottomTubeSpriteRenderer;
    private EdgeCollider2D _upperTubeEdgeCollider;
    private EdgeCollider2D _bottomTubeEdgeCollider;

    private void Start()
    {
        _upperTubeEdgeCollider = _upperTube.GetComponent<EdgeCollider2D>();
        _bottomTubeEdgeCollider = _bottomTube.GetComponent<EdgeCollider2D>();

        _upperTubeSpriteRenderer = _upperTube.GetComponent<SpriteRenderer>();
        _bottomTubeSpriteRenderer = _bottomTube.GetComponent<SpriteRenderer>();

        _bird.StartMoving += ParametersMovingStart;
        _UI.EndDeathMessage += PlaceTubes;

        RandomTubes();
    }
    private void FixedUpdate()
    {
        MoveTubes();
    }

    private void ParametersMovingStart()
    {
        _tubesSpeed = 0.5f;
    }
    private void PlaceTubes()
    {
        _tubesSpeed = 0;

        var position = Vector3.right * 50 * _tubeNumber;
        transform.localPosition = position;

        RandomTubes();
    }
    private void MoveTubes()
    {
        var nextPostion = transform.localPosition + Vector3.left * _tubesSpeed;
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

        _upperTubeSpriteRenderer.size = Vector2.up * upperTubeHeight + Vector2.right * _standartWidth;
        _bottomTubeSpriteRenderer.size = Vector2.up * bottomTubeHeight + Vector2.right * _standartWidth;

        _upperTubeEdgeCollider.offset = Vector2.up * upperTubeEdgeHeight;
        _bottomTubeEdgeCollider.offset = Vector2.up * bottomTubeEdgeHeight;
    }

}

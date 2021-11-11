using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bird : MonoBehaviour
{
    [Header("Parameters Bird")]
    [SerializeField] private float _maxRotationAngle = 0.8f;
    [SerializeField] private float _velocityScale = 0.02f;
    [SerializeField] private float _jumpForce = 20000f;
    [SerializeField] private Vector3 _startPosition;
    [Header("Input parameters")]
    [SerializeField] private UI _UI;

    private Rigidbody2D _rigidBody;
    private CircleCollider2D _circleCollider;

    private bool _canStartGame = true;
    private bool _gameInProgress = false;

    public event Action Death;
    public event Action TubePassed;
    public event Action StartMoving;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _circleCollider = GetComponent<CircleCollider2D>();
        _UI.EndDeathMessage += RespawnBird;
    }

    private void Update()
    {
        if (CanJump() && _gameInProgress)
            Jump();
        else if (CanJump() && _canStartGame)
            StartGame();
        else if(!_gameInProgress)
            return;
        RotateByVelocity();
    }
    private bool CanJump()
    {
        return Input.GetMouseButtonDown(0);
    }
    private void Jump()
    {
        _rigidBody.velocity = Vector2.zero;
        _rigidBody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }
    private void StartGame()
    {
        StartMoving();
        _rigidBody.simulated = true;
        _gameInProgress = true;
        _canStartGame = false;
    }

    private void RespawnBird()
    {
        _rigidBody.simulated = false;
        _circleCollider.enabled = true;
        transform.localPosition = _startPosition;
        _rigidBody.velocity = Vector2.zero;
        RotateByVelocity();
        _canStartGame = true;
    }
    private void RotateByVelocity()
    {
        if (_rigidBody.velocity.y == 0)
            return;

        var impulse = _rigidBody.velocity.y * _velocityScale;
        var clampedImpulse = Mathf.Clamp(impulse, -1f, 1f);
        var angle = _maxRotationAngle * clampedImpulse;

        transform.eulerAngles = Vector3.forward * angle;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var collisionObject = collision.gameObject;

        if (collisionObject.TryGetComponent<BoxCollider2D>(out _))
        {
            _circleCollider.enabled = false;
            _gameInProgress = false;
            Death();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var collisionObject = collision.gameObject;

        if (collisionObject.TryGetComponent<TubeParent>(out _))
            TubePassed();
    }
}

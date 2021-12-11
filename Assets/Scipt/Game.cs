using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [Header("Bird")]
    [SerializeField] private GameObject _bird;
    [SerializeField] private Vector3 _birdStartPosition;
    [Header("UI")]
    [SerializeField] private ScoreView _textScore;
    [Header("Tubes")]
    [SerializeField] private Tubes _tubes;

    private Rigidbody2D _birdRigidbody;
    private CircleCollider2D _birdCollider;
    private Bird _birdClass;
    private Transform _birdTranform;

    private bool _canStartGame = true;
    private bool _gameInProgress = false;
    private int _score = 0;

    private void Start()
    {
        _birdClass = _bird.GetComponent<Bird>();
        _birdRigidbody = _bird.GetComponent<Rigidbody2D>();
        _birdCollider = _bird.GetComponent<CircleCollider2D>();
        _birdTranform = _bird.transform;

        _birdClass.Death += BirdDeathStart;
        _birdClass.TubePassed += PlusScore;

        PlaceTubesStartPosition();
    }
    private void OnDestroy()
    {
        _birdClass.Death -= BirdDeathStart;
        _birdClass.TubePassed -= PlusScore;
    }
    private void Update()
    {
        if(!click())
            return;
        _birdClass.Jump();
    }

    private bool click()
    {
        if (!Input.GetMouseButtonDown(0))
            return false;
        if (!_gameInProgress)
        {
            if (!_canStartGame)
            {
                return false;
            }
            else
            {
                StartGame();
                return true;
            }
        }
        else
        {
            return true;
        }
    }
    private void StartGame()
    {
        _canStartGame = false;
        _gameInProgress = true;

        TubesActiveMoving();

        _birdRigidbody.simulated = true;
    }
    private void PlusScore()
    {
        _score++;
        _textScore.Update(_score);
    }
    private void BirdDeathStart()
    {
        _gameInProgress = false;
        _birdCollider.enabled = false;

        _tubes.StopMoving();

        _textScore.PlayAnimation(BirdDeathEnd);
    }
    private void BirdDeathEnd()
    {
        _canStartGame = true;

        PlaceTubesStartPosition();

        SetBirdStartPos();

        _score = 0;

        _textScore.Update(_score);
    }
    private void SetBirdStartPos()
    {
        _birdTranform.localPosition = _birdStartPosition;
        _birdTranform.eulerAngles = Vector3.zero;
        _birdRigidbody.velocity = Vector2.zero;
        _birdRigidbody.simulated = false;
        _birdCollider.enabled = true;
    }

    private void TubesActiveMoving()
    {
        _tubes.StartMoving();
    }
    private void PlaceTubesStartPosition()
    {
        _tubes.Spawn();
    }
}

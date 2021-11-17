using System;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] TextScore _textScore;
    [SerializeField] string _animationName;

    public event Action AnimDeathEnd;

    private void FixedUpdate()
    {
        _textScore.DeathScreenEnd += DeathEnd;
    }
    private void OnDestroy()
    {
        _textScore.DeathScreenEnd -= DeathEnd;
    }

    public void DisplayScore(int score)
    {
        var text = "Score: " + score;
        _textScore.SetTextScore(text);
    }
    private void DeathEnd()
    {
        AnimDeathEnd();
        DisplayScore(0);
    }
    public void DeathStart()
    {
        _textScore.PlayAnimation(_animationName);
    }
}

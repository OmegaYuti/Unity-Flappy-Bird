using System;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Bird _bird;
    [SerializeField] private GameObject _scoreText;

    public event Action EndDeathMessage;

    private int _score = 0;

    private void FixedUpdate()
    {
        var textAnimation = _scoreText.GetComponent<Animation>();
        if (textAnimation.enabled)
            return;
        EndDeathScreen(textAnimation);
    }
    private void Start()
    {
        _bird.Death += DeathScreen;
        _bird.TubePassed += PlusScore;
    }
    private void EndDeathScreen(Animation textAnimation)
    {
        var Text = _scoreText.GetComponent<Text>();

        textAnimation.enabled = true;

        _score = 0;
        Text.text = "Score: " + _score;

        EndDeathMessage();
    }
    private void DeathScreen()
    {
        var textAnimation = _scoreText.GetComponent<Animation>();

        textAnimation.Play("Death Animation");
    }
    private void PlusScore()
    {
        var Text = _scoreText.GetComponent<Text>();

        _score += 1;
        Text.text = "Score: " + _score;
    }
}

using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Text _textField;
    [SerializeField] string _textFormat;
    [SerializeField] Animation _animation;

    private Action _endAnimationCallback;

    public void OnAnimationEnd()
    {
        _endAnimationCallback?.Invoke();
    }
    public void Update(int score)
    {
        _textField.text = string.Format(_textFormat, score);
    }
    public void PlayAnimation(Action endCallback)
    {
        _endAnimationCallback = endCallback;
        _animation.Play();
    }
}

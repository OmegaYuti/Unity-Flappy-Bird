using System;
using UnityEngine;
using UnityEngine.UI;

public class TextScore : MonoBehaviour
{
    [SerializeField] Text _textObject;
    [SerializeField] Animation _animation;

    public event Action DeathScreenEnd;

    private void FixedUpdate()
    {
        if (_animation.enabled)
            return;
        _animation.enabled = true;
        DeathScreenEnd();
    }

    public void SetTextScore(string Text)
    {
        _textObject.text = Text;
    }
    public void PlayAnimation(string name)
    {
        _animation.Play(name);
    }
}

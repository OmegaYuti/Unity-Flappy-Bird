using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tube : MonoBehaviour
{
    [SerializeField] private EdgeCollider2D _collider;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public void ChangeHeight(float height, float colliderHeight)
    {
        var width = _spriteRenderer.size.x;

        _spriteRenderer.size = Vector2.up * height + Vector2.right * width;
        _collider.offset = Vector2.up * colliderHeight;
    }
}

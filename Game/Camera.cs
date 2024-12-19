using System;
using Microsoft.Xna.Framework;

namespace AlienQuest.Game;

public class Camera
{
    public Vector2 Position { get; private set; } = Vector2.Zero;
    public float Zoom { get; set; } = 1f;
    private Vector2 _shakeOffset;
    private float _shakeTimer;
    private float _shakeIntensity;

    public void SetPosition(Vector2 position)
    {
        Position = position;
    }

    public Matrix GetTransformMatrix()
    {
        return Matrix.CreateTranslation(-Position.X + _shakeOffset.X, -Position.Y + _shakeOffset.Y, 0) *
               Matrix.CreateRotationZ(0) *
               Matrix.CreateScale(Zoom, Zoom, 1);
    }

    public void Update(GameTime gameTime)
    {
        if (_shakeTimer > 0)
        {
            _shakeOffset = new Vector2(
                (float)(Random.Shared.NextDouble() * 2 - 1) * _shakeIntensity,
                (float)(Random.Shared.NextDouble() * 2 - 1) * _shakeIntensity
            );
            _shakeOffset = Vector2.Lerp(_shakeOffset, Vector2.Zero, 0.1f);
            _shakeTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            _shakeIntensity *= 0.9f;
        }
        else
        {
            _shakeOffset = Vector2.Zero; // Reset shake when timer expires
        }
    }
    public void Shake(float intensity, float duration)
    {
        _shakeIntensity = intensity;
        _shakeTimer = duration;
    }
}
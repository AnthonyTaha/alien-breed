using Microsoft.Xna.Framework;

namespace AlienBreed.Game;

public class AnimationPlayer
{
    private Animation _currentAnimation;
    private int _currentFrame;
    private int _currentIndex;
    private float _timer;
    private bool _played;
    
    public int CurrentFrame => _currentFrame;
    public bool Played => _played;
    public AnimationPlayer(int defaultFrame)
    {
        _currentFrame = defaultFrame;
        _timer = 0f;
        _currentIndex = 0;
        _played = false;
    }
    
    public void Play(Animation animation)
    {
        if (_currentAnimation != animation)
        {
            _currentIndex = 0;
            _currentAnimation = animation;
            _currentFrame = animation.Frames[_currentIndex];
            _timer = 0f;
        }
    }

    public void Update(GameTime gameTime)
    {
        if (_currentAnimation != null)
        {
            _timer += gameTime.ElapsedGameTime.Milliseconds;
            //If FrameTime milliseconds pass change frame
            if (_timer >= _currentAnimation.FrameTime)
            {
                _timer -= _currentAnimation.FrameTime;
                if (_currentIndex >= _currentAnimation.Frames.Length - 1)
                {
                    _played = true;
                    //Now Starts again
                    _currentIndex = 0;
                }
                else
                {
                    //Continue through Frames
                    _currentIndex++;
                }
                //Updates currentSprite
                _currentFrame = _currentAnimation.Frames[_currentIndex];
            }
        }

    }
}
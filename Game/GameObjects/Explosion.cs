using Microsoft.Xna.Framework;

namespace AlienBreed.Game.GameObjects;

public class Explosion:GameObject
{
    private Animation _animation;
    public Explosion(Vector2 position) : base(7, position, new Vector2(2,2), new Vector2(16,16))
    {
        _animation = new Animation(new[] { 7, 8, 9, 10 }, 50);
        Sprite.AnimationPlayer.Play(_animation);
    }

    public override void Update(GameTime gameTime,LevelManager levelManager)
    {
        base.Update(gameTime, levelManager);
        if (Sprite.AnimationPlayer.Played)
        {
            levelManager.RemoveGameObject(this);
        }
    }
}
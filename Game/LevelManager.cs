using System;
using System.Collections.Generic;
using System.Linq;
using AlienBreed.Game.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AlienBreed.Game;

public class LevelManager
{
    private GameState _gameState;
    
    private Camera _camera = new();
    private List<Bullet> _bullets = new();
    private Player _player;
    
    private ItemSpawner _itemSpawner;
    private PlayerLoader _playerLoader;
    
    private int _screenWidth, _screenHeight;
    
    private int _score;
    private int _scoreInterval = 0;
    private int _scoreIntervalCap = 100;
    private int _scoreIncreaseAmount = 10;
    private SpriteFont _font;
    
    private SoundEffect _explosionSoundEffect;
    private SoundEffect _firingSoundEffect;
    private SoundEffect _powerUpSoundEffect;
    
    public Camera Camera => _camera;
    public List<Bullet> Bullets => _bullets;
    public Player Player => _player;
    public ItemSpawner ItemSpawner => _itemSpawner;
    public int ScreenWidth => _screenWidth;
    public int ScreenHeight => _screenHeight;
    public GameState GameState => _gameState;

    public SoundEffect ExplosionSoundEffect => _explosionSoundEffect;

    public SoundEffect FiringSoundEffect => _firingSoundEffect;

    public SoundEffect PowerUpSoundEffect => _powerUpSoundEffect;

    public void LoadContent(ContentManager content,GraphicsDevice graphics, SpriteSheet spriteSheet)
    {
        _gameState = GameState.Playing;
        
        //Load Content
        _font = content.Load<SpriteFont>("font");
        _firingSoundEffect = content.Load<SoundEffect>("shoot");
        _explosionSoundEffect = content.Load<SoundEffect>("explosion");
        _powerUpSoundEffect = content.Load<SoundEffect>("powerup");
        
        _playerLoader = new PlayerLoader();
        _playerLoader.LoadPlayer("player1",content);
        // Get screen dimensions
        _screenWidth = graphics.Viewport.Width;
        _screenHeight = graphics.Viewport.Height;


        // Calculate position to center at the bottom
        float xPosition = _screenWidth / 2f;
        float yPosition = _screenHeight - 32;
        _player = new Player(new Vector2(xPosition,yPosition),_playerLoader.Speed);
        

        _itemSpawner = new ItemSpawner(100,_screenWidth,_screenHeight);
    }
    
    public void UpdateGameObjects(GameTime gameTime)
    {
        _player.Update(gameTime,this);
        _camera.Update(gameTime);
        
        _itemSpawner.Update(gameTime,this);

        foreach (Bullet bullet in _bullets.ToList())
        {
            bullet.Update(gameTime, this);
        }
        
    }
    
    public void DrawGameObjects(SpriteBatch spriteBatch,SpriteSheet spriteSheet)
    {
        
        foreach (Bullet bullet in _bullets)
        {
            bullet.Draw(spriteBatch,spriteSheet);
        }
        
        _player.Draw(spriteBatch,spriteSheet);
        
        _itemSpawner.Draw(spriteBatch,spriteSheet);
        spriteBatch.DrawString(_font, "Score: "+_score +" Health: "+_player.Health, new Vector2(10, _screenHeight-20), Color.White);
    }

    public void IncreaseScore()
    {
        _score+=_scoreIncreaseAmount;
        _scoreInterval += _scoreIncreaseAmount;
        if (_scoreInterval > _scoreIntervalCap)
        {
            _scoreInterval = 0;
            ItemSpawner.DecreaseInterval();
        }
    }

    public void AddBullet(Bullet bullet)
    {
        _bullets.Add(bullet);
    }

    public void RemoveBullet(Bullet bullet)
    {
        _bullets.Remove(bullet);
    }

    public void PlayerDeath()
    {
        _gameState = GameState.Dead;
    }

}
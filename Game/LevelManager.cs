using System;
using System.Collections.Generic;
using System.Linq;
using AlienQuest.Game.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AlienQuest.Game;

public class LevelManager
{
    private GameState _gameState;
    
    private Camera _camera = new();
    private List<GameObject> _gameObjects = new();
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
    
    public List<GameObject> GameObjects => _gameObjects;
    public Camera Camera => _camera;
    public Player Player => _player;
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
        _font = content.Load<SpriteFont>("font/font");
        _firingSoundEffect = content.Load<SoundEffect>("audio/shoot");
        _explosionSoundEffect = content.Load<SoundEffect>("audio/explosion");
        _powerUpSoundEffect = content.Load<SoundEffect>("audio/powerup");
        
        _playerLoader = new PlayerLoader();
        _playerLoader.LoadPlayer("player1",content);
        
        // Get screen dimensions
        _screenWidth = graphics.Viewport.Width;
        _screenHeight = graphics.Viewport.Height;


        // Calculate position to center at the bottom
        float xPosition = _screenWidth / 2f;
        float yPosition = _screenHeight - 32;
        _player = new Player(new Vector2(xPosition,yPosition),_playerLoader.Speed);
        _itemSpawner = new ItemSpawner(100);
        
        AddGameObject(_player);
    }
    
    public void UpdateGameObjects(GameTime gameTime)
    {
        _camera.Update(gameTime);
        
        _itemSpawner.Update(gameTime,this);

        foreach (GameObject gameObject in _gameObjects.ToList())
        {
            gameObject.Update(gameTime, this);
        }
        
    }
    
    public void DrawGameObjects(SpriteBatch spriteBatch,SpriteSheet spriteSheet)
    {
        
        foreach (GameObject gameObject in _gameObjects)
        {
            gameObject.Draw(spriteBatch,spriteSheet);
        }
        
        _player.Draw(spriteBatch,spriteSheet);
        
        spriteBatch.DrawString(_font, "Score: "+_score +" Health: "+_player.Health, new Vector2(10, _screenHeight-20), Color.White);
    }

    public void IncreaseScore()
    {
        _score+=_scoreIncreaseAmount;
        _scoreInterval += _scoreIncreaseAmount;
        if (_scoreInterval > _scoreIntervalCap)
        {
            _scoreInterval = 0;
            _itemSpawner.DecreaseInterval();
        }
    }

    public void AddGameObject(GameObject gameObject)
    {
        _gameObjects.Add(gameObject);
    }

    public void RemoveGameObject(GameObject gameObject)
    {
        _gameObjects.Remove(gameObject);
    }

    public void PlayerDeath()
    {
        _gameState = GameState.Dead;
    }

}
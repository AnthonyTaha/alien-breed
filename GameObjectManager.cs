using System.Collections;
using System.Collections.Generic;
using GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlienBreed;

public class GameObjectManager
{
    private List<GameObject> _gameObjects;
    
    public GameObjectManager()
    {
        _gameObjects = new List<GameObject>();
    }

    public void addGameObject(GameObject gameObject)
    {
        _gameObjects.Add(gameObject);
    }

    public void removeGameObject(GameObject gameObject)
    {
        _gameObjects.Remove(gameObject);
    }

    public void updateGameObjects(GameTime gameTime)
    {
        for (int i = 0; i < _gameObjects.Count; i++)
        {
            _gameObjects[i].Update(gameTime,this);
        }
    }
    public void drawGameObjects(SpriteBatch spriteBatch)
    {
        for (int i = 0; i < _gameObjects.Count; i++)
        {
            _gameObjects[i].Draw(spriteBatch);
        }
    }
    public List<GameObject> GetAllGameObjects()
    {
        return _gameObjects;
    }
}
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WishnusArmy.GameManagement;

public class GameStateManager : IGameLoopObject
{
    Dictionary<string, IGameLoopObject> gameStates;
    IGameLoopObject currentGameState;
    SoundManager soundManager;

    public GameStateManager()
    {
        gameStates = new Dictionary<string, IGameLoopObject>();
        currentGameState = null;
        soundManager = new SoundManager();
    }

    public void AddGameState(string name, IGameLoopObject state)
    {
        gameStates[name] = state;
    }

    public IGameLoopObject GetGameState(string name)
    {
        return gameStates[name];
    }

    public void SwitchTo(string name)
    {
        if (gameStates.ContainsKey(name))
        {
            currentGameState = gameStates[name];
            switch(currentGameState.ToString())
            {
                case "MainMenuState":
                case "HelpState":
                case "CreditsState":
                    PopupScreen.ClearButtons();
                    PopupScreen.AddButton("Exit to Desktop", delegate { WishnusArmy.WishnusArmy.self.Exit(); });
                    break;

                case "PlayingState":
                case "LevelBuilderState":
                case "LevelGeneratorState":
                    PopupScreen.ClearButtons();
                    PopupScreen.AddButton("Exit to Menu", delegate { WishnusArmy.WishnusArmy.GameStateManager.SwitchTo("MainMenuState"); });
                    PopupScreen.AddButton("Exit to Desktop", delegate { WishnusArmy.WishnusArmy.self.Exit(); });
                    break;
            }
            soundManager.PlayMusic(name);
        }
        else
        {
            throw new KeyNotFoundException("Could not find game state: " + name);
        }
    }

    public IGameLoopObject CurrentGameState
    {
        get
        {
            return currentGameState;
        }
    }

    public void HandleInput(InputHelper inputHelper)
    {
        if (currentGameState != null)
        {
            if (PopupScreen.show)
                PopupScreen.HandleInput(inputHelper);
            else
                currentGameState.HandleInput(inputHelper);
        }
    }

    public void Update(object gameTime)
    {
        if (currentGameState != null)
        {
            if (PopupScreen.show)
                PopupScreen.Update(gameTime);
            else
                currentGameState.Update(gameTime);
        }
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (currentGameState != null)
        {
            currentGameState.Draw(gameTime, spriteBatch);
            if (PopupScreen.show)
                PopupScreen.Draw(gameTime, spriteBatch);
        }
    }

    public void Reset()
    {
        if (currentGameState != null)
        {
            currentGameState.Reset();
        }
    }
}
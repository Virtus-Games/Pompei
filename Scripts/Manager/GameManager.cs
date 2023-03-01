using System;
using UnityEngine;

[Serializable]
public enum GAMESTATE
{
    START,
    PLAY,
    VICTORY,
    MARKET,
    SETTINGS,
    DEFEAT,
    TEASER
}

public class GameManager : Singleton<GameManager>
{

    #region  Value Settings

    [HideInInspector]
    public static event Action<GAMESTATE> OnGameStateChanged;
    public GAMESTATE gameState;
    private GameObject _player;
    public bool isPlay { get; private set; }
    public static event Action<bool> OnPlayerHaveInGame;

    public bool status = false;

    #endregion
    public void UpdateGameState(GAMESTATE state)
    {
        gameState = state;

        switch (gameState)
        {
            case GAMESTATE.START:
                HandleStartAction();
                break;
            case GAMESTATE.PLAY:
                HandlePlayAction();
                break;
            case GAMESTATE.VICTORY:
                HandleVictoryAction();
                break;
            case GAMESTATE.MARKET:
                HandleMarketAction();

                break;
            case GAMESTATE.DEFEAT:
                HandleDefeatAction();
                break;
            case GAMESTATE.SETTINGS:
                HandleSettingsAction();
                break;
        }

        OnGameStateChanged?.Invoke(state);
    }
    private void Awake()
    {
        UpdateGameState(GAMESTATE.START);
    }

    #region Update States
    private void HandleSettingsAction()
    {

    }

    private void HandleStartAction()
    {

    }

    private void HandlePlayAction()
    {

        isPlay = true;
    }

    private void HandleDefeatAction()
    {
        isPlay = false;
    }

    private void HandleVictoryAction()
    {
        isPlay = false;
    }

    private void HandleMarketAction()
    {
        isPlay = false;
    }

    #endregion

    #region  Player Status Manager

    public void UpdatePlayerStatus(bool isHave) => OnPlayerHaveInGame?.Invoke(isHave);



    #endregion


}

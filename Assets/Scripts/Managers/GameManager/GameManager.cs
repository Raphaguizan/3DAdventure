using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Game.Util;
using Game.StateMachine;

public class GameManager : Singleton<GameManager>
{
    public enum GameStates
    {
        MENU,
        GAME,
        PAUSE,
        WIN,
        LOSE
    }
    public StateMachineBase<GameStates> StateMachine => _stateMachine;

    private StateMachineBase<GameStates> _stateMachine;

    private void Start()
    {
        Init();
    }
    
    public void Init()
    {
        _stateMachine = new StateMachineBase<GameStates>();
        _stateMachine.SwitchState(GameStates.GAME);
    }


    public void ReloadScene(float delay)
    {
        Invoke(nameof(ReloadScene), delay);
    }
    public void ReloadScene()
    {
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.name);
    }
}

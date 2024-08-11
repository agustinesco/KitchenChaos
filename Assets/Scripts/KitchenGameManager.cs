using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{

    public static KitchenGameManager Instance { get; private set; }
    public enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }

    private State state;
    private float waitingToStartTimerS = 1f;
    private float CountdownToStartTimerS = 3f;
    private float gamePlayingTimer = 10f;
    private float gamePlayingTimerMax = 10f;

    private bool paused = false;

    public event EventHandler OnStateChange;
    public event EventHandler<OnPauseToggleEventArgs> OnPauseToggle;
    public class OnPauseToggleEventArgs : EventArgs
    {
        public bool paused;
    }

    private void Awake()
    {
        state = State.WaitingToStart;

        Instance = this;
    }

    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        TogglePauseGame();
    }

    public void TogglePauseGame()
    {
        paused = !paused;
        OnPauseToggle?.Invoke(this, new OnPauseToggleEventArgs { paused = paused });
        if (paused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                waitingToStartTimerS -= Time.deltaTime;
                if (waitingToStartTimerS < 0)
                {
                    state = State.CountdownToStart;
                    OnStateChange?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.CountdownToStart:
                CountdownToStartTimerS -= Time.deltaTime;
                if (CountdownToStartTimerS < 0)
                {
                    state = State.GamePlaying;
                    OnStateChange?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0)
                {
                    state = State.GameOver;
                    OnStateChange?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
        }

        Debug.Log(state);
    }

    public State GetGameState()
    {
        return state;
    }

    public float GetCountdownToStart()
    {
        return CountdownToStartTimerS;
    }
    public float GetPlayingTimerNormalized()
    {
        return 1 - (gamePlayingTimer / gamePlayingTimerMax);
    }

}

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

    public event EventHandler OnStateChange;


    private void Awake()
    {
        state = State.WaitingToStart;

        Instance = this;
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

}

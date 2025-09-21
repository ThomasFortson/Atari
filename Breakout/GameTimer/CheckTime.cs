
namespace Breakout.GameTimer;
using System;
using System.Numerics;
using System.Threading.Tasks.Dataflow;
using Breakout.GameEvents;
using Breakout.States;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Timers;
using Breakout.Ball;
using System.Net.NetworkInformation;

public class CheckTime {

    private static StateMachine state;
    private TimerDisplay timerDisplay = new TimerDisplay();
    public CheckTime(TimerDisplay timerDisplay) {
        this.timerDisplay = timerDisplay;
    }
    public CheckTime(StateMachine stateMachine) {
        state = stateMachine;
    }
    public CheckTime(StateMachine stateMachine, TimerDisplay timerDisplay) {
        state = stateMachine;
        this.timerDisplay = timerDisplay;
    }


    public void Check() {
        if (state.ActiveState is GamePaused) {
            timerDisplay.PauseTime();
        } else if (state.ActiveState is GameRunning) {
            if (Ball.checkStart == 1) {
                timerDisplay.PauseTime();
                timerDisplay.ResumeTime();

            } else {
                timerDisplay.PauseTime();
            }
        } else if (state.ActiveState is GameLost || state.ActiveState is GameWon) {
            timerDisplay.PauseTime();
            StaticTimer.RestartTimer();
        } else if (state.ActiveState is MainMenu) {
            timerDisplay.PauseTime();
            StaticTimer.RestartTimer();
        }

    }
    public void NewDisplay(TimerDisplay timerDisplay) {
        this.timerDisplay = timerDisplay;
    }
}


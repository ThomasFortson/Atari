using System;
using System.Numerics;
using System.Threading.Tasks.Dataflow;
using Breakout.GameEvents;
using Breakout.States;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Timers;

namespace Breakout.GameTimer;

public class TimerDisplay : Text {
    private static int totalSeconds;
    public bool IsRenderOn;
    public TimerDisplay() : base(totalSeconds.ToString(), new Vector2(0.1f, 0.1f), 0.5f) {
    }
    public TimerDisplay(int seconds) : base(seconds.ToString(), new Vector2(0.1f, 0.1f), 0.5f) {
        totalSeconds = seconds;
        IsRenderOn = true;
    }
    public static int TotalSeconds => totalSeconds;

    public double GetSeconds() {
        double timeLeft = totalSeconds - Math.Floor(StaticTimer.GetElapsedSeconds());
        if (timeLeft <= 0) {
            StaticTimer.PauseTimer();
        }
        return timeLeft;
    }

    public void ResetTimer(int seconds) {
        totalSeconds = seconds;
        StaticTimer.RestartTimer();
    }

    public void SetTime(int seconds) {
        ResetTimer(seconds);
    }
    public void ResumeTime() {
        StaticTimer.PauseTimer();
        StaticTimer.ResumeTimer();
    }
    public void PauseTime() {
        StaticTimer.ResumeTimer();
        StaticTimer.PauseTimer();
    }

    public virtual void Update() {
        double currentSecondsLeft = this.GetSeconds();
        SetText(currentSecondsLeft.ToString());
        if (currentSecondsLeft <= 0) {
            StaticTimer.PauseTimer();
            TimeRanOutEvent e = new TimeRanOutEvent();
            BreakoutBus.Instance.RegisterEvent<TimeRanOutEvent>(e);
        }
    }

    public void RenderTimer(WindowContext context) {
        if (IsRenderOn)
            this.Render(context);
    }
}

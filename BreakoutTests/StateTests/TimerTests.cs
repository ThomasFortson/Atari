namespace  BreakoutTests;

using Breakout.GameTimer;
using DIKUArcade.Timers;
using System.Threading;
using Breakout.States;

public class TimerTests {
    private TimerDisplay timerDisplay;

    [SetUp]
    public void SetUp() {
        StaticTimer.RestartTimer();
        StaticTimer.PauseTimer();
        timerDisplay = new TimerDisplay(5);
    }

    [Test]
    public void TimerCounts() {
        timerDisplay.ResumeTime();
        Thread.Sleep(1500);
        timerDisplay.Update();
        double remaining = timerDisplay.GetSeconds();
        Assert.That(remaining, Is.EqualTo(4)); //Because of floor = 4 not 3.5
    }

    [Test]
    public void TimerPause() {
        timerDisplay.ResumeTime();
        Thread.Sleep(1000);
        StaticTimer.PauseTimer();
        double timePause = timerDisplay.GetSeconds();
        Thread.Sleep(1000);
        timerDisplay.Update();
        double timeWait = timerDisplay.GetSeconds();
        Assert.That(timePause, Is.EqualTo(timeWait).Within(0.1));
    }

    [Test]
    public void TimerResumesAfterPause() {
        timerDisplay.ResumeTime();
        Thread.Sleep(1000);
        StaticTimer.PauseTimer();
        double timePause = timerDisplay.GetSeconds();
        Thread.Sleep(1000);
        StaticTimer.ResumeTimer();
        Thread.Sleep(1000);
        timerDisplay.Update();
        double resume = timerDisplay.GetSeconds();
        Assert.That(resume, Is.LessThan(timePause - 0.5));
    }

    [Test]
    public void TimerLoseEvent() {
        StateMachine stateMachine = new StateMachine();
        stateMachine.ActiveState = new GameRunning(stateMachine);
        timerDisplay = new TimerDisplay(1);
        timerDisplay.ResumeTime();
        Thread.Sleep(1200);
        timerDisplay.Update();
        stateMachine.ActiveState.Update();
        double remaining = timerDisplay.GetSeconds();
        Assert.That(remaining, Is.LessThanOrEqualTo(0));
        Assert.That(stateMachine.ActiveState, Is.InstanceOf<GameLost>());
    }
}

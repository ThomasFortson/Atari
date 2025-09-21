namespace Breakout;

using Breakout.Lives;
using Breakout.States;
using DIKUArcade.Entities;
using Breakout.Blocks;
using Breakout.GameEvents;

public class WinLossStatus {
    LifeContainer lifeContainer;
    StateMachine stateMachine;
    EntityContainer<Block> level;
    int levelNum;
    int finalLevel;

    private void RegisterTimeRanOut(TimeRanOutEvent e) {
        stateMachine.ActiveState = new GameLost(stateMachine);
    }

    public WinLossStatus(LifeContainer lifeContainer, StateMachine stateMachine, EntityContainer<Block> level, int levelNum) {
        BreakoutBus.Instance.Subscribe<TimeRanOutEvent>(RegisterTimeRanOut);
        this.lifeContainer = lifeContainer;
        this.stateMachine = stateMachine;
        this.level = level;
        finalLevel = levelNum;
    }

    public void Check(int newLevelNum, EntityContainer<Block> newLevel) {
        level = newLevel;
        levelNum = newLevelNum;
        int countNotUnreakables = 0;
        foreach (Block i in newLevel) {
            if (i is not Unbreakable) {
                countNotUnreakables++;
            }
        }
        if (lifeContainer.Lives <= 0) {
            stateMachine.ActiveState = new GameLost(stateMachine);
        }
        if (levelNum == finalLevel && countNotUnreakables == 0) {

            stateMachine.ActiveState = new GameWon(stateMachine);
        }
    }
}
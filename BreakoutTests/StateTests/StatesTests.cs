namespace BreakoutTests;

using Breakout;
using NUnit.Framework;
using System.Numerics;
using Breakout.PlayerEntity;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using DIKUArcade.Input;
using Breakout.PlayerEntity;
using Breakout.Blocks;
using Breakout.States;
using Breakout.Lives;
using Breakout.Level;
using NUnit.Framework.Interfaces;
using System.Net;

public class StatesTests {
    StateMachine state;
    IGameState gameRunning;
    IGameState mainMenu;
    IGameState gamePaused;
    LifeContainer lifeContainer;
    EntityContainer<Block> specificLevel;
    WinLossStatus status;


    [SetUp]
    public void SetUp() {
        state = new StateMachine();
        gameRunning = new GameRunning(state);
        mainMenu = new MainMenu(state);
        gamePaused = new GamePaused(state);
        lifeContainer = new LifeContainer(0);
        specificLevel = LevelLoader.LoadLevel($"BreakoutTests.Assets.Levels.level1.txt");
        status = new WinLossStatus(lifeContainer, state, specificLevel, 2);

    }

    [Test]
    public void TestPreviousRunToPause() {
        state.ActiveState = gameRunning;
        state.ActiveState = gamePaused;
        Assert.That(state.PreviousState, Is.EqualTo(gameRunning));
        Assert.That(state.ActiveState, Is.EqualTo(gamePaused));
    }
    [Test]
    public void TestPreviousPauseToMain() {
        state.ActiveState = gamePaused;
        state.ActiveState = mainMenu;
        Assert.That(state.PreviousState, Is.EqualTo(gamePaused));
        Assert.That(state.ActiveState, Is.EqualTo(mainMenu));
    }
    [Test]
    public void TestPreviousMainToRun() {
        state.ActiveState = mainMenu;
        state.ActiveState = gameRunning;
        Assert.That(state.PreviousState, Is.EqualTo(mainMenu));
        Assert.That(state.ActiveState, Is.EqualTo(gameRunning));
    }

    [TestCase(1)]
    [TestCase(10)]
    [TestCase(100)]
    public void TestLostByLives(int lives) {
        lifeContainer.Lives = lives;
        state.ActiveState = gameRunning;

        while (lifeContainer.Lives != 0) {

            Assert.That(state.ActiveState, Is.InstanceOf<GameRunning>());
            lifeContainer.Lives -= 1;
            status.Check(3, specificLevel);

        }
        Assert.That(state.ActiveState, Is.InstanceOf<GameLost>());
    }

    [Test]
    public void TestGameWon() {
        lifeContainer.Lives = 3;
        state.ActiveState = gameRunning;
        specificLevel = LevelLoader.LoadLevel($"BreakoutTests.Assets.Levels.level1.txt");
        for (int i = 0; i <= 2; i++) {
            Assert.That(state.ActiveState, Is.InstanceOf<GameRunning>());
            specificLevel = LevelLoader.LoadLevel($"BreakoutTests.Assets.Levels.level{(i+1).ToString()}.txt");
            specificLevel.ClearContainer();
            status.Check(i, specificLevel);
        }
        specificLevel.ClearContainer();
        Assert.That(state.ActiveState, Is.InstanceOf<GameWon>());





    }


}
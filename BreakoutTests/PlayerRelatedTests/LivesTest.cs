namespace BreakoutTests.PlayerRelatedTests;

using System;
using DIKUArcade.Entities;
using Breakout;
using Breakout.Lives;
using Breakout.Ball;
using Breakout.States;
using Breakout.Ball.BallMovementStrategy;
using Breakout.GameEvents;
using System.Numerics;
using DIKUArcade.Graphics;

public class LivesTest {
    private LifeContainer lifeContainer;
    private EntityContainer<Ball> ballContainer = new EntityContainer<Ball>();
    private GameRunning state;
    private StateMachine machine;


    [SetUp]
    public void SetUp() {
        lifeContainer = new LifeContainer(2);
        ballContainer = new GenerateBalls().Generate(1, new AutoMove());
        machine = new StateMachine();
        state = new GameRunning(machine);
    }

    // R.1 The value of lives must never be negative.
    [Test]
    public void LivesShouldNotBeNegative() {
        lifeContainer.Lives = -5;
        Assert.That(lifeContainer.Lives, Is.GreaterThanOrEqualTo(0), "Lives should not be negative");
    }


    // S.1 When the last remaining ball leaves the screen, the player loses a life.
    // S.2 As long as balls are in play on the screen, the player should not lose a life.
    [Test]
    public void LivesUnchangedIfBallsStillExist() {
        int initialLives = lifeContainer.Lives;
        Assert.That(ballContainer, Is.Not.Empty);
        Assert.That(lifeContainer.Lives, Is.EqualTo(initialLives), "Lives should not change if balls remain");
    }
    
    // S.3 When the player loses a life, if any lives are remaining, the player will be
    // deducted one life and gain a new ball to launch.
    [Test]
    public void LosingLifeRespawnsBallIfLivesLeft() {
        EntityContainer<Ball> testBallContainer = state.GetBallContainer();
        testBallContainer.ClearContainer();
        Ball testBall = new Ball(new Vector2(0.5f, 0.5f), new Vector2(0.1f, 0.1f), new NoImage(), new AutoMove());
        testBallContainer.AddEntity(testBall);
        state.GetLifeContainer().Lives = 2;
        Assert.That(state.GetLifeContainer().Lives, Is.EqualTo(2));
        Assert.That(testBallContainer.CountEntities(), Is.EqualTo(1));

        testBallContainer.Iterate(ball => ball.AddDieEvent());
        state.Update();

        Assert.That(testBallContainer.CountEntities(), Is.EqualTo(0));
        Assert.That(state.GetLifeContainer().Lives, Is.EqualTo(1));
    }

    // S.4 When the player loses a life with zero remaining, the player is then considered dead and the game should be over.
    [Test]
    public void PlayerHasLostIfNoLivesLeft() {
        GameRunning gameRunningState =new GameRunning(machine);
        machine.ActiveState = gameRunningState;
        LifeContainer playerLifeContainer = gameRunningState.GetLifeContainer();
        
        Assert.That(machine.ActiveState, Is.TypeOf(new GameRunning(machine).GetType()));

        
        playerLifeContainer.Lives = 0;
        playerLifeContainer.Update();
        machine.ActiveState.Update();
        Assert.That(playerLifeContainer.Lives, Is.EqualTo(0), "Player should have 0 lives");
        Assert.That(machine.ActiveState, Is.TypeOf(new GameLost(machine).GetType()));
        
    }
}

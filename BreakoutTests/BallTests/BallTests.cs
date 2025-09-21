namespace BreakoutTests.BallTests;

using Breakout;
using Breakout.Ball;
using Breakout.Ball.BallMovementStrategy;
using Breakout.Blocks;
using Breakout.PlayerEntity;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using NUnit.Framework;
using System.Numerics;


public class BallTests {

    private Ball ball;
    private Player player = new Player();
    EntityContainer<Block> blocks = new EntityContainer<Block>();

    [SetUp]
    public void SetUp() {
        Vector2 pos = new Vector2(0.5f, 0.35f);
        Vector2 dir = new Vector2(0.0f, 1f);
        ball = new Ball(pos, dir, new NoImage(), new AutoMove());
    }

    [Test]
    public void BallMoves() {
        var initialPos = ball.Shape.Position;

        ball.MovementStrategy.Move(player, ball);
        var newPos = ball.Shape.Position;

        Assert.That(newPos, Is.Not.EqualTo(initialPos));
    }

    // R.1 The ball must only be able to leave the screen at the bottom.
    // When a ball leaves the screen, it should be deleted.
    [Test]
    public void BallDeletes() {
        blocks = new EntityContainer<Block>();
        ball.Shape.Position = new Vector2(0.5f, -0.1f); // simulate leaving screen

        ball.MovementStrategy.Move(player, ball); // expected to check for off-screen
        ball.DetectCollision(blocks, player);

        Assert.That(ball.IsDeleted(), Is.True);
    }

    // R.2 If the ball collides with a block, said block must be ’hit’.
    [Test]
    public void BallHitsBlock() {
        ball = new Ball(new Vector2(0.5f, 0.35f), new Vector2(0f, 1f), new NoImage(), new AutoMove());
        DynamicShape shape = new DynamicShape(new Vector2(0.5f, 0.5f), new Vector2(1, 1));
        IBaseImage image = new NoImage();
        Normal normal = new Normal(shape, image);

        blocks.AddEntity(normal);
        blocks = new EntityContainer<Block>();

        ball.Shape.AsDynamicShape().Velocity = new Vector2(0.5f, 0.35f);

        int maxIterations = 500;
        int i = 0;
        bool hit = false;
        float beforeHitPoints = normal.Health;

        while (i < maxIterations || !hit) {

            ball.MovementStrategy.Move(player, ball);
            ball.DetectCollision(blocks, player);
            hit = normal.Health < beforeHitPoints ? false : true;
            i++;
        }

        Assert.That(hit, Is.True);
    }



    // R.3 All balls must at all times move at the same speed.
    // (Hint: The speed of the ball is determined by the length of the vector)
    // R.3: All balls should move at the same speed
    [Test]
    public void BallHasConstantSpeed() {
        blocks = new EntityContainer<Block>();
        ball.MovementStrategy.Move(player, ball);
        float speed = ball.Shape.AsDynamicShape().Velocity.Length();
        ball.MovementStrategy.Move(player, ball);
        Assert.That(ball.Shape.AsDynamicShape().Velocity.Length(), Is.EqualTo(speed), "Ball direction should be normalized");
    }


    // R.4 The ball is unable to get stuck in the same trajectory2
    [Test]
    public void BallIsUnableToGetStuck() {
        blocks = new EntityContainer<Block>();
        ball.MovementStrategy.Move(player, ball);
        float speed = ball.Shape.AsDynamicShape().Velocity.Length();
        ball.MovementStrategy.Move(player, ball);
        Assert.That(ball.Shape.AsDynamicShape().Velocity.Length(), Is.EqualTo(speed), "Ball direction should be normalized");
    }



    // R.5 A ball must always be launched in a positive upward direction.
    // It must always launch more vertically than horizontally.
    [TestCase(1)]
    [TestCase(10)]
    [TestCase(50)]
    [TestCase(100)]
    public void TestBallLaunchedUpward(int ballAmt) {
        blocks = new EntityContainer<Block>();
        EntityContainer<Ball> ballContainer = new GenerateBalls().Generate(ballAmt, new AutoMove());
        ballContainer.Iterate(ball => {
            Assert.That(ball.Direction.Y, Is.GreaterThan(ball.Direction.X));
        });
    }

    [Test]
    public void BallBouncesTest() {
        blocks = new EntityContainer<Block>();
        EntityContainer<Ball> ballContainer = new GenerateBalls().Generate(1, new AutoMove());
        ballContainer.Iterate(ball => {
            Vector2 dir = ball.Direction;
            int maxIterations = 500;
            int i = 0;
            bool hit = false;

            // when ball hits a border, it exits and check that the direction is different
            while (i < maxIterations || !hit) {
                ball.MovementStrategy.Move(player, ball);
                ball.DetectCollision(blocks, player);
                hit = ball.Shape.AsDynamicShape().Velocity != dir;
                i++;
            }

            Assert.That(Vector2.Normalize(ball.Direction), Is.Not.EqualTo(dir));
        }
        );
    }



}
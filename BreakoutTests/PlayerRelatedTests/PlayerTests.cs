namespace BreakoutTests;

using Breakout;
using System.Numerics;
using Breakout.PlayerEntity;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using DIKUArcade.Input;

public class PlayerTests {

    private Player testPlayer;

    [SetUp]
    public void Setup() {
        testPlayer = new Player();
    }

    [Test]
    public void TestExistance() {
        var initPos = testPlayer.Shape.Position;
        Assert.That(initPos.X, Is.InRange(0.0f, 1.0f));
        Assert.That(initPos.Y, Is.InRange(0.0f, 1.0f));
    }

    // R.2 The player should be, upon pressing left or right / a or d, capable of moving horizontally
    [Test]
    public void TestMoveRight() {
        var initPos = testPlayer.Shape.Position.X;
        testPlayer.KeyHandler(KeyboardAction.KeyPress, KeyboardKey.D);
        testPlayer.Move();
        testPlayer.KeyHandler(KeyboardAction.KeyRelease, KeyboardKey.D);

        Assert.That(testPlayer.Shape.Position.X, Is.GreaterThan(initPos));
    }

    // R.2 The player should be, upon pressing left or right / a or d, capable of moving horizontally
    [Test]
    public void TestMoveLeft() {
        var initPos = testPlayer.Shape.Position.X;
        testPlayer.KeyHandler(KeyboardAction.KeyPress, KeyboardKey.A);
        testPlayer.Move();
        testPlayer.KeyHandler(KeyboardAction.KeyRelease, KeyboardKey.A);

        Assert.That(testPlayer.Shape.Position.X, Is.LessThan(initPos));
    }

    // R.1 The player must start in the horizontal center of the screen.
    [Test]
    public void TestPositionIsHorizontalCenter() {
        float expectedCenterX = 0.5f;
        float actualX = testPlayer.Shape.Position.X;

        Assert.That(actualX, Is.EqualTo(expectedCenterX).Within(0.01f));
    }

    // R.3 The player should not be able to exit either side of the screen
    // and movement should be prohibited if it results in the player moving outside of the screen

    [TestCase(16)]
    [TestCase(32)]
    [TestCase(64)]
    public void TestLeftBoundary(int iterations) {
        testPlayer.KeyHandler(KeyboardAction.KeyPress, KeyboardKey.A);

        for (int i = 0; i < iterations; i++) {
            testPlayer.Move();
        }

        testPlayer.KeyHandler(KeyboardAction.KeyRelease, KeyboardKey.A);

        Assert.That(testPlayer.Shape.Position.X, Is.GreaterThanOrEqualTo(0.0f));
    }

    [TestCase(16)]
    [TestCase(32)]
    [TestCase(64)]
    public void TestRightBoundary(int iterations) {
        testPlayer.KeyHandler(KeyboardAction.KeyPress, KeyboardKey.D);

        for (int i = 0; i < iterations; i++) {
            testPlayer.Move();
        }

        testPlayer.KeyHandler(KeyboardAction.KeyRelease, KeyboardKey.D);

        float rightEdge = testPlayer.Shape.Position.X + testPlayer.Shape.Extent.X;
        Assert.That(rightEdge, Is.LessThanOrEqualTo(1.0f));
    }
}
namespace BreakoutTests;

using System;
using System.Numerics;

using Breakout.PowerUps;
using Breakout.PlayerEntity;

using DIKUArcade.Graphics;


public class PowerUpTests {

    private PowerUpEntity powerUp;
    private Player player;

    [SetUp]
    public void SetUp() {
        powerUp = new DoubleSpeedEntity(new Vector2(0.1f, 0.1f));
        player = new Player();
    }


    // R.1 This visual element must move at a constant negative vertical speed.
    [Test]
    public void PowerUpMovesDownward() {
        var initialY = powerUp.Shape.Position.Y;

        powerUp.Move();
        var newY = powerUp.Shape.Position.Y;

        Assert.That(newY, Is.LessThan(initialY), "Power-up should move downwardS");
        Assert.That(powerUp.Shape.AsDynamicShape().Velocity.Y, Is.EqualTo(-0.0025f));
    }

    // S.2 If the visual element collides with the player, the power-up will be considered activated and the visual element should disappear.
    [Test]
    public void PowerUpDisappearsOnCollision() {
        powerUp.Shape.Position = player.Shape.Position;
        powerUp.Move();

        if (powerUp.DetectCollision(player))
            Assert.That(powerUp.IsDeleted(), Is.True, "Power-up should be deleted after activation");
    }


    // S.3 If the vertical position of the visual element is less than the lower window
    // boundary, then it must be marked for deletion and promptly deleted.
    [Test]
    public void PowerUpDeletesWhenOffScreen() {
        powerUp.Shape.Position = new Vector2(0.5f, -0.1f);

        powerUp.Move();

        Assert.That(powerUp.IsDeleted(), Is.True, "Power-up should be deleted after leaving screen bottom");
    }

}

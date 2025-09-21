namespace BreakoutTests;

using Breakout;
using System.Numerics;
using Breakout.PlayerEntity;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using DIKUArcade.Input;
using Breakout.PlayerEntity;
using Breakout.Blocks;
using Breakout.Level;

public class BlockTests {

    Block normal;
    Block unbreakable;
    Block powerup;
    Hardened hardened;

    Teleport teleport;

    IBaseImage image;
    DynamicShape shape;

    [SetUp]
    public void Setup() {
        shape = new DynamicShape(new Vector2(1, 1), new Vector2(1, 1));
        image = new NoImage();
        normal = new Normal(shape, image);
        unbreakable = new Unbreakable(shape, image);
        powerup = new PowerUp(shape, image);
        hardened = new Hardened(shape, image);
        hardened.NoSprite = true;
        teleport = new Teleport(shape, image);

    }

    [Test]
    public void TestHitNormal() {
        var health = normal.Health;
        normal.Hit(5);
        var health2 = normal.Health;
        Assert.That(health, Is.Not.EqualTo(health2));
        Assert.That(normal.Health, Is.EqualTo(health - 5));
    }
    [Test]
    public void TestHitUnbreakable() {
        var health = unbreakable.Health;
        unbreakable.Hit(5);
        var health2 = unbreakable.Health;
        Assert.That(health, Is.EqualTo(health2));
        Assert.That(normal.Health, Is.Not.EqualTo(health - 5));
    }
    [Test]
    public void TestHitPowerUp() {
        var health = powerup.Health;
        powerup.Hit(5);
        var health2 = powerup.Health;
        Assert.That(health, Is.Not.EqualTo(health2));
        Assert.That(powerup.Health, Is.EqualTo(health - 5));
    }
    [Test]
    public void TestHitHardened() {
        var health = hardened.Health;
        hardened.Hit(5);
        var health2 = hardened.Health;
        Assert.That(health, Is.Not.EqualTo(health2));
        Assert.That(hardened.Health, Is.EqualTo(health - 5));
    }

    // [Test]
    // public void TestHitNormalDestroy() {
    //     normal.Health = 5;
    //     normal.Hit(5);
    //     var health = normal.Health;
    //     normal.Hit(10);
    //     var health2 = normal.Health;
    //     bool exist = normal.IsDeleted();
    //     Assert.That(health, Is.EqualTo(0));
    //     Assert.That(health2, Is.EqualTo(0));
    //     Assert.That(exist, Is.EqualTo(true));
    // }

    [Test]
    public void TestHitUnbreakableDestroy() {
        unbreakable.Health = 5;
        unbreakable.Hit(5);
        var health = unbreakable.Health;
        unbreakable.Hit(10);
        var health2 = unbreakable.Health;
        bool exist = unbreakable.IsDeleted();
        Assert.That(health, Is.EqualTo(5));
        Assert.That(health2, Is.EqualTo(5));
        Assert.That(exist, Is.EqualTo(false));
    }
    // [Test]
    // public void TestHitPowerUpDestroy() {
    //     powerup.Health = 5;
    //     powerup.Hit(5);
    //     var health = powerup.Health;
    //     powerup.Hit(10);
    //     var health2 = powerup.Health;
    //     bool exist = powerup.IsDeleted();
    //     Assert.That(health, Is.EqualTo(0));
    //     Assert.That(health2, Is.EqualTo(0));
    //     Assert.That(exist, Is.EqualTo(true));
    // }

    [Test]
    public void TestHitHardenedDestroy() {
        hardened.Health = 5;
        hardened.Hit(5);
        int health = hardened.Health;
        hardened.Hit(10);
        int health2 = hardened.Health;
        bool exist = hardened.IsDeleted();
        Assert.That(health, Is.EqualTo(0));
        Assert.That(health2, Is.EqualTo(0));
        Assert.That(exist, Is.EqualTo(true));
    }

    // [Test]
    // public void TestHitTeleport() {
    //     var path = $"BreakoutTests.Assets.Levels.TeleportTest.txt";
    //     EntityContainer<Block> level = LevelLoader.LoadLevel(path);
    //     teleport.BlockContainer = level;
    //     teleport.Hit(5);
    //     foreach (Block block in level) {
    //         Assert.That(teleport.Shape.Position.X, Is.Not.EqualTo(block.Shape.Position.X));
    //         Assert.That(teleport.Shape.Position.Y, Is.Not.EqualTo(block.Shape.Position.Y));

    //     }

    // }


}

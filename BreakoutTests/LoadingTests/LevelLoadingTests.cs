namespace BreakoutTests;

using Breakout;
using System.Numerics;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using Breakout.Level;
using Breakout.Blocks;
using NUnit.Framework;

[TestFixture]
public class LevelLoadingTests {

    [SetUp]
    public void Setup() {

    }

    [TestCase("level1.txt")]
    [TestCase("level2.txt")]
    [TestCase("level3.txt")]
    [TestCase("wall.txt")]
    [TestCase("central-mass.txt")]
    [TestCase("columns.txt")]
    public void TestLevelLoadingNotNull(string path) {
        path = $"BreakoutTests.Assets.Levels.{path}";
        EntityContainer<Block> level = LevelLoader.LoadLevel(path);
        Assert.That(level, Is.Not.Null);
    }

    [TestCase("level1.txt")]
    [TestCase("level2.txt")]
    [TestCase("level3.txt")]
    [TestCase("wall.txt")]
    [TestCase("central-mass.txt")]
    [TestCase("columns.txt")]
    public void TestLevelLoadingInScreen(string path) {
        double tolerance = 0.0005; // Half of one decimal place

        path = $"BreakoutTests.Assets.Levels.{path}";
        EntityContainer<Block> level = LevelLoader.LoadLevel(path);
        foreach (Entity block in level) {
            var shape = block.Shape;
            Assert.That(shape.Position.X, Is.GreaterThanOrEqualTo(0).Within(tolerance));
            Assert.That(shape.Position.Y, Is.GreaterThanOrEqualTo(0).Within(tolerance));
            Assert.That(shape.Position.X, Is.LessThanOrEqualTo(1));
            Assert.That(shape.Position.Y, Is.LessThanOrEqualTo(1));

        }
    }


}
namespace Breakout.PowerUps;

using System;
using System.Numerics;


public static class PowerUpGenerator {
    private static Random rng = new Random();

    private static List<Func<Vector2, PowerUpEntity>> powerUpFactories = new List<Func<Vector2, PowerUpEntity>>
    {
        pos => new AddHealthEntity(pos),
        pos => new DoubleSpeedEntity(pos),
        pos => new SplitEntity(pos),
        pos => new HazardEntity(pos),
        pos => new DoubleTimeEntity(pos),
        pos => new HalfTimeHazardEntity(pos),
        pos => new BiggerBallEntity(pos),

    };
    public static PowerUpEntity Generate(Vector2 position) {
        int index = rng.Next(powerUpFactories.Count);
        return powerUpFactories[index](position);
    }
}

namespace Breakout.PowerUps;
using System;
using System.Numerics;


public static class HazardGenerator {
    private static Random rng = new Random();

    private static List<Func<Vector2, PowerUpEntity>> powerUpFactories = new List<Func<Vector2, PowerUpEntity>>
    {
        pos => new HazardEntity(pos),

    };
    public static PowerUpEntity Generate(Vector2 position) {
        int index = rng.Next(powerUpFactories.Count);
        return powerUpFactories[index](position);
    }
}

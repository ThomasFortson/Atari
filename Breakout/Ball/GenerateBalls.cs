namespace Breakout.Ball;

using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.Numerics;
using Breakout.Ball.BallMovementStrategy;

public class GenerateBalls {

    public GenerateBalls() {
    }

    public EntityContainer<Ball> Generate(int amount, IMovementStrategy strategy) {
        float variance = 0.1f;
        EntityContainer<Ball> balls = new EntityContainer<Ball>();
        Image ballImage = new Image("Breakout.Assets.Images.ball.png");

        Random random = new Random();
        for (int i = 0; i < amount; i++) {
            float x1 = 0f + (i - 1f) * variance;
            float y1 = 0.25f;
            Vector2 v1 = new Vector2(x1, y1);

            // Generate a tight spread of upward angles
            float angleSpread = 25f * (float) (Math.PI / 180f); // 15 degrees in radians
            float baseAngle = 120f * (float) (Math.PI / 180f); // Straight up (negative y if y goes down)

            float angle = baseAngle + ((float) random.NextDouble() - 0.5f) * angleSpread;

            Vector2 v2 = new Vector2((float) Math.Cos(angle), (float) Math.Sin(angle));
            balls.AddEntity(new Ball(v1, Vector2.Normalize(v2), ballImage, strategy));
        }
        return balls;
    }

    public EntityContainer<Ball> SplitBalls(int amount, IMovementStrategy strategy, Vector2 pos, Vector2 vel) {
        float variance = 0.001f;
        EntityContainer<Ball> balls = new EntityContainer<Ball>();
        Image ballImage = new Image("Breakout.Assets.Images.ball.png");

        Random random = new Random();
        for (int i = 0; i < amount; i++) {
            Vector2 varvec = Vector2.Normalize(vel);
            varvec = new Vector2(varvec.Y, varvec.X);
            float x1 = Math.Clamp(pos.X + varvec.X * variance * i / amount - variance / 2, 0, 1);
            float y1 = Math.Clamp(pos.Y + varvec.Y * variance * i / amount - variance / 2, 0, 1);

            Vector2 v1 = new Vector2(x1, y1);

            float angleSpread = i * 50f * (float) (Math.PI / 180f) - 25f * (float) (Math.PI / 180f);
            float baseAngle = (float) Math.Atan2((double) vel.Y, (double) vel.X);
            float angle = baseAngle + ((float) random.NextDouble() - 0.5f) * angleSpread;
            Vector2 v2 = new Vector2((float) Math.Cos(angle), (float) Math.Sin(angle));
            balls.AddEntity(new Ball(v1, Vector2.Normalize(v2), ballImage, strategy));
        }
        return balls;
    }
}

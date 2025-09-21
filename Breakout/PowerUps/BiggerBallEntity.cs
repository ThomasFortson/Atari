namespace Breakout.PowerUps;

using System;
using System.Numerics;
using DIKUArcade.Graphics;


public class BiggerBallEntity : PowerUpEntity {
    protected override PowerUpType PowerUpType => PowerUpType.BiggerBall;

    public BiggerBallEntity(Vector2 position) : base(position, new Image("Breakout.Assets.Images.BigPowerUp.png")) { }

}

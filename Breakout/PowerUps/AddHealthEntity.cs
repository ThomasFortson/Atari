namespace Breakout.PowerUps;

using System;
using System.Numerics;
using DIKUArcade.Graphics;


public class AddHealthEntity : PowerUpEntity {
    protected override PowerUpType PowerUpType => PowerUpType.AddHealth;

    public AddHealthEntity(Vector2 position) : base(position, new Image("Breakout.Assets.Images.LifePickUp.png")) { }

}

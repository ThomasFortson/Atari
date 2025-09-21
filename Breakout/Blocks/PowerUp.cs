namespace Breakout.Blocks;

using System;
using DIKUArcade;
using DIKUArcade.Events;
using DIKUArcade.Input;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Entities;
using Breakout.Score;
using Breakout.GameEvents;

public class PowerUp : Block {
    private bool hasHitDeath = false;
    public PowerUp(DynamicShape shape, IBaseImage image) : base(shape, image, 1, 2) {
    }

    public override void Hit(int damage) {
        Health -= damage;
        if (Health <= 0 && !hasHitDeath) {
            hasHitDeath = true;
            BlockDeath();

            var e = new PowerUpBlockDeathEvent(Shape.Position);
            BreakoutBus.Instance.RegisterEvent<PowerUpBlockDeathEvent>(e);
            this.DeleteEntity();
        }
    }
}
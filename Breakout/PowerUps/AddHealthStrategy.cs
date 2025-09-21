namespace Breakout.PowerUps;

using System;
using System.Numerics;
using Breakout.PlayerEntity;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using Breakout.GameEvents;
using Breakout;


public class AddHealthStrategy : IBasePowerUp {
    const int HEALTH_ADD_AMOUNT = 1;
    protected PowerUpType PowerUpType => PowerUpType.AddHealth;
    private Player player;

    public AddHealthStrategy(Player p) {
        player = p;
    }

    public override void Setup() {
        BreakoutBus.Instance.RegisterEvent(new AddHealthEvent(HEALTH_ADD_AMOUNT));
        Disengage();
    }

    public override void Update(float t) {
    }


}

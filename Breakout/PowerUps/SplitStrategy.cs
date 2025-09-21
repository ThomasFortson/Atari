namespace Breakout.PowerUps;

using System;
using System.Dynamic;
using System.Numerics;
using Breakout.GameEvents;
using Breakout.PlayerEntity;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

public class SplitStrategy : IBasePowerUp {

    private Player player;

    public SplitStrategy(Player p) {
        player = p;
    }

    public override void Setup() {
        BreakoutBus.Instance.RegisterEvent(new SplitBallEvent());
        Disengage();
    }


}

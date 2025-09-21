namespace Breakout.PowerUps;

using Breakout.PlayerEntity;
using DIKUArcade.Timers;

public class HazardPowerUp : IBasePowerUp {
    const float SPEED_MULTIPLIER = 0.5f;

    private Player player;


    public HazardPowerUp(Player p) {
        IsHazard = true;
        TimeCreated = (float) StaticTimer.GetElapsedSeconds();
        player = p;
    }

    public override void Setup() {
        player.SpeedMultiplier = SPEED_MULTIPLIER;

    }

    public override void Update(float t) {
        t = (float) StaticTimer.GetElapsedSeconds();
        if (t - TimeCreated > 5) {
            Disengage();
        }
    }

    public override void Disengage() {
        player.SpeedMultiplier = 1.0f;
        IsDeleted = true;
    }
}
namespace Breakout.PowerUps;

using Breakout.PlayerEntity;

public class IBasePowerUp {
    public bool IsHazard;
    public float TimeCreated;
    public bool IsDeleted;

    public PowerUpType PowerUpTypeP = PowerUpType.NoPowerUp;

    public IBasePowerUp() {

    }

    public virtual void Setup() {


    }

    public virtual void Update(float t) {


    }

    public virtual void Disengage() {
        IsDeleted = true;

    }

}
namespace Breakout.PowerUps;

using System;
using System.Numerics;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Physics;
using Breakout.GameEvents;
using Breakout.PlayerEntity;


public abstract class PowerUpEntity : Entity {
    private bool oneColission = false;

    protected abstract PowerUpType PowerUpType {
        get;
    }
    public PowerUpEntity(Vector2 position, Image image) : base(new DynamicShape(position, new Vector2(0.05f, 0.05f)), image) {

    }

    public void Move() {
        Shape.AsDynamicShape().Velocity = new Vector2(0f, -0.0025f);
        Shape.Move();
        if (this.Shape.Position.Y < 0f) {
            this.DeleteEntity();
        }
    }

    public bool DetectCollision(Player player) {
        CollisionData poCollisionData = CollisionDetection.Aabb(Shape.AsDynamicShape(), player.Shape.AsDynamicShape());
        CollisionData plCollisionData = CollisionDetection.Aabb(player.Shape.AsDynamicShape(), Shape.AsDynamicShape());
        if (poCollisionData.Collision || plCollisionData.Collision) {
            DeleteEntity();
            if (oneColission == false) {
                oneColission = true;
                var e = new PowerUpCollisionEvent(PowerUpType);
                BreakoutBus.Instance.RegisterEvent<PowerUpCollisionEvent>(e);

            }
        }
        return poCollisionData.Collision || plCollisionData.Collision;
    }
}

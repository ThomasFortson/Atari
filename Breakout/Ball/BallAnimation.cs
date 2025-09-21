namespace Breakout.Ball;
using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;


public class BallAnimation : AnimationContainer {
    private const int ANIMATION_DURATION = 300;
    private List<Image> puffStrides = ImageStride.CreateStrides(4, "Breakout.Assets.Images.PuffOfSmoke.png");
    public BallAnimation() : base(4) { }

    public void TriggerAnimation(Vector2 position, Vector2 extent) {
        StationaryShape shape = new StationaryShape(position, extent);
        this.AddAnimation(shape, ANIMATION_DURATION / 4, new ImageStride(100, puffStrides));
    }
}

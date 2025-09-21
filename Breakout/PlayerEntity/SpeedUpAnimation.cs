using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.PlayerEntity;

public class SpeedUpAnimation {
    private List<Image> images = ImageStride.CreateStrides(3, "Breakout.Assets.Images.playerStride.png");
    public SpeedUpAnimation() {
    }

    public ImageStride Stride() {
        var stride = new ImageStride(80, images[0], images[1], images[2]);
        return stride;
    }
}

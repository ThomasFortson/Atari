namespace Breakout.Score;

using System.Numerics;
using DIKUArcade.Entities;
using DIKUArcade.Font;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;

public class ScoreContainer : Entity {
    private ScoreText scoreText;

    public ScoreContainer() : base(new StationaryShape(0.8f, 0.9f, 0.15f, 0.06f), new Image("Breakout.Assets.Images.emptyPoint.png")) {
        scoreText = new ScoreText(new Vector2(0.87f, 0.925f), 0.35f);
    }

    public void Update() {
        scoreText.Update();
    }

    public void RenderScore(WindowContext context) {
        this.Image.Render(context, this.Shape);
        scoreText.Render(context);
    }
}
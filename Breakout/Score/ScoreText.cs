namespace Breakout.Score;

using System.Numerics;
using DIKUArcade.Entities;
using DIKUArcade.Font;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;

public class ScoreText : Text {

    private ScoreManager scoreManager;

    public ScoreText(Vector2 position, float scale) : base("00", position, scale) {
        scoreManager = new ScoreManager();
    }

    public void Update() {
        scoreManager.Update();
        this.SetColor(255, 0, 0);
        SetText($"{scoreManager.GetScore()}");
    }

}
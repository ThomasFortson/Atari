namespace Breakout.Score;

using DIKUArcade;
using DIKUArcade.Graphics;
using Breakout.GameEvents;

public class ScoreManager {
    private int score = 0;

    public ScoreManager() {
        this.score = 0;
        BreakoutBus.Instance.Subscribe<BlockDeathEvent>(AddScoreEvent);
    }

    public int GetScore() {
        return score;
    }

    private void AddScore(int amount) {
        score += amount;
    }

    private void AddScoreEvent(BlockDeathEvent deathEvent) {
        this.AddScore(deathEvent.Points);
    }

    public void Update() {
    }
}
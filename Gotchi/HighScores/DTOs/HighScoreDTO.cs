namespace Gotchi.HighScores.DTOs;

public class HighScoreDTO
{
    public string? UserName { get; set; }
    public string? GotchiName { get; set; }
    public int Score { get; set; }
    public DateTime DateSet { get; set; }
    public TimeSpan TimeGotchiAlive { get; set; }
}

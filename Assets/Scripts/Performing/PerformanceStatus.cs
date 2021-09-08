public class PerformanceStatus
{
    private int performingScore;
    private int multiplier;
    public int PerformingScore { get { return performingScore; } }
    public int Multiplier { get { return multiplier; } }

    public PerformanceStatus()
    {
        performingScore = 0;
        multiplier = 1;
    }

    public void ModifyMultiplier(AttackBuff attack)
    {
        multiplier = DanceMoveEffectConversion.GetBuffedMultiplier(multiplier, attack);
        if (multiplier < 1)
            ResetMultiplier();
    }

    public void ModifyMultiplier(DefenseBuff defense)
    {
        multiplier = DanceMoveEffectConversion.GetBuffedMultiplier(multiplier, defense);
        if (multiplier < 1)
            ResetMultiplier();
    }

    public void IncreaseScore(int score)
    {
        performingScore += score * Multiplier;
    }

    public void ResetMultiplier()
    {
        multiplier = 1;
    }
}
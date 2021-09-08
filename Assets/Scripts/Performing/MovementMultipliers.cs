public enum DefenseBuff
{
    //RecoverStreak,
    StreakPlus3,
    StreakPlus5,
}

public enum AttackBuff
{
    DecreaseStreak,
    HalfStreak,
    //BlockScore,
    //BlockAttack,
    //BlockDefense,
}

public static class DanceMoveEffectConversion
{
    public static int GetBuffedMultiplier(int current, DefenseBuff defenseBuff)
    {
        int final = current;
        switch (defenseBuff)
        {
            case DefenseBuff.StreakPlus3:
                final += 3;
                break;
            case DefenseBuff.StreakPlus5:
                final += 5;
                break;
        }
        return final;
    }

    public static int GetBuffedMultiplier(int current, AttackBuff attackBuff)
    {
        int final = current;
        switch (attackBuff)
        {
            case AttackBuff.DecreaseStreak:
                final -= 2;
                break;
            case AttackBuff.HalfStreak:
                final /= 2;
                break;
        }
        return final;
    }
}

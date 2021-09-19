public static class PerformanceConversions
{
    public static MoveType ConvertMoveTypeFromIndex(int index)
    {
        switch (index)
        {
            case 0:
                return MoveType.Score;
            case 1:
                return MoveType.Defense;
            case 2:
                return MoveType.Attack;
        }
        return MoveType.Score;
    }

    public static int ConvertIndexFromMoveType(MoveType moveType)
    {
        switch (moveType)
        {
            case MoveType.Score:
                return 0;
            case MoveType.Defense:
                return 1;
            case MoveType.Attack:
                return 2;
        }
        return 0;
    }

    public static int MoveTypesQuantity { get { return 3; } }
}
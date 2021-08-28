public static class PerformanceConversions
{
    public static MoveType ConvertMoveTypeFromIndex(int index)
    {
        switch (index)
        {
            case 0:
                return MoveType.AType;
            case 1:
                return MoveType.BType;
            case 2:
                return MoveType.XType;
            case 3:
                return MoveType.YType;
        }
        return MoveType.AType;
    }

    public static int ConvertIndexFromMoveType(MoveType moveType)
    {
        switch (moveType)
        {
            case MoveType.AType:
                return 0;
            case MoveType.BType:
                return 1;
            case MoveType.XType:
                return 2;
            case MoveType.YType:
                return 3;
        }
        return 0;
    }
}
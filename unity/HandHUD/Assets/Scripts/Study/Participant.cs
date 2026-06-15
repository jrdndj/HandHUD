using System;

namespace Study
{
    public class Participant
    {
        public SuperBlock SuperBlock { get; }
        public int LatinSquareGroup { get; }

        public Participant(int latinSquareGroup, SuperBlock superBlock)
        {
            if (latinSquareGroup < 0 || latinSquareGroup >= StudyConfig.LatinSquareGroupCount)
                throw new ArgumentException(
                    $"latinSquareGroup must be between 0 and {StudyConfig.LatinSquareGroupCount - 1}");

            LatinSquareGroup = latinSquareGroup;
            SuperBlock = superBlock;
        }
    }
}
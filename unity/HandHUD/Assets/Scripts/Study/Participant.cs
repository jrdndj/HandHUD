using static Study.StudyConfig;

namespace Study
{
    public class Participant
    {
        public readonly int ParticipantNo;
        public readonly int LatinSquareGroup;
        public readonly SuperBlock SuperBlockGroup;

        public Participant(int participantNo)
        {
            int idx = (participantNo - 1) % (ConditionsPerSuperBlock * SuperBlockCount);
            int latinSquareGroup = idx % LatinSquareGroupCount;

            ParticipantNo = participantNo;
            SuperBlockGroup = idx < 3
                ? SuperBlock.NoGlove
                : SuperBlock.Glove;
            ;
            LatinSquareGroup = latinSquareGroup;
        }
    }
}
namespace Character.View.Npc.States
{
    public struct NextState
    {
        public float Probability { get; }
        public IState State { get; }

        public NextState(float probability, IState state)
        {
            Probability = probability;
            State = state;
        }
    }
}
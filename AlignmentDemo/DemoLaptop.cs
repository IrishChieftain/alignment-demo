namespace AlignmentDemo
{
    public enum LaptopState
    {
        Available,
        CheckedOut,
        ReturnReported
    }

    public sealed class DemoLaptop
    {
        private readonly object stateLock = new object();

        public LaptopState State { get; private set; } = LaptopState.Available;

        public bool TryCheckOut()
        {
            return TryTransition(LaptopState.Available, LaptopState.CheckedOut);
        }

        public bool TryReportReturn()
        {
            return TryTransition(LaptopState.CheckedOut, LaptopState.ReturnReported);
        }

        public bool TryConfirmReceipt()
        {
            return TryTransition(LaptopState.ReturnReported, LaptopState.Available);
        }

        private bool TryTransition(LaptopState expectedState, LaptopState nextState)
        {
            lock (stateLock)
            {
                if (State != expectedState)
                {
                    return false;
                }

                State = nextState;
                return true;
            }
        }
    }
}

namespace AbstractLL
{
    public abstract class AbstractEnvironment<T>
    {
        public int CurrentRow { get; set; }
        public int CurrentColumn { get; set; }
        public string[] Text { get; set; } = default!;

        protected AbstractEnvironment()
        {
            CurrentRow = CurrentColumn = 0;
            Inicializa();
            Errors = new List<string>();
        }

        public bool EndOfText => CurrentRow >= Text.Length;

        public char NextChar()
        {
            if (CurrentColumn >= Text[CurrentRow].Length)
            {
                CurrentRow++;
                CurrentColumn = 0;
                return '\n';

            }

            char ch = Text[CurrentRow][CurrentColumn];

            CurrentColumn++;

            return ch;
        }

        public void Retract()
        {
            if (CurrentColumn == 0)
            {
                CurrentRow--;
                CurrentColumn = Text[CurrentRow].Length;
            }
            else
                CurrentColumn--;
        }

        // Informações compartilhadas entre parser, scanner e semantic
        public abstract T? Result { get; }
        public bool Success { get; set; }
        public List<string> Errors { get; set; }

        public abstract void Inicializa();
    }
}

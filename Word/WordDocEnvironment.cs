using AbstractLL;

namespace Word
{
    public enum Context
    {
        ElementEnv,
        ParameterEnv,
        StringEnv,
        RunEnv,
        PathEnv,
        CodeEnv,
        VerbatimEnv,
    };

    public enum ListTypeValues
    {
        Itemize,
        Enumerate,
        None,
    };

    public class WordDocEnvironment : AbstractEnvironment<WordDocument>
    {
        protected static readonly Stack<object> contextStack = new();

        public override WordDocument Result => wordDocument;

        private readonly WordDocument wordDocument;
        public WordDocument WordDocument { get { return wordDocument; } }

        public Context Context { get; set; }
        public ListTypeValues ListType { get; set; }
        public int Level { get; set; }

        public WordDocEnvironment(WordDocument wordDocument)
        {
            this.wordDocument = wordDocument;
            Inicializa();
        }

        public WordDocEnvironment(WordDocEnvironment environment)
        {
            this.wordDocument = environment.wordDocument;
            Inicializa();
        }

        public void ChangeContext(Context context)
        {
            Save();
            Context = context;
        }

        public void RestoreContext()
        {
            Restore();
        }

        public virtual void Restart()
        {
            Context = Context.ElementEnv;
            Level = 0;
            ListType = ListTypeValues.None;
        }

        public void IncrementLevel() => Level++;

        public void DecrementLevel() { if (Level > 0) Level--; }

        public virtual void Save()
        {
            contextStack.Push(Context);
            contextStack.Push(Level);
            contextStack.Push(ListType);
        }

        public virtual bool Restore()
        {
            if (contextStack.Count == 0)
                return false;

            ListType = (ListTypeValues)contextStack.Pop();
            Level = (int)contextStack.Pop();
            Context = (Context)contextStack.Pop();

            return true;
        }

        public override void Inicializa()
        {
            Restart();
        }
    }
}

using System.Timers;

namespace UserManager.App.Components.Toaster
{
    public class ToasterService : IDisposable
    {
        private readonly List<Toast> _toastList = new List<Toast>();
        private System.Timers.Timer _timer = new System.Timers.Timer();
        public event EventHandler? ToasterChanged;
        public event EventHandler? ToasterTimerElapsed;
        public bool HasToasts => _toastList.Count > 0;

        public ToasterService()
        {
            AddToast(new Toast { Title = "Todo", Message = "Resolver problema com a tecla ENTER nas entradas de filtro", TimeToBurn = DateTimeOffset.Now.AddSeconds(10) });
            _timer.Interval = 5000;
            _timer.AutoReset = true;
            _timer.Elapsed += this.TimerElapsed;
            _timer.Start();
            AddToast(new Toast { Title = "Todo", Message = "Implementar edição e criação das entidades nas telas LIST", TimeToBurn = DateTimeOffset.Now.AddSeconds(10) });
            _timer.Interval = 5000;
            _timer.AutoReset = true;
            _timer.Elapsed += this.TimerElapsed;
            _timer.Start();
            AddToast(new Toast { Title = "Todo", Message = "Atualizar componente de navegação ao sair dos detalhes", TimeToBurn = DateTimeOffset.Now.AddSeconds(10) });
            _timer.Interval = 5000;
            _timer.AutoReset = true;
            _timer.Elapsed += this.TimerElapsed;
            _timer.Start();
        }

        public List<Toast> GetToasts()
        {
            ClearBurntToast();
            return _toastList;
        }

        private void TimerElapsed(object? sender, ElapsedEventArgs e)
        {
            this.ClearBurntToast();
            this.ToasterTimerElapsed?.Invoke(this, EventArgs.Empty);
        }

        private void AddToast(Toast toast)
        {
            _toastList.Add(toast);
            // only raise the ToasterChanged event if it hasn't already been raised by ClearBurntToast
            if (!this.ClearBurntToast())
                this.ToasterChanged?.Invoke(this, EventArgs.Empty);
        }

        public void AddToastError(string message, int seconds = 10)
            => AddToast(Toast.NewToast("Erro", message, MessageColour.Danger, seconds));
        public void AddToastSuccess(string message, int seconds = 5)
            => AddToast(Toast.NewToast("Sucesso", message, MessageColour.Success, seconds));
        public void AddToastWarning(string message, int seconds = 7)
            => AddToast(Toast.NewToast("Atenção", message, MessageColour.Warning, seconds));

        public void ClearToast(Toast toast)
        {
            if (_toastList.Contains(toast))
            {
                _toastList.Remove(toast);
                // only raise the ToasterChanged event if it hasn't already been raised by ClearBurntToast
                if (!this.ClearBurntToast())
                    this.ToasterChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private bool ClearBurntToast()
        {
            var toastsToDelete = _toastList.Where(item => item.IsBurnt).ToList();
            if (toastsToDelete is not null && toastsToDelete.Count > 0)
            {
                toastsToDelete.ForEach(toast => _toastList.Remove(toast));
                this.ToasterChanged?.Invoke(this, EventArgs.Empty);
                return true;
            }
            return false;
        }

        public void Dispose()
        {
            if (_timer is not null)
            {
                _timer.Elapsed += this.TimerElapsed;
                _timer.Stop();
            }

            GC.SuppressFinalize(this);
        }
    }
}

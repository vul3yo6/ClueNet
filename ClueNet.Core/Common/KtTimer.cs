using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace ClueNet.Core.Common
{
    internal class KtTimer
    {
        private Timer _timer;
        private Action _action;
        public event EventHandler<ExceptionEventArgs> Exception;

        private bool _isRunning;
        private readonly object _lockOfRunning = new object();

        public string Name { get; private set; }

        public bool IsRunning
        {
            get { return _isRunning; }
            set 
            {
                lock (_lockOfRunning)
                {
                    _isRunning = value;
                }
            }
        }

        public KtTimer(string name, double interval, Action action)
        {
            Name = name;
            _action = action;

            _timer = new Timer(interval);
            _timer.Elapsed += Timer_Elapsed;
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (IsRunning)
            {
                return;
            }

            IsRunning = true;

            try
            {
                _action?.Invoke();
            }
            catch (Exception ex)
            {
                Exception?.Invoke(this, new ExceptionEventArgs(Name, ex));
            }
            finally
            {
                IsRunning = false;
            }
        }
    }

    internal class ExceptionEventArgs : EventArgs
    {
        public string Source { get; set; }
        public Exception Ex { get; set; }

        public ExceptionEventArgs(string source, Exception ex)
        {
            Source = source;
            Ex = ex;
        }
    }
}

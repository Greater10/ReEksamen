using System;
using System.ComponentModel;
using System.Collections.Specialized;

namespace DeltaProject.ViewModel
{
    public delegate bool ConfirmMessage(object sender, MessageEventArgs e);
    public delegate void WarningMessage(object sender, MessageEventArgs e);
    public delegate void ErrorMessage(object sender, MessageEventArgs e);

    public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable, INotifyCollectionChanged
    {
        public event ErrorMessage ErrorHandler;
        public event WarningMessage WarningHandler;
        public event ConfirmMessage ConfirmHandler;
        public event PropertyChangedEventHandler PropertyChanged;
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public event EventHandler CloseHandler;
        public RelayCommand CloseCommand { get; private set; }

        public ViewModelBase()
        {
            CloseCommand = new RelayCommand(p => { if (CloseHandler != null) CloseHandler(this, EventArgs.Empty); });
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnCollectionChanged(NotifyCollectionChangedAction action, object item)
        {
            if (CollectionChanged != null) CollectionChanged(this, new NotifyCollectionChangedEventArgs(action, item));
        }

        public void Dispose()
        {
            this.OnDispose();
        }

        protected virtual void OnDispose()
        {
        }

        public void OnWarning(string message)
        {
            if (WarningHandler != null) WarningHandler(this, new MessageEventArgs(message));
        }

        public void OnError(string error)
        {
            if (ErrorHandler != null) ErrorHandler(this, new MessageEventArgs(error));
        }

        public bool OnConfirm(string message)
        {
            if (ConfirmHandler != null) return ConfirmHandler(this, new MessageEventArgs(message));
            return false;
        }

        public void OnClose()
        {
            if (CloseHandler != null) CloseHandler(this, EventArgs.Empty);
        }
    }
}

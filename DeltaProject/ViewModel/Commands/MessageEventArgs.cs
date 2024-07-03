using System;

namespace DeltaProject.ViewModel
{
    public class MessageEventArgs : EventArgs
  {
    public string Message { get; private set; }

    public MessageEventArgs(string message)
      : base()
    {
      Message = message;
    }
  }
}

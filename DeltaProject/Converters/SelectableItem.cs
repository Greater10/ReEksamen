using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace DeltaProject.Converters
{
    public class SelectableItem : INotifyPropertyChanged
    {
        private string _data;
        public string Data
        {
            get { return _data; }
            set
            {
                _data = value;
                OnPropertyChanged(nameof(Data));
            }
        }

        private ObservableCollection<string> _selectedItems;
        public ObservableCollection<string> SelectedItems
        {
            get { return _selectedItems; }
            set
            {
                _selectedItems = value;
                OnPropertyChanged(nameof(SelectedItems));
            }
        }

        public bool IsChecked
        {
            get { return SelectedItems.Contains(Data); }
            set
            {
                if (value && !SelectedItems.Contains(Data))
                    SelectedItems.Add(Data);
                else if (!value && SelectedItems.Contains(Data))
                    SelectedItems.Remove(Data);

                OnPropertyChanged(nameof(IsChecked));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

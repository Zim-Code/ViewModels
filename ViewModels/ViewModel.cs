using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZimCode.ViewModels
{
    /// <summary>
    /// The base view model. This implements <see cref="System.ComponentModel.INotifyPropertyChanged"/>.
    /// </summary>
    public class ViewModel : IHaveLabel
    {
        string label;

        /// <summary>
        /// The user facing value to be used if it is needed to be represented as a string.
        /// </summary>
        public string Label
        {
            get { return label; }
            protected set
            {
                if (label == value) return;
                label = value;
                OnPropertyChanged(nameof(Label).NewPropertyChangedEventArgs());
            }
        }

        /// <summary>
        /// Invoked when a property has changed.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e) =>
            PropertyChanged?.Invoke(this, e);

        /// <summary>
        /// Raised when a property has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}

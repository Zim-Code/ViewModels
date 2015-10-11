using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZimCode.ViewModels
{
    /// <summary>
    /// A <see cref="ZimCode.ViewModels.ViewModelCollection"/> with a label.
    /// </summary>
    public class LabeledViewModelCollection : ViewModelCollection, IHaveLabel
    {
        string label;

        /// <summary>
        /// Initialize a new <see cref="ZimCode.ViewModels.LabeledViewModelCollection"/>.
        /// </summary>
        /// <param name="label">The user facing value to be displayed if need be.</param>
        /// <param name="collection">The collection to wrap.</param>
        /// <param name="viewModelGenerator">The func used to generate the view models.</param>
        public LabeledViewModelCollection(string label, IEnumerable collection, Func<object, object> viewModelGenerator)
            : base(collection, viewModelGenerator)
        {
            Label = label;
        }

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

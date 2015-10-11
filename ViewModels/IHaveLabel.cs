using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZimCode.ViewModels
{
    public interface IHaveLabel : INotifyPropertyChanged
    {
        /// <summary>
        /// The user facing value to be used if it is needed to be represented as a string.
        /// </summary>
        string Label { get; }
    }
}

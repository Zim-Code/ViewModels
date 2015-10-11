using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZimCode.ViewModels
{
    /// <summary>
    /// Extension methods for <see cref="System.String" />'s to help in the ViewModels.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Create a new <see cref="System.ComponentModel.PropertyChangedEventArgs"/> object from this string.
        /// </summary>
        /// <returns>A new <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance.</returns>
        public static PropertyChangedEventArgs NewPropertyChangedEventArgs(this string str)
            => new PropertyChangedEventArgs(str);
    }
}

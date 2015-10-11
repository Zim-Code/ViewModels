using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZimCode.ViewModels;

namespace ViewModels.Tests.Helpers
{
    class HelperLabeledViewModelCollection : LabeledViewModelCollection
    {
        public HelperLabeledViewModelCollection(string label, IEnumerable collection, Func<object, object> viewModelGenerator)
            : base(label, collection, viewModelGenerator)
        {
        }

        public void SetLabel(string value) => Label = value;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZimCode.ViewModels;

namespace ViewModels.Tests.Helpers
{
    class HelperViewModel : ViewModel
    {
        public void SetLabel(string value) => Label = value;
    }
}

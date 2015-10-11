using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModels.Tests.Helpers;
using System.Threading;

namespace ViewModels.Tests
{
    [TestClass]
    public class Test_ViewModel
    {
        [TestMethod]
        public void TestLabelAndNotifyPropertyChanged()
        {
            const string labelValue = "TestLabelValue";

            ManualResetEvent waitHandle = new ManualResetEvent(false);
            bool eventRaised = false, raisedForLabel = false;

            HelperViewModel viewModel = new HelperViewModel();
            viewModel.PropertyChanged += (s, e) =>
            {
                eventRaised = true;
                if (e.PropertyName == nameof(viewModel.Label))
                {
                    raisedForLabel = true;
                }
                waitHandle.Set();
            };

            viewModel.SetLabel(labelValue);

            waitHandle.WaitOne(2000);

            Assert.AreEqual(labelValue, viewModel.Label, "Label value not set.");
            Assert.IsTrue(eventRaised, "Event not raised.");
            Assert.IsTrue(raisedForLabel, "Event raised, but not for label.");
        }
    }
}

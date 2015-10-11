using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using ViewModels.Tests.Helpers;
using System.Linq;
using System.Collections;

using ZimCode.ViewModels;
using System.Collections.ObjectModel;

namespace ViewModels.Tests
{
    [TestClass]
    public class Test_LabeledViewModelCollection
    {
        [TestMethod]
        public void TestLabelAndNotifyPropertyChanged()
        {
            const string labelValue = "TestLabelValue";

            ManualResetEvent waitHandle = new ManualResetEvent(false);
            bool eventRaised = false, raisedForLabel = false;

            HelperLabeledViewModelCollection viewModel = new HelperLabeledViewModelCollection(null, Enumerable.Empty<object>(), o => o);
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

        [TestMethod]
        public void TestCollectionSync()
        {
            string item0 = "Item0";
            string item1 = "Item1";
            string item2 = "Item2";
            string item3 = "Item3";

            ObservableCollection<string> collection = new ObservableCollection<string>();

            HelperLabeledViewModelCollection viewModel = new HelperLabeledViewModelCollection(null, collection, o => o);

            collection.Add(item0);
            collection.Add(item1);
            collection.Add(item3);
            Assert.IsTrue(CompareCollectionValues(collection, viewModel), "Add did not work.");

            collection.Insert(2, item2);
            Assert.IsTrue(CompareCollectionValues(collection, viewModel), "Insert did not work.");

            collection.Remove(item3);
            Assert.IsTrue(CompareCollectionValues(collection, viewModel), "Remove did not work.");

            collection.Move(0, 1);
            Assert.IsTrue(CompareCollectionValues(collection, viewModel), "Move did not work.");

            collection.Clear();
            Assert.IsTrue(CompareCollectionValues(collection, viewModel), "Clear did not work.");
        }

        private bool CompareCollectionValues(IEnumerable collection1, IEnumerable collection2)
        {
            int count1 = collection1.Count();
            if (count1 != collection2.Count()) return false;

            var list1 = collection1.ToList();
            var list2 = collection2.ToList();

            for (int i = 0; i < count1; i++)
                if (list1[i] != list2[i]) return false;

            return true;
        }
    }
}

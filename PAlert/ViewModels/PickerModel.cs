using System;
using System.Collections.Generic;
using UIKit;

namespace PAlert.ViewModels
{
    /// <summary>
    /// Model for the PredPickView
    /// </summary>
    public class PredModel : UIPickerViewModel
    {

        public List<string> names = new List<string>();
        public List<Item> items = new List<Item>();
        //public event EventHandler ItemSelected;
        public EventHandler ValueChanged;
        public EventHandler pressOccured;

        private void getData()
        {
            MockDataStore mockData = new MockDataStore();
            foreach(var m in mockData.items)
            {
                names.Add(m.Name);
                items.Add(m);
            }
        }
        private nint selectedIndex = 0;

        public string SelectedName
        {
            get
            {
                return names[(int)selectedIndex];
            }
        }

        public Item SelectedItem
        {
            get
            {
                return items[(int)selectedIndex];
            }
        }
        private UILabel predLabel;

        public PredModel(UILabel predaLabel)
        {
            getData();
            predLabel = predaLabel;
        }

        public override nint GetComponentCount(UIPickerView pickerView)
        {
            return 1;
        }

        public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
        {
            return names.Count;
        }

        public override string GetTitle(UIPickerView pickerView, nint row, nint component)
        {

            if (component == 0)
                return names.ToArray()[row];
            else
                return row.ToString();
        }

        public override void Selected(UIPickerView pickerView, nint row, nint component)
        {
            selectedIndex = row;
            predLabel.Text = names.ToArray()[row];
            //ValueChanged(this, new EventArgs());
        }
        public override nfloat GetComponentWidth(UIPickerView pickerView, nint component)
        {
            if (component == 0)
                return 240f;
            else
                return 40f;
        }

        public override nfloat GetRowHeight(UIPickerView pickerView, nint component)
        {
            return 40f;
        }
    }
}
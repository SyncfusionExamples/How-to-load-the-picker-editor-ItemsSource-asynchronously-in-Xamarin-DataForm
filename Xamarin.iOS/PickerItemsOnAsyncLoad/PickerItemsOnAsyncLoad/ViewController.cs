using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Syncfusion.iOS.DataForm;
using UIKit;

namespace PickerItemsOnAsyncLoad
{
    public partial class ViewController : UIViewController
    {
        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
        SfDataForm dataForm;
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            dataForm = new SfDataForm();
            dataForm.DataObject = new CityInfo();
            dataForm.SourceProvider = new SourceProviderExt();
            dataForm.RegisterEditor("CityName", "Picker");
            dataForm.BackgroundColor = UIColor.White;
            dataForm.AutoGeneratingDataFormItem += DataForm_AutoGeneratingDataFormItem;
            this.View = dataForm;
        }
        private void DataForm_AutoGeneratingDataFormItem(object sender, AutoGeneratingDataFormItemEventArgs e)
        {
            if (e.DataFormItem != null && e.DataFormItem.Name == "CityName")
            {
                    (e.DataFormItem as DataFormPickerItem).DisplayMemberPath = "City";
                    (e.DataFormItem as DataFormPickerItem).ValueMemberPath = "PostalCode";
            }
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
    public class SourceProviderExt : SourceProvider
    {
        List<Address> details;
        public override IList GetSource(string sourceName)
        {
            if (sourceName == "CityName")
            {
                GetSources(sourceName);
                return details;
            }
            return new List<string>();
        }

        public async Task<IList> GetSources(string sourceName)
        {
            details = new List<Address>();

            details.Add(new Address() { City = "Tokyo", PostalCode = 1 });
            details.Add(new Address() { City = "Mexico", PostalCode = 2 });
            details.Add(new Address() { City = "Shanghai", PostalCode = 3 });
            details.Add(new Address() { City = "London", PostalCode = 4 });
            details.Add(new Address() { City = "Paris", PostalCode = 5 });

            await Task.Delay(1000);
            return details;
            //  return await Task.Run(()=> details);
        }
    }

    public class CityInfo
    {
        public string name { get; set; }
        [Display(Name = "City Name")]
        public string CityName { get; set; }

    }

    public class Address
    {
        public int PostalCode { get; set; }
        public string City { get; set; }
    }
   
}

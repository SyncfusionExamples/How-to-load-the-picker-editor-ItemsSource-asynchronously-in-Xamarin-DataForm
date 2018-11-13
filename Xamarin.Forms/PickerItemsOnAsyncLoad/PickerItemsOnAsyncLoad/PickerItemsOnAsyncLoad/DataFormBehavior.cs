using Syncfusion.XForms.DataForm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PickerItemsOnAsyncLoad
{
    public class DataFormBehavior : Behavior<ContentPage>
    {

        SfDataForm dataForm = null;
        protected override void OnAttachedTo(ContentPage bindable)
        {
            base.OnAttachedTo(bindable);
            dataForm = bindable.FindByName<SfDataForm>("dataForm");
            dataForm.DataObject = new CityInfo();
            dataForm.SourceProvider = new SourceProviderExt();
            if (Device.RuntimePlatform == Device.UWP)
                dataForm.RegisterEditor("CityName", "DropDown");
            else
                dataForm.RegisterEditor("CityName", "Picker");

            dataForm.AutoGeneratingDataFormItem += DataForm_AutoGeneratingDataFormItem;

        }
        private void DataForm_AutoGeneratingDataFormItem(object sender, AutoGeneratingDataFormItemEventArgs e)
        {
            if (e.DataFormItem != null && e.DataFormItem.Name == "CityName")
            {
                if (Device.RuntimePlatform != Device.UWP)
                {
                    (e.DataFormItem as DataFormPickerItem).DisplayMemberPath = "City";
                    (e.DataFormItem as DataFormPickerItem).ValueMemberPath = "PostalCode";
                }
            }
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
        [Display(Name = "City Name")]
        public string CityName { get; set; }
    }

    public class Address
    {
        public int PostalCode { get; set; }
        public string City { get; set; }
    }
}

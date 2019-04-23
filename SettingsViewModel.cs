using Neolant.SPF.Model.Core.Setting;
using Neolant.SPF.NewUI.Resources.Localization.Messages;
using Neolant.SPF.NewUI.ViewModels.Sites.Settings;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;

namespace Neolant.SPF.NewUI.ViewModels.Sites.Components
{
    public class SettingsViewModel : BaseViewModel
    {
        public IOptionHolder Setting { private set; get; }
        private System.Action Save;

        public ObservableCollection<Property> Properties { get; set; }

        public SettingsViewModel(IOptionHolder selectedSetting, System.Action saveMethod)
        {
            Setting = selectedSetting;
            Save = saveMethod;

            Properties = GetProperties();
        }

        private ObservableCollection<Property> GetProperties()
        {
            ObservableCollection<Property> properties = new ObservableCollection<Property>();

            if (Setting != null)
            {
                PropertyInfo[] props = Setting.GetType().GetProperties();

                foreach (PropertyInfo pr in props)
                {
                    BrowsableAttribute browsable = (BrowsableAttribute)Attribute.GetCustomAttribute(pr, typeof(BrowsableAttribute));
                    if (browsable == null)
                    {
                        properties.Add(new Property(Setting, pr));
                    }
                }
            }

            return properties;
        }

        public void SaveSettings()
        {
            try
            {
                Save?.Invoke();
            }
            catch (Exception e)
            {
                ShowErrorMessageBox(e.Message, Messages.SaveFailed);
            }
        }
    }
}

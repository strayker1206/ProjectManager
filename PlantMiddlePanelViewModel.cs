using Neolant.SPF.Model.Core.Setting;
using Neolant.SPF.Model.Plant.Layer;
using Neolant.SPF.Model.Site.Layer;
using Neolant.SPF.NewUI.Resources.Localization.Messages;
using NLog;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Neolant.SPF.NewUI.ViewModels.Sites.Components
{
    public class PlantMiddlePanelViewModel : BaseViewModel
    {
        private static readonly Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public IPlant Plant { private set; get; }
        public ISite Site => Plant.GetMyselfOrParent<ISite>();
        public ObservableCollection<IOptionHolder> Settings { get; private set; }        

        private IOptionHolder _selectedSetting;
        public IOptionHolder SelectedSetting
        {
            get { return _selectedSetting; }
            private set
            {
                _selectedSetting = value;

                if (_selectedSetting != null)
                {
                    ActivateSettingView();
                }
            }
        }

        public PlantMiddlePanelViewModel(IPlant plant, ObservableCollection<IOptionHolder> settings)
        {
            Plant = plant;
            Settings = settings;
            SelectedSetting = Settings.FirstOrDefault();
        }

        private void ActivateSettingView()
        {
            ActivateItem(new SettingsViewModel(_selectedSetting, Plant.Save));
        }

        public bool CanRegister => Site.State.CanOperateSPF.CheckState();
        public void Register()
        {
            try
            {
                if (Plant.State.HasRegistration.CheckState())
                {
                    if (ShowQuestionMessageBox(SiteMessages.CancelRegistation, SiteMessages.RegisterTitle))
                    {
                        Plant.Services.Adapter.UnRegistrer();
                        ShowInfoMessageBox(SiteMessages.RegisterAbortSuccess, SiteMessages.RegisterTitle);
                    }
                }
                else
                {
                    Plant.Services.Adapter.Register();
                    ShowInfoMessageBox(SiteMessages.RegisterSuccess, SiteMessages.RegisterTitle);
                }
                NotifyOfPropertyChange(nameof(CanRegister));
            }
            catch (Exception e)
            {
                logger.Error(e, SiteMessages.RegistrationError);
                ShowErrorMessageBox(e.Message, SiteMessages.RegistrationError);
            }
        }

        public bool CanRetrievePbs => Plant.State.CanRetrieve.CheckState();
        public void RetrievePbs()
        {
            try
            {
                Plant.Services.Adapter.RetrievePBS();
                NotifyOfPropertyChange(nameof(CanRetrievePbs));
                ShowInfoMessageBox(SiteMessages.OperationCompleted, SiteMessages.RetrievePBS);
            }
            catch (Exception e)
            {
                logger.Error(e, SiteMessages.PBSRetrieveError);
                ShowErrorMessageBox(e.Message, SiteMessages.PBSRetrieveError);
            }
        }

        public bool CanGetPbsSignature => Site.State.CanOperateSPF.CheckState();
        public void GetPbsSignature()
        {
            try
            {
                Plant.Services.Adapter.GetPBSSignature();
                NotifyOfPropertyChange(nameof(CanGetPbsSignature));
                ShowInfoMessageBox(SiteMessages.OperationCompleted, SiteMessages.GettingPBSSignature);
            }
            catch (Exception e)
            {
                logger.Error(e, SiteMessages.ErrorGettingPBSSignature);
                ShowErrorMessageBox(e.Message, SiteMessages.ErrorGettingPBSSignature);
            }
        }
    }
}

using Neolant.SPF.Model.Core.Setting;
using Neolant.SPF.Model.Site.Layer;
using Neolant.SPF.NewUI.Resources.Localization.Messages;
using Neolant.SPF.NewUI.ViewModels._ProcessesAbstract;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Neolant.SPF.NewUI.ViewModels.Sites.Components
{
    public class SiteMiddlePanelViewModel : BaseViewModel
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public ISite Site { private set; get; }
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

        public SiteMiddlePanelViewModel(ISite site, ObservableCollection<IOptionHolder> settings)
        {
            Site = site;
            Settings = settings;
            SelectedSetting = Settings.FirstOrDefault();
        }

        private void ActivateSettingView()
        {
            ActivateItem(new SettingsViewModel(_selectedSetting, Site.Save));
        }

        public bool CanGetCMF => Site.State.CanOperateSPF.CheckState();
        public async void GetCMF()
        {
            try
            {
                LongProcessViewModel screen = new LongProcessViewModel(Site.Services.ProcessService.GetCMF);
                ActivateItem(screen);
                if (!screen.InProgress)
                {
                    await screen.Run();
                }
                if (!screen.ProcessContainsError)
                {
                    ShowInfoMessageBox(SiteMessages.OperationCompleted, SiteMessages.GettingCMF);
                }
            }
            catch (Exception e)
            {
                logger.Error(e, SiteMessages.ErrorGettingCMF);
                ShowErrorMessageBox(e.Message, SiteMessages.ErrorGettingCMF);
            }
        }
    }
}

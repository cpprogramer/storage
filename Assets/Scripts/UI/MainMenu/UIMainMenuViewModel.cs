using System;
using System.Threading;
using Common.Profile;
using Common.UI;
using Common.UI.Model;
using Configs;
using StorageTest.Messages;
using StorageTest.UI.View;
using UniRx;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

namespace StorageTest.UI.ViewModel
{
    public sealed class UIMainMenuViewModel : UIBaseViewModel<UIMainMenuView>
    {
        private readonly IConsumablesConfig _consumablesConfig;
        private readonly IUserProfile _userProfile;
      
        private UIMainMenuDTO _dto;

        public UIMainMenuViewModel(IUiRootAggregator uiRootAggregator, IUserProfile userProfile)
            : base(uiRootAggregator)
        {
            _userProfile = userProfile ?? throw new ArgumentNullException(nameof(userProfile));
            _consumablesConfig = Resources.Load<ConsumablesConfig>("Configs/ConsumablesConfig");
        }

        protected override void OnInitialize(BaseWindowDTO dto)
        {
            _dto = (UIMainMenuDTO)dto;
            _baseView.OnPlayClicked += PlayClickedHandler;
        }

        protected override void OnClosing()
        {
        }

        private void PlayClickedHandler()
        {
           AddRequest(new GotoHangarMessage());
        }
    }
}
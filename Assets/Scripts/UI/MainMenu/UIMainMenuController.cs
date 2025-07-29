using Common.Messages;
using Common.Profile;
using Common.UI;
using Common.UI.Model;
using Configs;
using MonopolySpace.Messages;
using MonopolySpace.UI.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UniRx;
using UnityEngine;

namespace MonopolySpace.UI.Controllers
{
    public sealed class UIMainMenuController : Window< UIMainMenuView >
    {
        private readonly IUserProfile _userProfile;
        private readonly IConsumablesConfig _consumablesConfig;
        
        private UIMainMenuModel _model;
        private CompositeDisposable _compositeDisposable = new();
        private CancellationTokenSource _cancellationTokenSource;
        
        
        public UIMainMenuController(IUiRootAggregator uiRootAggregator, IUserProfile userProfile  )
            : base( uiRootAggregator )
        {
            _userProfile = userProfile ?? throw new ArgumentNullException(nameof(userProfile));
            _consumablesConfig = Resources.Load< ConsumablesConfig >("Configs/ConsumablesConfig");
        }

        protected override void OnInitialize( BaseWindowModel model )
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _compositeDisposable = new CompositeDisposable();
            _model = (UIMainMenuModel)model;
            _baseView.OnPlayClicked += PlayClickedHandler;
            _baseView.OnGoldClicked += GoldClickHandler;
            
            _compositeDisposable = new CompositeDisposable
            {
                _uiRootAggregator.MessageBroker.Receive<UpdateConsumablesMessage>().Subscribe(UpdateConsumablesHandler)
            };

            var current = _userProfile.UserInventory.Consumables.Select( item => ( item.uid, item.count, 0 ) );
            UpdateConsumablesView(current);
        }

        //TEST
        private void GoldClickHandler( )
        {
            //Получить
            var gold = _userProfile.UserInventory.GetConsumableCount( _consumablesConfig.GoldUid );
            //Обновить
            _userProfile.UserInventory.UpdateConsumables( _consumablesConfig.GoldUid, gold + 1, gold );
        }
        //TEST

        private void UpdateConsumablesHandler(UpdateConsumablesMessage message)
        {
            UpdateConsumablesView(message.ConsumableData);
        }

        private void UpdateConsumablesView(IEnumerable<(string key, int value, int prevValue)> consumables)
        {
            _baseView.UpdateConsumables(consumables);
        }
        
        protected override void OnClosing()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = null;
            _compositeDisposable.Dispose();
        }

        private void PlayClickedHandler() => _uiRootAggregator.MessageBroker.Publish( new UIConnectToLobbyMessage() );
    }
}
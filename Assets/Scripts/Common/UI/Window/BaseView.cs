using Assets.Scripts.Timeline;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Common
{
    public abstract class BaseView : MonoBehaviour, IBaseView
    {
        public event Action OnClose;

        public event Action OnStarted;
        [ SerializeField ] private Button _buttonClose;
        
        protected ITimeline _timeline;
        
        public virtual WindowLayer WindowLayer => WindowLayer.Windows;

        public virtual bool IsRootView => true;

        public virtual bool IsDestroyable => true;

        public virtual void Hide() => gameObject.SetActive( false );

        public virtual void Show() => gameObject.SetActive( true );

        protected virtual void Awake()
        {
            if ( _buttonClose != null )
                _buttonClose.onClick.AddListener( Close );
        }

        protected virtual void Start() => OnStarted?.Invoke();

        protected void Close() => OnClose?.Invoke();

        public void CloseView()
        {
            if ( IsDestroyable )
                Utils.Destroy( gameObject );
            else
                gameObject.SetActive( false );
        }

        public void Setup( ITimeline timeline )
        {
            _timeline = timeline;
        }
        public void ShowCloseButton( bool isShow )
        {
            if ( _buttonClose != null ) _buttonClose.gameObject.SetActive( isShow );
        }
    }
}
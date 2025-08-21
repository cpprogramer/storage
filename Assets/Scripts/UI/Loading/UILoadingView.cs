using Common;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace StorageTest.UI.View
{
    public sealed class UILoadingView : BaseView
    {
        [ SerializeField ] private Image[] _points;
        [ SerializeField ] private Color _normal;
        [ SerializeField ] private Color _active;
        [ SerializeField ] private float _duration;

        private Sequence _sequence;

        private void OnDestroy() => _sequence.Kill();

        public void StartAnimation()
        {
            _sequence = DOTween.Sequence();
            _sequence.Append( _points[ 0 ].DOColor( _active, _duration ) );
            _sequence.AppendInterval( 0.2F );
            _sequence.Append( _points[ 0 ].DOColor( _normal, _duration ) );
            _sequence.Append( _points[ 1 ].DOColor( _active, _duration ) );
            _sequence.AppendInterval( 0.2F );
            _sequence.Append( _points[ 1 ].DOColor( _normal, _duration ) );
            _sequence.Append( _points[ 2 ].DOColor( _active, _duration ) );
            _sequence.AppendInterval( 0.2F );
            _sequence.Append( _points[ 2 ].DOColor( _normal, _duration ) );
            _sequence.SetLoops( -1 );
        }
    }
}
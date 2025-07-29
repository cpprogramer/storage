using Assets.Scripts.Timeline;
using Common;
using DG.Tweening;
using TMPro;
using UnityEngine;
using View.Timeline;

namespace JigsawPuzzles.UI.View
{
    public sealed class ConsumableCountView : BaseConsumableView
    {
        #region Fields

        [SerializeField] private TextMeshProUGUI _textCount;

        [SerializeField] private string _format;

        #endregion
        
        //TEST
        [ SerializeField ] private GameObject _coinsTemplate;
        //Config
        [ SerializeField ] private float _moveDuration  = 0.2f;
        [ SerializeField ] private float _scaleDuration = 0.2f;
        [ SerializeField ] private float _waitDuration  = 0.2f;
        //TEST
        
        #region Methods

        public override void UpdateConsumable(string uid, int count, ITimeline timeline)
        {
            var text = string.Format(_format, count);
            if (text != _textCount.text)
            {
                var inst =  Object.Instantiate( _coinsTemplate, _coinsTemplate.transform.parent,true );
                timeline.Add( () =>
                {
                    inst.SetActive( true );
                    inst.transform.DOScale( Vector3.one, _scaleDuration );
                });
                
                timeline.AddDelay( _waitDuration );
                
                timeline.Add( () =>
                {
                    inst.transform.DOLocalMove( Vector3.zero, _moveDuration );
                });
                
                timeline.AddDelay( _moveDuration );
                
                timeline.Add(() =>
                {
                    Utils.Destroy( inst );
                    _textCount.text = text;
                    _textCount.transform.localScale = Vector3.one;
                    _textCount.transform.DOScale(Vector3.one * 1.2f, 0.25f).OnComplete(() =>
                    {
                        _textCount.text = text;
                        _textCount.transform.localScale = Vector3.one;
                    });
                });
            }
        }
        
        #endregion
    }
}
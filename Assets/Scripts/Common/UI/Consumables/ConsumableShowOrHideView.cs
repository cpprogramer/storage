using Assets.Scripts.Timeline;
using UnityEngine;
using View.Timeline;

namespace JigsawPuzzles.UI.View
{
    public sealed class ConsumableShowOrHideView : BaseConsumableView
    {
        #region Fields

        [Header("visible if count = 0")]
        [SerializeField] private Transform[] _visibleifCountZero;
        [SerializeField] private Transform[] _hiddenIfCountZero;
        
        
        [Header("hidden if count != 0")]
        [SerializeField] private Transform[] _visoibleIfCountNotZero;
        [SerializeField] private Transform[] _hiddenIfCountNotZero;
        #endregion

        #region Methods

        public override void UpdateConsumable(string uid, int count, ITimeline timeline)
        {
            timeline.Add(() =>
            {
                _visibleifCountZero.ForEach(item =>
                {
                    if(item != null)
                        item.gameObject.SetActive(count == 0);
                });
                
                _hiddenIfCountZero.ForEach(item =>
                {
                    if(item != null)
                        item.gameObject.SetActive(count != 0);
                });
                
                
                _visoibleIfCountNotZero.ForEach(item =>
                {
                    if(item != null)
                        item.gameObject.SetActive(count != 0);
                });
                
                _hiddenIfCountNotZero.ForEach(item =>
                {
                    if(item != null)
                        item.gameObject.SetActive(count == 0);
                });

            });
        }

        #endregion
    }
}
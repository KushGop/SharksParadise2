﻿#if GLEY_PLAYMAKER_SUPPORT
namespace HutongGames.PlayMaker.Actions
{
    [HelpUrl("https://gley.gitbook.io/mobile-ads/")]
    [ActionCategory(ActionCategory.ScriptControl)]
    [Tooltip("Displays a rewarded interstitial")]
    public class ShowRewardedInterstitial : FsmStateAction
    {
        [Tooltip("Where to send the event.")]
        public FsmEventTarget eventTarget;

        [UIHint(UIHint.FsmEvent)]
        [Tooltip("Event sent when a rewarded interstitial was fully seen")]
        public FsmEvent videoComplete;

        [UIHint(UIHint.FsmEvent)]
        [Tooltip("Event sent when a rewarded interstitial was skipped")]
        public FsmEvent videoSkipped;


        public override void Reset()
        {
            base.Reset();
            videoComplete = null;
            videoSkipped = null;
            eventTarget = FsmEventTarget.Self;
        }

        public override void OnEnter()
        {
            if (Gley.MobileAds.API.IsRewardedInterstitialAvailable())
            {
                Gley.MobileAds.API.ShowRewardedInterstitial(VideoComplete);
            }
            else
            {
                Finish();
            }
        }

        private void VideoComplete(bool complete)
        {
            if(complete)
            {
                Fsm.Event(eventTarget, videoComplete);
            }
            else
            {
                Fsm.Event(eventTarget, videoSkipped);
            }
            Finish();
        }
    }
}
#endif

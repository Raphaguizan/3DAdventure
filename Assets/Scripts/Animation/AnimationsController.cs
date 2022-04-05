using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Animations
{
    public enum AnimationType
    {
        NONE,
        IDLE,
        RUN,
        ATTACK,
        DIE
    }
    public class AnimationsController : MonoBehaviour
    {
        public Animator anim;
        public List<AnimationSetup> animationsList;
        
        public void Play(AnimationType type)
        {
            var trigger = animationsList.Find(x => x.type == type);
            if(trigger != null)
                anim.SetTrigger(trigger.trigger);
        }
    }

    [Serializable]
    public class AnimationSetup
    {
        public AnimationType type;
        public string trigger;
    }
}

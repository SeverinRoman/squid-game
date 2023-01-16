//#region import
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//#endregion


public class Animations
{
    public class Character
    {
        public readonly int isClimb = Animator.StringToHash("is_climb");
        public readonly int climbStop = Animator.StringToHash("climb_stop");
        public readonly int jump = Animator.StringToHash("jump");
    }


    public Character character = new Character();

}

//#region import
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//#endregion


public class Animations
{
    public class Character
    {
        public readonly int isClimb = Animator.StringToHash("isClimb");
    }


    public Character character = new Character();

}

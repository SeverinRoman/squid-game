//#region import
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//#endregion


public class Animations
{
    public class Character
    {
        public readonly int speed = Animator.StringToHash("speed");
        public readonly int isHaveItem = Animator.StringToHash("isHaveItem");
    }


    public Character character = new Character();

}

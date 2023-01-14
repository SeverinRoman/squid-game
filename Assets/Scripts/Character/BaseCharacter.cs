//#region import
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
//#endregion

namespace Gameplay
{
    public enum CharacterState
    {
        Idle = 0,
        Climb = 1,
        Death = 3,
    }

    public class BaseCharacter : MonoBehaviour
    {
        //#region editors fields and properties
        [SerializeField] private float healt = 100;
        [SerializeField] private GameObject projectile = null;

        [SerializeField] private List<BaseCharacter> enemies = new List<BaseCharacter>();

        //#endregion
        //#region public fields and properties

        public CharacterState State
        {
            get
            {
                return state;
            }
            set
            {
                if (state == value)
                {
                    return;
                }

                ChangeState(value);
                state = value;
            }
        }

        //#endregion
        //#region private fields and properties

        private CharacterState state;

        private Tween tweenReloadAttack;



        //#endregion


        //#region life-cycle callbacks

        private void Start()
        {
            State = CharacterState.Idle;
        }


        void Update()
        {
            // 
        }

        //#endregion

        //#region public methods
        public virtual void ChangeState(CharacterState state)
        {
            if (tweenReloadAttack != null)
            {
                tweenReloadAttack.Kill();
            }

            switch (state)
            {
                case CharacterState.Death:
                    {
                        Death();
                    }
                    break;
            }
        }


        public virtual void Death()
        {
            // Destroy(gameObject);
        }



        //#endregion

        //#region private methods

        //#endregion

        //#region event handlers
        //#endregion
    }
}

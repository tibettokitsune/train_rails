using GoodCat.Fsm;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Scripts.RailsBuildingSystem
{
    public class BuilderEnabledState : State
    {
        private BuilderConfiguration _configuration;
        private ValidationScreen _validationScreen;
        private FSM _fsm;
        private bool _isValidate = false;
        public BuilderEnabledState(BuilderConfiguration configuration, ValidationScreen validationScreen)
        {
            _configuration = configuration;
            _validationScreen = validationScreen;
        }

        protected override void OnEnable()
        {
            _fsm = new FSM();

            var idleState = new StateSimple("NotPressedState", () => { }, () => { }, () => { });
            var pressingState = new PressingState(_configuration);
            var validationState = new StateSimple("ValidationState", () =>
            {
                _validationScreen.Show();
                _isValidate = false;
                _validationScreen.OnClose.Take(1).Subscribe(v =>
                {
                    _isValidate = true;
                    if (v)
                    {
                        pressingState.Apply();
                    }
                    else
                    {
                        pressingState.Discard();
                    }
                });
            }, () => { }, () => { });

            _fsm.StatesCollection.Add(idleState)
                .Add(pressingState)
                .Add(validationState);

            _fsm.StatesCollection.Transitions.From(idleState).To(pressingState).Set(() => Input.GetMouseButton(0) 
                && !EventSystem.current.IsPointerOverGameObject());
            _fsm.StatesCollection.Transitions.From(pressingState).To(validationState).Set(() => !Input.GetMouseButton(0));
            _fsm.StatesCollection.Transitions.From(validationState).To(idleState).Set(() => _isValidate);

            _fsm.StatesCollection.SetStartState(idleState);
            
            _fsm.Initialize();
        }

        protected override bool OnUpdate()
        {
            _fsm.Update();
            return false;
        }

        protected override void OnDisable()
        {
        }
    }
}
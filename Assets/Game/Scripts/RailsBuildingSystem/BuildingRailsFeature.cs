using GoodCat.Fsm;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts.RailsBuildingSystem
{
    public class BuildingRailsFeature : ITickable, IInitializable
    {
        [Inject] private Toggle toggle;
        [Inject] private BuilderConfiguration _configuration;
        [Inject] private ValidationScreen validationScreen;
        private FSM _fsm;

        private GameObject _buildingPlane;
        public void Tick()
        {
            _fsm?.Update();
        }

        public void Initialize()
        {
            _fsm = new FSM();
            var simpleState = new StateSimple("WaitingForPress", () =>
            {
                _buildingPlane.gameObject.SetActive(false);
            }, () => { }, () =>
            {
                _buildingPlane.gameObject.SetActive(true);
            });
            var pressedState = new BuilderEnabledState(_configuration, validationScreen);

            _fsm.StatesCollection.Add(simpleState).Add(pressedState);

            _fsm.StatesCollection.SetStartState(simpleState);
            _fsm.StatesCollection.Transitions.From(simpleState).To(pressedState).Set(() => toggle.isOn);
            _fsm.StatesCollection.Transitions.From(pressedState).To(simpleState).Set(() => !toggle.isOn);
            
            _fsm.Initialize();

            _buildingPlane = GameObject.Find("BuildingPlane");
        }
    }
}
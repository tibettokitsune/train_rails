using System.Collections.Generic;
using Dreamteck.Splines;
using GoodCat.Fsm;
using UnityEngine;

namespace Game.Scripts.RailsBuildingSystem
{
    public class PressingState : State
    {
        private Vector2 startPosition;
        private Vector2 endPosition;
        private readonly BuilderConfiguration _configuration;
        private SplineComputer computer;
        private readonly List<GameObject> _tempItems;
        public PressingState(BuilderConfiguration configuration)
        {
            _configuration = configuration;
            _tempItems = new List<GameObject>();
        }
        protected override void OnEnable()
        {
            CreateStartPositionCell();
            var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.transform.position = GetPlaneIntersection();
            _tempItems.Add(go);
        }

        protected override bool OnUpdate()
        {
            // TryBuildRailroad();
            
            return false;
        }

        private void TryBuildRailroad()
        {
            // computer.SetPoints(new SplinePoint[]
            // {
            //     new SplinePoint(startPosition), new SplinePoint(endPosition)
            // });
        }

        protected override void OnDisable()
        {
            // GameObject.Destroy(computer);
            var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.transform.position = GetPlaneIntersection();
            _tempItems.Add(go);
        }
        
        private void CreateStartPositionCell()
        {
            computer = GameObject.Instantiate(_configuration.splineComputer);
        }
        
        Vector3 GetPlaneIntersection()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float delta = ray.origin.y;
            Vector3 dirNorm = ray.direction / ray.direction.y;
            Vector3 IntersectionPos = ray.origin - dirNorm * delta;
            return IntersectionPos;
        }

        public void Discard()
        {
            while (_tempItems.Count > 0)
            {
                GameObject.Destroy(_tempItems[^1]);
                _tempItems.RemoveAt(_tempItems.Count - 1);
            }
        }

        public void Apply()
        {
            
        }
    }
}
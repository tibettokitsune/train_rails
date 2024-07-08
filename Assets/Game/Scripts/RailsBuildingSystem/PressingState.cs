using System.Collections.Generic;
using System.Linq;
using CodeMonkey.Utils;
using Dreamteck.Splines;
using GoodCat.Fsm;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Game.Scripts.RailsBuildingSystem
{
    public class PressingState : State
    {
        private Vector3 startPosition;
        private Vector3 endPosition;
        private readonly BuilderConfiguration _configuration;
        private SplineComputer computer;
        private SplinePoint[] wayPoints;
        private Pathfinding pathfinding;


        public PressingState(BuilderConfiguration configuration)
        {
            _configuration = configuration;
            pathfinding = new Pathfinding(20, 20);
        }

        protected override void OnEnable()
        {
            startPosition = GetPlaneIntersection();
        }

        protected override bool OnUpdate()
        {
            endPosition = GetPlaneIntersection();

            if (!startPosition.Equals(endPosition) && computer == null)
            {
                computer = Object.Instantiate(_configuration.splineComputer);
            }
            TryBuildRailroad();
            return false;
        }

        private void TryBuildRailroad()
        {
            List<PathNode> path = pathfinding.FindPath((int) startPosition.x + 10, (int) startPosition.z + 10,
                (int) endPosition.x + 10, (int) endPosition.z + 10);
            wayPoints = path.Select(x =>
            {
                var p = new SplinePoint(new Vector3(x.x - 10, 0, x.y - 10));
                p.size = 0.3f;
                return p;
            }).ToArray();
            computer.SetPoints(wayPoints);
        }


        protected override void OnDisable()
        {
            // Object.Destroy(computer.gameObject);
        }
        

        Vector3 GetPlaneIntersection()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float delta = ray.origin.y;
            Vector3 dirNorm = ray.direction / ray.direction.y;
            Vector3 IntersectionPos = ray.origin - dirNorm * delta;
            IntersectionPos.x = Mathf.RoundToInt(IntersectionPos.x);
            IntersectionPos.z = Mathf.RoundToInt(IntersectionPos.z);

            return IntersectionPos;
        }

        public void Discard()
        {
            Object.Destroy(computer.gameObject);
        }

        public void Apply()
        {
        }
    }
}
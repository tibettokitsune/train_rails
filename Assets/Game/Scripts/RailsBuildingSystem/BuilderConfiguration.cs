using Dreamteck.Splines;
using UnityEngine;

namespace Game.Scripts.RailsBuildingSystem
{
    [CreateAssetMenu(menuName = "Configs/Builder")]
    public class BuilderConfiguration : ScriptableObject
    {
        public SplineComputer splineComputer;
    }
}
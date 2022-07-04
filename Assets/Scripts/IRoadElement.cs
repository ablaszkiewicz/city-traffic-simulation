using System.Collections.Generic;
using PathCreation;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class IRoadElement : MonoBehaviour
    {
        [SerializeField]
        protected List<IRoadElement> nextRoadElements = new List<IRoadElement>();
        
        [SerializeField]
        protected List<IRoadElement> previousRoadElements = new List<IRoadElement>();
        
        public abstract List<IRoadElement> GetNextRoadElements(IRoadElement previous);
        public abstract List<PathCreator> GetPathCreators();
        public abstract PathCreator GetPathCreatorToGetThrough(IRoadElement previous, IRoadElement next);

        public abstract List<RoadElementsPathCreatorTuple> GetMap();
    }
    
}
﻿using System.Collections.Generic;
using PathCreation;

namespace Assets.Scripts
{
    public interface IRoadElement
    {
        public List<IRoadElement> GetNextRoadElements();
        public List<PathCreator> GetPathCreators();
        public RoadElementType GetRoadElementType();
        
    }

    public enum RoadElementType
    {
        ROAD,
        CROSSROAD,
    }
}
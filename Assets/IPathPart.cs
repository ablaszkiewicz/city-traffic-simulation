using System;
using PathCreation;

public interface IPathPart
{
    public VertexPath GetPath(IPathPart nextPart = null);
}

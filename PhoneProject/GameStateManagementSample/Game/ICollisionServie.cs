using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameStateManagement
{
    public interface ICollisionService
    {
        void RegisterObject(GameObject go);
    }
}
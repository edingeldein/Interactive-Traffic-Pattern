using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
    public static class ListExtensions
    {
        public static GameObject GetByName(this List<GameObject> list, string objName)
        {
            foreach(var go in list)
            {
                if (go.name.Equals(objName))
                    return go;
            }

            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using _Scripts.Inventory_System;
using UnityEngine;

namespace _Scripts
{
    public class ContainerManager : MonoBehaviour
    {

        [Serializable]
        public struct ContainerCollection
        {
            public enum ContainerType
            {
                Fridge,
                Storage
            }

            public ContainerType type;
            public ContainerUI containerUI;
        }
        
        public List<ContainerCollection> containerCollections;

        public ContainerUI GetContainerByType(ContainerCollection.ContainerType type)
        {
            foreach (var collection in containerCollections)
            {
                if (collection.type == type)
                {
                    return collection.containerUI;
                }
            }

            return null;
        }
    }
}
using System;
using System.Collections.Generic;
using _Scripts.InventorySystem.UI;
using UnityEngine;

namespace _Scripts.ManagerCollection
{
    public class StorageManager : MonoBehaviour
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
            public StorageUI containerUI;
        }
        
        public List<ContainerCollection> containerCollections;

        public StorageUI GetContainerByType(ContainerCollection.ContainerType type)
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
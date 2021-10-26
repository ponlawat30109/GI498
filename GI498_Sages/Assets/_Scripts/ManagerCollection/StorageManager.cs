using System;
using System.Collections.Generic;
using _Scripts.InventorySystem;
using _Scripts.InventorySystem.ScriptableObjects.Storage;
using _Scripts.InventorySystem.UI;
using UnityEngine;

namespace _Scripts.ManagerCollection
{
    public class StorageManager : MonoBehaviour
    {

        [Serializable]
        public struct StorageCollection
        {
            public StorageObject.StorageTypeEnum type;
            public Storage storage;
        }
        
        public List<StorageCollection> storageCollections;

        public List<MiniStorage> miniStorageCollections;

        
        ////////////////////////////////////////////////////////////////////////////////////////////

        private void Start()
        {
            
        }

        /////////////////////////////////////////////////////////////////////////////////////////////

        public Storage GetStorageByType(StorageObject.StorageTypeEnum type)
        {
            var obj = gameObject.GetComponent<Storage>();
            
            foreach (var storage in storageCollections)
            {
                if (storage.type == type)
                {
                    obj = storage.storage;
                }
            }

            return obj;
        }
        
        private void OnApplicationQuit()
        {
            
        }
    }
}
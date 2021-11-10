using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Helper
{
    public class BundleHelper
    {
        private static AssetBundle[] bundles;

        static public AssetBundle[] GetBundles()
        {
            return Resources.FindObjectsOfTypeAll<AssetBundle>();
        }

        static public AssetBundle GetBundle(string name)
        {
            AssetBundle[] bundles = GetBundles();

            for(int i = 0 ; i < bundles.Length ; ++i)
            {
                Debug.Log(bundles[i].name);
                if(bundles[i].name == name)
                    return bundles[i];
            }

            return AssetBundle.LoadFromFile(Path.Combine(Path.Combine(Application.dataPath, "bundles"), name));
        }

        static public GameObject GetPrefab(AssetBundle bundle, string name)
        {
            return bundle.LoadAsset<GameObject>(name);
        }

        static public GameObject[] GetPrefabs(AssetBundle bundle)
        {
            return bundle.LoadAllAssets<GameObject>();
        }

        static public Material GetMaterial(AssetBundle bundle, string name)
        {
            return bundle.LoadAsset<Material>(name);
        }

        static public Material[] GetMaterials(AssetBundle bundle)
        {
            return bundle.LoadAllAssets<Material>();
        }
    }
}
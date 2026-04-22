using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class ImageTracker : MonoBehaviour
{
    public ARTrackedImageManager trackedImageManager;
    public GameObject MonaLisaPrefab;
    public GameObject GreatWavePrefab;
    public GameObject StarryNightPrefab;
    public GameObject GirlWithPearlPrefab;

    private Dictionary<string, GameObject> prefabDict = new Dictionary<string, GameObject>();

    void Awake()
    {
        prefabDict.Add("MonaLisa", MonaLisaPrefab);
        prefabDict.Add("GreatWave", GreatWavePrefab);
        prefabDict.Add("StarryNight", StarryNightPrefab);
        prefabDict.Add("GirlWithPearl", GirlWithPearlPrefab);
    }

    void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (ARTrackedImage trackedImage in args.added)
            UpdateImage(trackedImage);

        foreach (ARTrackedImage trackedImage in args.updated)
            UpdateImage(trackedImage);
    }

    void UpdateImage(ARTrackedImage trackedImage)
    {
        foreach (var pair in prefabDict)
            pair.Value.SetActive(false);

        string name = trackedImage.referenceImage.name;
        if (prefabDict.ContainsKey(name))
        {
            GameObject prefab = prefabDict[name];
            prefab.SetActive(true);
            prefab.transform.position = trackedImage.transform.position;
            prefab.transform.rotation = trackedImage.transform.rotation;
        }
    }
}

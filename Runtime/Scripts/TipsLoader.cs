using FredericRP.BucketGenerator;
using FredericRP.GenericSingleton;
using UnityEngine;

namespace FredericRP.Tips
{
  public class TipsLoader : Singleton<TipsLoader>
  {
    [SerializeField]
    string assetName = null;

    string[] tipList;
    Bucket bucket;
    bool loaded = false;

    // Start is called before the first frame update
    void Start()
    {
      loaded = false;
      ResourceRequest request = Resources.LoadAsync<TextAsset>(assetName);
      request.completed += OnLoadDone;
    }

    void OnLoadDone(AsyncOperation tipsHandle)
    {
      TextAsset tipsFile = (tipsHandle as ResourceRequest).asset as TextAsset;
      if (tipsFile != null)
      {
        tipList = tipsFile.text.Split(new string[] { "\n" }, System.StringSplitOptions.RemoveEmptyEntries);
        bucket = new Bucket(tipList.Length);
        loaded = true;
      }
      else
      {
        Debug.LogWarning("Could not load TextAsset <" + assetName + ">");
      }
    }

    /// <summary>
    /// Get next random tip from loaded text file
    /// </summary>
    /// <returns></returns>
    public string GetTip()
    {
      if (loaded && tipList != null && tipList.Length > 0)
        return tipList[bucket.GetRandomNumber()];
      return null;
    }

  }
}
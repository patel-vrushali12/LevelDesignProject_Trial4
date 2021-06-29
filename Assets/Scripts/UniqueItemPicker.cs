using MoreMountains.CorgiEngine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickerInfo
{
    public string sceneName;
    public Vector3 position;

    public PickerInfo(string sceneName, Vector3 position)
    {
        this.sceneName = sceneName;
        this.position = position;
    }
}

public class UniqueItemPicker : InventoryPickableItem
{
    static List<PickerInfo> usedPickers = new List<PickerInfo>();

    private void Awake()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        foreach (var pickerInfo in usedPickers)
        {
            if (pickerInfo.sceneName != currentSceneName)
            {
                continue;
            }

            if (Vector3.Distance(pickerInfo.position, transform.position) < 0.5f)
            {
                Destroy(gameObject);
                break;
            }
        }
    }

    protected override void PickSuccess()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        usedPickers.Add(new PickerInfo(currentSceneName, transform.position));
        base.PickSuccess();
    }

    public static void Reset()
    {
        usedPickers.Clear();
    }
}

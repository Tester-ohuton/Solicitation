using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class LocaleChanger : MonoBehaviour
{
    [SerializeField] Sprite sprite;

    public void Change(string locale)
    {
        GetComponent<Image>().sprite = sprite;
        var _ = ChangeSelectedLocale(locale);
    }

    private async Task ChangeSelectedLocale(string locale)
    {
        LocalizationSettings.SelectedLocale = Locale.CreateLocale(locale);
        await LocalizationSettings.InitializationOperation.Task;
    }
}
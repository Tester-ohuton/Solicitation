using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LocaleChanger : MonoBehaviour
{
    public void Change(string locale)
    {
        var _ = ChangeSelectedLocale(locale);
    }

    private async Task ChangeSelectedLocale(string locale)
    {
        LocalizationSettings.SelectedLocale = Locale.CreateLocale(locale);
        await LocalizationSettings.InitializationOperation.Task;
    }
}
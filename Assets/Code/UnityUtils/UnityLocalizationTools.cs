using System;
using UnityEngine.Localization.Settings;

namespace Code.UnityUtils
{
    public sealed class UnityLocalizationTools: BaseController
    {
        private readonly string _localizationTable;

        public UnityLocalizationTools(string localizationTable)
        {
            _localizationTable = localizationTable;
        }
        
        public string GetLocalizedString(string entryName, params object[] args)
        {
            var table = LocalizationSettings.StringDatabase.GetTable(_localizationTable);
            if (table == null)
                throw new Exception($"Не удалось загрузить Localization таблицу с именем {_localizationTable}!");

            return table.GetEntry(entryName)?.GetLocalizedString(args);
        }
    }
}
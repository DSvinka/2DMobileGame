using System;
using Code.Types;
using TMPro;
using UnityEngine;

namespace Code.Views.UI
{
    public sealed class PlayerCurrencyView: MonoBehaviour
    {
        public TMP_Text _textWoodCount;
        public TMP_Text _textMetalCount;
        public TMP_Text _textMoneyCount;

        public void Init(int woodCount, int metalCount, int moneyCount)
        {
            _textWoodCount.text = $"{woodCount}";
            _textMetalCount.text = $"{metalCount}";
            _textMoneyCount.text = $"{moneyCount}";
        }
        
        public void SetData(CurrencyType currencyType, int value)
        {
            switch (currencyType)
            {
                case CurrencyType.Metal:
                    _textMetalCount.text = $"{value}";
                    break;
                
                case CurrencyType.Money:
                    _textMoneyCount.text = $"{value}";
                    break;
                
                case CurrencyType.Wood:
                    _textWoodCount.text = $"{value}";
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(currencyType), currencyType, null);
            }
        }
    }
}
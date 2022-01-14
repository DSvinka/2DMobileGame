using System;
using System.Collections.Generic;

namespace Code.Interfaces.Views
{
    public interface IAbilityCollectionView: IView
    {
        event Action<IAbility> UseRequested;

        void Init(IReadOnlyList<IAbility> abilityItems);
    }
}
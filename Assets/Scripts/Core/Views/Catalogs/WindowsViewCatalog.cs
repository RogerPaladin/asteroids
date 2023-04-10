using System;
using System.Linq;
using Model.Windows;
using Static.Windows;
using UnityEngine;
using Views.Windows;

namespace Views.Catalogs
{
    [CreateAssetMenu(fileName = "WindowsViewCatalog", menuName = "Catalogs/View/WindowsViewCatalog")]
    public class WindowsViewCatalog : ScriptableObject
    {
        [field: SerializeField] public WindowViewCatalog[] Windows { get; private set; }

		public WindowViewCatalog GetByType(WindowType type)
		{
			//todo!!!! Windows.First(...) также генерирует Exception. Но лучше подумать как в таких справочниках от массива перейти к dictionary. [SerializeField] Dictionary не работает
			return Windows.FirstOrDefault(e => e.Type == type);
		}
		
		public AbstractWindowView Create(AbstractWindowModel windowModel)
		{
			var effectViewCatalog = GetByType(windowModel.Type);

			if (effectViewCatalog == null)
				throw new Exception(windowModel.Type.ToString());
			
			return Instantiate(effectViewCatalog.View);
		}
	}

    [Serializable]
    public class WindowViewCatalog
    {
        [field: SerializeField] public WindowType Type { get; private set; }
        [field: SerializeField] public AbstractWindowView View { get; private set; }
    }
}
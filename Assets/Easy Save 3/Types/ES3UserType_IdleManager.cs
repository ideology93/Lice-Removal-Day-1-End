using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("unlockedChairs", "unlockedLevels")]
	public class ES3UserType_IdleManager : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_IdleManager() : base(typeof(IdleManager)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (IdleManager)obj;
			
			writer.WriteProperty("unlockedChairs", instance.unlockedChairs, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(System.Collections.Generic.List<Chair>)));
			writer.WriteProperty("unlockedLevels", instance.unlockedLevels, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(System.Collections.Generic.List<UnityEngine.GameObject>)));
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (IdleManager)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "unlockedChairs":
						instance.unlockedChairs = reader.Read<System.Collections.Generic.List<Chair>>();
						break;
					case "unlockedLevels":
						instance.unlockedLevels = reader.Read<System.Collections.Generic.List<UnityEngine.GameObject>>();
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_IdleManagerArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_IdleManagerArray() : base(typeof(IdleManager[]), ES3UserType_IdleManager.Instance)
		{
			Instance = this;
		}
	}
}
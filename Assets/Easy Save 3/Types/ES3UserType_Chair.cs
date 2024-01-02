using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("isUnlocked")]
	public class ES3UserType_Chair : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_Chair() : base(typeof(Chair)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (Chair)obj;
			
			writer.WriteProperty("isUnlocked", instance.isUnlocked, ES3Type_bool.Instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (Chair)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "isUnlocked":
						instance.isUnlocked = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_ChairArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_ChairArray() : base(typeof(Chair[]), ES3UserType_Chair.Instance)
		{
			Instance = this;
		}
	}
}
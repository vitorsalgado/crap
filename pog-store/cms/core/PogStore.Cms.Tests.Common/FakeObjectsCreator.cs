using System;
using System.Collections.Generic;
using System.Reflection;

namespace PogStore.Cms.Tests.Common
{
	public static class FakeObjectsCreator
	{
		private const string DEFAULT_STRING = "generic string for tests purpose *-*";
		private const Int16 DEFAULT_SMALLINT = 16;
		private static readonly Int32 DEFAULT_INTEGER = (new Random().Next(10000));
		private static readonly Int64 DEFAULT_LARGEINTEGER = (new Random().Next(10000));
		private static readonly DateTime DEFAULT_DATE_TIME = DateTime.Now;
		private static readonly Guid DEFAULT_GUID = Guid.NewGuid();
		private static readonly Decimal DEFAULT_DECIMAL = 125.05m;
		private const float DEFAULT_FLOAT = 125.00f;
		private const double DEFAULT_DOUBLE = 190.00;

		public static T CreateSingleInstanceOf<T>() where T : class, new()
		{
			T obj = new T();
			PropertyInfo[] properties = obj.GetType().GetProperties();

			foreach (var property in properties)
				SetPropertyValue<T>(obj, property);

			return obj;
		}

		public static IEnumerable<T> CreateCollectionOf<T>(int totalItens) where T : class, new()
		{
			var list = new List<T>();

			for (int i = 0; i < totalItens; i++)
			{
				T obj = CreateSingleInstanceOf<T>();
				list.Add(obj);
			}

			return list;
		}

		private static void SetPropertyValue<T>(T obj, PropertyInfo property) where T : class, new()
		{
			if (property.PropertyType == typeof(String))
			{
				property.SetValue(obj, DEFAULT_STRING, null);
			}
			else if (property.PropertyType == typeof(Int32) || property.PropertyType == typeof(Nullable<Int32>))
			{
				property.SetValue(obj, DEFAULT_INTEGER, null);
			}
			else if (property.PropertyType == typeof(Int16) || property.PropertyType == typeof(Nullable<Int16>))
			{
				property.SetValue(obj, DEFAULT_SMALLINT, null);
			}
			else if (property.PropertyType == typeof(Int64) || property.PropertyType == typeof(Nullable<Int64>))
			{
				property.SetValue(obj, DEFAULT_LARGEINTEGER, null);
			}
			else if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(Nullable<DateTime>))
			{
				property.SetValue(obj, DEFAULT_DATE_TIME, null);
			}
			else if (property.PropertyType == typeof(Boolean) || property.PropertyType == typeof(Nullable<Boolean>))
			{
				property.SetValue(obj, true, null);
			}
			else if (property.PropertyType == typeof(Guid) || property.PropertyType == typeof(Nullable<Guid>))
			{
				property.SetValue(obj, DEFAULT_GUID, null);
			}
			else if (property.PropertyType == typeof(Decimal) || property.PropertyType == typeof(Nullable<Decimal>))
			{
				property.SetValue(obj, DEFAULT_DECIMAL, null);
			}
			else if (property.PropertyType == typeof(double) || property.PropertyType == typeof(Nullable<double>))
			{
				property.SetValue(obj, DEFAULT_DOUBLE, null);
			}
			else if (property.PropertyType == typeof(float) || property.PropertyType == typeof(Nullable<float>))
			{
				property.SetValue(obj, DEFAULT_FLOAT, null);
			}
		}
	}
}
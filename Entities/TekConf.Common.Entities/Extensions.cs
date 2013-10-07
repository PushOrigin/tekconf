﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TekConf.Common.Entities
{
    using AutoMapper;

    public static class Extensions
	{
		public static bool IsNotNull(this object value)
		{
			return value != null;
		}

        public static Destination Map<Destination>(this object source)
	    {
            var result = Mapper.Map<Destination>(source);

            return result;
	    }

		public static Task<List<T>> ToGenericListAsync<T>(this IQueryable<T> list)
		{
			return Task.Run(() => list.ToList());
		}

		public static bool IsNull(this object value)
		{
			return value == null;
		}

		public static bool IsNullOrWhiteSpace(this string value)
		{
			return string.IsNullOrWhiteSpace(value);
		}

		public static void TrimAllProperties<T>(this T entity) where T : IEntity
		{
			var stringProperties = entity.GetType().GetProperties()
													.Where(p => p.PropertyType == typeof(string));

			foreach (var stringProperty in stringProperties)
			{
				var currentValue = (string)stringProperty.GetValue(entity, null);
				if (!currentValue.IsNullOrWhiteSpace() && stringProperty.CanWrite)
				{
					stringProperty.SetValue(entity, currentValue.Trim().Substring(0, currentValue.Length > 5000 ? 5000 : currentValue.Length), null);
				}
			}
		}
	}



	public static class TypeExtensions
	{
		public static string ToGenericTypeString(this Type t)
		{
			if (!t.IsGenericType)
				return t.Name;
			string genericTypeName = t.GetGenericTypeDefinition().Name;
			genericTypeName = genericTypeName.Substring(0,
					genericTypeName.IndexOf('`'));
			string genericArgs = string.Join(",",
					t.GetGenericArguments()
							.Select(ta => ToGenericTypeString(ta)).ToArray());
			return genericTypeName + "<" + genericArgs + ">";
		}
	}
}

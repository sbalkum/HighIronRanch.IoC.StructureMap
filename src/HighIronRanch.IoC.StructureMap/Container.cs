using System;
using System.Collections.Generic;

namespace HighIronRanch.IoC
{
	public class Container
    {
	    public static StructureMap.Container TheContainer;

		public static T GetInstance<T>()
		{
			return TheContainer.GetInstance<T>();
		}

		public static object GetInstance(Type modelType)
		{
			return TheContainer.GetInstance(modelType);
		}

		public static T TryGetInstance<T>()
		{
			return TheContainer.TryGetInstance<T>();
		}

		public static object TryGetInstance(Type modelType)
		{
			return TheContainer.TryGetInstance(modelType);
		}

		public static IEnumerable<T> GetAllInstances<T>()
		{
			return TheContainer.GetAllInstances<T>();
		}

		public static void BuildUp(object target)
		{
			TheContainer.BuildUp(target);
		}
    }
}

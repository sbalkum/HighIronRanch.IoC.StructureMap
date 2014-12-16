using System.Collections;
using System.Collections.Generic;
using StructureMap;
using StructureMap.Graph;

namespace HighIronRanch.IoC
{
	public class Bootstrapper : IBootstrapper
	{
		private readonly IEnumerable<string> _namespacesToScan;

		public Bootstrapper(IEnumerable<string> namespacesToScan)
		{
			_namespacesToScan = namespacesToScan;
		}

		public void BootstrapStructureMap()
		{
			var container = new StructureMap.Container();

			container.Configure(x => x.Scan(scanner =>
			{
				scanner.TheCallingAssembly();
				//scanner.AssembliesFromApplicationBaseDirectory();
				if (_namespacesToScan != null)
				{
					foreach (var name in _namespacesToScan)
					{
						scanner.AssembliesFromApplicationBaseDirectory(assembly => assembly.GetName().Name.StartsWith(name));
					}
				}

				scanner.LookForRegistries();

				scanner.RegisterConcreteTypesAgainstTheFirstInterface();

				scanner.WithDefaultConventions();
			}));

			var whatIHave = container.WhatDoIHave();
			//container.AssertConfigurationIsValid();

			Container.TheContainer = container;
		}

		public static void Initialize()
		{
			Initialize(null);
		}

		public static void Initialize(IEnumerable<string> namespacesToScan)
		{
			new Bootstrapper(namespacesToScan).BootstrapStructureMap();
		}
	}
}
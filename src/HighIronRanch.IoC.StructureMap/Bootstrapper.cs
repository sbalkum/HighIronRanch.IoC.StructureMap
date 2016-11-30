using System.Collections;
using System.Collections.Generic;
using StructureMap;
using StructureMap.Graph;

namespace HighIronRanch.IoC
{
	public class Bootstrapper
	{
		private readonly IEnumerable<string> _namespacesToScan;

		public Bootstrapper(IEnumerable<string> namespacesToScan)
		{
			_namespacesToScan = namespacesToScan;
		}

		public void BootstrapStructureMap()
		{
            var registry = new Registry();
            registry.Scan(scan =>
            {
                scan.TheCallingAssembly();

                if (_namespacesToScan != null)
                {
                    foreach (var name in _namespacesToScan)
                    {
                        scan.AssembliesFromApplicationBaseDirectory(assembly => assembly.GetName().Name.StartsWith(name));
                    }
                }

                scan.LookForRegistries();

                scan.RegisterConcreteTypesAgainstTheFirstInterface();
                scan.WithDefaultConventions();
            });

            var container = new StructureMap.Container(registry);

			//var whatIHave = container.WhatDoIHave();
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
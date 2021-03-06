﻿using System;
using System.Collections.Generic;
using System.Reflection;

namespace Grace.DependencyInjection.Impl.CompiledExport
{
	/// <summary>
	/// Represents the information needed to build a compiled delegate
	/// </summary>
	public class CompiledExportDelegateInfo
	{
		private List<MethodInfo> _activationMethods;
		private BeforeDisposalCleanupDelegate _cleanupDelegate;
		private List<ConstructorParamInfo> _constructorParamInfos;
		private List<EnrichWithDelegate> _enrichWithDelegates;
		private List<ICustomEnrichmentLinqExpressionProvider> _enrichmentExpressionProviders; 
		private List<ImportMethodInfo> _importMethods;
		private List<ImportPropertyInfo> _importProperties;

	    /// <summary>
		/// Attributes associated with the export type
		/// </summary>
		public IEnumerable<Attribute> Attributes { get; set; }

		/// <summary>
		/// Is this export transient
		/// </summary>
		public bool IsTransient { get; set; }

		/// <summary>
		/// Create new context before injecting
		/// </summary>
		public bool InNewContext { get; set; }

		/// <summary>
		/// Track the export if disposable
		/// </summary>
		public bool TrackDisposable { get; set; }

		/// <summary>
		/// This is the type that is being activated can be interface
		/// </summary>
		public Type ActivationType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public BeforeDisposalCleanupDelegate CleanupDelegate
		{
			get { return _cleanupDelegate; }
		}

		/// <summary>
		/// List of properties to import
		/// </summary>
		public IEnumerable<ImportPropertyInfo> ImportProperties
		{
			get { return _importProperties; }
		}

		/// <summary>
		/// List of methods to import
		/// </summary>
		public IEnumerable<ImportMethodInfo> ImportMethods
		{
			get { return _importMethods; }
		}

		/// <summary>
		/// Constructor to use for importing, can be null
		/// </summary>
		public ConstructorInfo ImportConstructor { get; private set; }

		/// <summary>
		/// List of constructor arguements
		/// </summary>
		public IEnumerable<ConstructorParamInfo> ConstructorParams
		{
			get { return _constructorParamInfos; }
		}

		/// <summary>
		/// List of activation methods 
		/// </summary>
		public IEnumerable<MethodInfo> ActivationMethodInfos
		{
			get { return _activationMethods; }
		}

		/// <summary>
		/// List of enrichment delegates to apply to the export
		/// </summary>
		public IEnumerable<EnrichWithDelegate> EnrichmentDelegates
		{
			get { return _enrichWithDelegates; }
		}

		/// <summary>
		/// Get a list of enrichment expression providers
		/// </summary>
		public IEnumerable<ICustomEnrichmentLinqExpressionProvider> EnrichmentExpressionProviders
		{
			get { return _enrichmentExpressionProviders; }
		}

        /// <summary>
        /// Custom constructor enrichment
        /// </summary>
	    public ICustomConstructorEnrichmentLinqExpressionProvider CustomConstructorEnrichment { get; set; }

	    /// <summary>
		/// Adds a constructor parameter info
		/// </summary>
		/// <param name="paramInfo"></param>
		public void AddConstructorParamInfo(ConstructorParamInfo paramInfo)
		{
			if (_constructorParamInfos == null)
			{
				_constructorParamInfos = new List<ConstructorParamInfo>(1);
			}

			_constructorParamInfos.Add(paramInfo);
		}

		/// <summary>
		/// Imports a property for the export
		/// </summary>
		/// <param name="info"></param>
		public void ImportProperty(ImportPropertyInfo info)
		{
			if (_importProperties == null)
			{
				_importProperties = new List<ImportPropertyInfo>(1);
			}

			_importProperties.Add(info);
		}

		/// <summary>
		/// Mark a method for importing 
		/// </summary>
		public void ImportMethod(ImportMethodInfo info)
		{
			if (_importMethods == null)
			{
				_importMethods = new List<ImportMethodInfo>(1);
			}

			_importMethods.Add(info);
		}

		/// <summary>
		/// Mark a method to be called during activation
		/// </summary>
		/// <param name="methodInfo"></param>
		public void ActivateMethod(MethodInfo methodInfo)
		{
			if (_activationMethods == null)
			{
				_activationMethods = new List<MethodInfo>(1);
			}

			_activationMethods.Add(methodInfo);
		}

		/// <summary>
		/// Add an enrich with delegate to the strategy
		/// </summary>
		/// <param name="enrichWithDelegate"></param>
		public void EnrichWithDelegate(EnrichWithDelegate enrichWithDelegate)
		{
			if (_enrichWithDelegates == null)
			{
				_enrichWithDelegates = new List<EnrichWithDelegate>(1);
			}

			_enrichWithDelegates.Add(enrichWithDelegate);
		}

		/// <summary>
		/// Add a custom enrichment expression provider
		/// </summary>
		/// <param name="provider">expression provider</param>
		public void EnrichmentExpressionProvider(ICustomEnrichmentLinqExpressionProvider provider)
		{
			if (_enrichmentExpressionProviders == null)
			{
				_enrichmentExpressionProviders = new List<ICustomEnrichmentLinqExpressionProvider>();
			}

			_enrichmentExpressionProviders.Add(provider);
		}

		/// <summary>
		/// Sets the constructor info for the export
		/// </summary>
		/// <param name="constructorInfo"></param>
		public void SetImportConstructor(ConstructorInfo constructorInfo)
		{
			ImportConstructor = constructorInfo;
		}

		/// <summary>
		/// Adds new cleanup delegate 
		/// </summary>
		/// <param name="newCleanupDelegate"></param>
		public void AddCleanupDelegate(BeforeDisposalCleanupDelegate newCleanupDelegate)
		{
			_cleanupDelegate += newCleanupDelegate;
		}
	}

	/// <summary>
	/// Information on how to import a method
	/// </summary>
	public class ImportMethodInfo
	{
		/// <summary>
		/// After construction
		/// </summary>
		public bool AfterConstruction { get; set; }

		/// <summary>
		/// Import the method
		/// </summary>
		public MethodInfo MethodToImport { get; set; }

		/// <summary>
		/// List of method params for the import
		/// </summary>
		public List<MethodParamInfo> MethodParamInfos { get; private set; }

		/// <summary>
		/// Add a method parameter
		/// </summary>
		/// <param name="methodParamInfo"></param>
		public void AddMethodParamInfo(MethodParamInfo methodParamInfo)
		{
			if (MethodParamInfos == null)
			{
				MethodParamInfos = new List<MethodParamInfo>();
			}

			MethodParamInfos.Add(methodParamInfo);
		}
	}

	/// <summary>
	/// Information on how to import method parameter
	/// </summary>
	public class MethodParamInfo
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public MethodParamInfo()
		{
			IsRequired = true;
		}

		/// <summary>
		/// After construction
		/// </summary>
		public bool? AfterConstruction { get; set; }

		/// <summary>
		/// parameter name being configured
		/// </summary>
		public string ParameterName { get; set; }

		/// <summary>
		/// paraeter type being configured
		/// </summary>
		public Type ParameterType { get; set; }

		/// <summary>
		/// export filter
		/// </summary>
		public ExportStrategyFilter Filter { get; set; }

		/// <summary>
		/// Comparer object
		/// </summary>
		public object Comparer { get; set; }

		/// <summary>
		/// Is required when exporting
		/// </summary>
		public bool IsRequired { get; set; }

		/// <summary>
		/// Import name 
		/// </summary>
		public string ImportName { get; set; }

		/// <summary>
		/// Value provider for the import
		/// </summary>
		public IExportValueProvider ValueProvider { get; set; }

		/// <summary>
		/// Locate key
		/// </summary>
		public ILocateKeyValueProvider LocateKey { get; set; }
	}

	/// <summary>
	/// Information on how to import a property
	/// </summary>
	public class ImportPropertyInfo
	{
		/// <summary>
		/// Default Constructor
		/// </summary>
		public ImportPropertyInfo()
		{
			IsRequired = true;
			AfterConstruction = false;
		}

		/// <summary>
		/// Import name to use if not importing by type
		/// </summary>
		public string ImportName { get; set; }

		/// <summary>
		/// Is the import required
		/// </summary>
		public bool IsRequired { get; set; }

		/// <summary>
		/// Property to import
		/// </summary>
		public PropertyInfo Property { get; set; }

		/// <summary>
		/// Value provider for the import
		/// </summary>
		public IExportValueProvider ValueProvider { get; set; }

		/// <summary>
		/// Export filter associated with this import
		/// </summary>
		public ExportStrategyFilter ExportStrategyFilter { get; set; }

		/// <summary>
		/// Locate key
		/// </summary>
		public ILocateKeyValueProvider LocateKey { get; set; }

		/// <summary>
		/// IComparer(T) to be used to sort collections
		/// </summary>
		public object ComparerObject { get; set; }

		/// <summary>
		/// Import the property after construction
		/// </summary>
		public bool AfterConstruction { get; set; }
	}

	/// <summary>
	/// Information about a constructor arguement
	/// </summary>
	public class ConstructorParamInfo
	{
		/// <summary>
		/// Default Constructor
		/// </summary>
		public ConstructorParamInfo()
		{
			IsRequired = true;
		}

		/// <summary>
		/// Name of the arguement 
		/// </summary>
		public string ParameterName { get; set; }

		/// <summary>
		/// Type of the arguement 
		/// </summary>
		public Type ParameterType { get; set; }

		/// <summary>
		/// Import name to use if not importing by type
		/// </summary>
		public string ImportName { get; set; }

		/// <summary>
		/// Is the import required
		/// </summary>
		public bool IsRequired { get; set; }

		/// <summary>
		/// The value provider
		/// </summary>
		public IExportValueProvider ValueProvider { get; set; }

		/// <summary>
		/// Export filter associated with this import
		/// </summary>
		public ExportStrategyFilter ExportStrategyFilter { get; set; }

		/// <summary>
		/// Locate key
		/// </summary>
		public ILocateKeyValueProvider LocateKeyProvider { get; set; }

		/// <summary>
		/// IComparer(T) to use to compare export values
		/// </summary>
		public object ComparerObject { get; set; }
	}
}
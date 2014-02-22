﻿using System;
using Grace.DependencyInjection.Attributes.Interfaces;
using Grace.DependencyInjection.Conditions;

namespace Grace.DependencyInjection.Attributes
{
	/// <summary>
	/// Applies an condition on the export where it will be injected into specified types and only those types
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
	public class ExportWhenInjectedIntoAttribute : Attribute, IExportConditionAttribute
	{
		private readonly Type[] injectedTypes;

		/// <summary>
		/// Default constructor takes list of injected types
		/// </summary>
		/// <param name="injectedTypes">types that this export can be used in</param>
		public ExportWhenInjectedIntoAttribute(params Type[] injectedTypes)
		{
			this.injectedTypes = injectedTypes;
		}

		/// <summary>
		/// Provide a new WhenInjectedInto condition
		/// </summary>
		/// <param name="exportType">attributed type</param>
		/// <returns>new condition</returns>
		public IExportCondition ProvideCondition(Type exportType)
		{
			return new WhenInjectedInto(injectedTypes);
		}
	}
}
﻿using System;   
using System.Configuration;
using System.Linq.Expressions;

namespace NSimpleOLAP.Configuration
{
	/// <summary>
	/// Represents a single XML tag inside a ConfigurationSection
	/// or a ConfigurationElementCollection.
	/// </summary>
	public sealed class MetricElement : ConfigurationElement
	{
		/// <summary>
		/// The attribute <c>name</c> of a <c>MetricElement</c>.
		/// </summary>
		[ConfigurationProperty("name", IsKey = true, IsRequired = true)]
		public string Name
		{
			get { return (string)this["name"]; }
			set { this["name"] = value; }
		}
	
	
		/// <summary>
		/// A demonstration of how to use a boolean property.
		/// </summary>
		[ConfigurationProperty("special")]
		public bool IsSpecial {
			get { return (bool)this["special"]; }
			set { this["special"] = value; }
		}
		
		[ConfigurationProperty("id")]
		public ValueType ID
		{
			get { return (ValueType)this["id"]; }
			set { this["id"] = value; }
		}
		
		public Type DataType
		{
			get;
			set;
		}
		
		public Expression MetricFunction
		{
			get;
			set;
		}
	}
	
}


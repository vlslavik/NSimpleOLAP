﻿using System;
using System.Collections.Generic;
using NSimpleOLAP.Interfaces;
using NSimpleOLAP.Schema;
using NSimpleOLAP.Schema.Interfaces;
using NSimpleOLAP.Storage;
using NSimpleOLAP.Storage.Interfaces;
using NSimpleOLAP.Configuration;
using NSimpleOLAP.Data;
using NSimpleOLAP.Common;
using NSimpleOLAP.Common.Interfaces;

namespace NSimpleOLAP
{
	/// <summary>
	/// Description of Cube.
	/// </summary>
	public class Cube<T> : ICube<T, Cell<T>>
		where T: struct, IComparable
	{
		public Cube()
		{
			this.Config = DefaultCubeConfiguration.GetConfig();
			this.Init();
		}
		
		public Cube(CubeConfig config)
		{
			this.Config = config;
			this.Init();
		}
		
		#region props
		
		public T Key {
			get;
			set;
		}
		
		public string Name {
			get;
			set;
		}
		
		public string Source { 
			get; 
			set; 
		}
		
		public INamespace<T> NameSpace { 
			get; 
			private set;
		}
		
		public DataSchema<T> Schema {
			get;
			private set;
		}
		
		public IStorage<T, Cell<T>> Storage {
			get;
			private set;
		}
		
		public ICellCollection<T, Cell<T>> Cells { 
			get;
			private set;
		}
		
		public DataSourceCollection DataSources {
			get;
			private set;
		}
		
		public bool IsProcessing {
			get;
			private set;
		}
		
		public CubeConfig Config { 
			get; 
			internal set;
		}
		
		#endregion
		
		#region IDisposable implementations
		
		public void Dispose()
		{
			if (Schema != null)
			{
				this.Schema.Dispose();
				this.Storage.Dispose();
				this.NameSpace.Dispose();
			}
			
			this.DataSources = null;
		}
		
		#endregion
		
		public void Initialize()
		{
			this.Storage = StorageFactory<T, Cell<T>>.Create(this.Key, this.Config.Storage);
			this.NameSpace = Storage.NameSpace;
			this.DataSources = new DataSourceCollection(this.Config);
			this.Schema = new DataSchema<T>(this.Config,this.DataSources, 
			                                this.Storage.Dimensions, this.Storage.Measures,
			                              this.Storage.Metrics);
		}
		
		#region IProcess implementation
		
		public void Process()
		{
			this.Schema.Process();
		}
		
		public void Refresh()
		{
			throw new NotImplementedException();
		}
		
		#endregion
		
		#region private methods
		
		private void Init()
		{
			this.Name = this.Config.Name;
			this.Source = this.Config.Source;
		}
		
		#endregion
	}
}

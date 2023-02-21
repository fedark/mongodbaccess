﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;
using MongoDbAccess;
using MongoDbAccess.DataAccess;
using MongoDbAccess.DataAccess.Abstractions;
using MongoDbAccess.Models;

namespace Mock
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine(new Model().ToString());
		}
	}

	[CachedCollection(1)]         
	public class Model : IModel
	{
		public string Id { get; set; }
	}

	[CollectionDefinition]
	public interface IModelCollection : IDbCollection<Model>
	{
		Task AuxOperationAsync(Model model);
	}

	//[CollectionImplementation]
	public partial class MongoModelCollection
	{
		public override Task AuxOperationAsync(Model model)
		{
			Cache.Remove();
			return Task.CompletedTask;
		}

		public static void Test()
		{
			MongoModelCollection collection = new MongoModelCollection();
			collection.GetAllAsync();
			collection.AuxOperationAsync(new());
		}
	}
}

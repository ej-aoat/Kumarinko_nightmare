﻿using Akalib.Entity;
using log4net;
using Mogami.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogami.Gateway
{
	public sealed class AppDbContext : AtDbContext
	{

		#region フィールド

		public static SQLiteConnectionStringBuilder SDbConnection;
		private static ILog LOG = LogManager.GetLogger(typeof(AppDbContext));

		#endregion フィールド


		#region コンストラクタ

		public AppDbContext()
		: base(new SQLiteConnection(SDbConnection.ConnectionString), true)
		{
		}

		#endregion コンストラクタ


		#region プロパティ

		public DbSet<ApMetadata> ApMetadatas { get; set; }

		#endregion プロパティ


		#region メソッド

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}

		protected override System.Data.Entity.Validation.DbEntityValidationResult ValidateEntity(System.Data.Entity.Infrastructure.DbEntityEntry entityEntry, IDictionary<object, object> items)
		{
			return base.ValidateEntity(entityEntry, items);
		}

		#endregion メソッド
	}
}

﻿using AutoMapper;
using log4net;
using Mogami.Model;
using Mogami.Service.Construction;
using Mogami.Service.Response;
using Mogami.Service.Serialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Mogami.Service.Request;
using Mogami.Gateway;
using Mogami.Model.Repository;

namespace Mogami.Service
{
	

	[ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single, IncludeExceptionDetailInFaults = true)]
	public class MogamiApiService : IMogamiApiService
	{


		#region フィールド

		static ILog LOG = LogManager.GetLogger(typeof(MogamiApiService));

		static IMapper Mapper;

		static MapperConfiguration MapperConfig;

		#endregion フィールド


		#region コンストラクタ

		public MogamiApiService()
		{
			MapperConfig = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<Artifact, DataArtifact>();
			});

			Mapper = MapperConfig.CreateMapper();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="reqparam"></param>
		/// <returns></returns>
		public RESPONSE_FINDARTIFACT FindArtifact(REQUEST_FINDARTIFACT reqparam)
		{
			var rsp = new RESPONSE_FINDARTIFACT();
			
			using(var dbc = new AppDbContext())
			{
				var repo = new ArtifactRepository(dbc);
				foreach(var prop in repo.GetAll())
				{
					rsp.Artifacts.Add(prop);
				}
			}

			using(var tdbc = new ThumbDbContext())
			{
				rsp.Thumbnails = new List<byte[]>();
				var repo = new ThumbnailRepository(tdbc);
				foreach (var prop in rsp.Artifacts)
				{
					if (!string.IsNullOrEmpty(prop.ThumbnailKey))
					{
						var thumb = repo.FindFromKey(prop.ThumbnailKey).FirstOrDefault();
						if (thumb != null) rsp.Thumbnails.Add(thumb.BitmapBytes);
						else rsp.Thumbnails.Add(null);
					}
				}
			}

			rsp.Success = true;
			return rsp;
		}

		#endregion コンストラクタ

		#region メソッド

		/// <summary>
		/// 
		/// </summary>
		/// <param name="versionType"></param>
		/// <returns></returns>
		public RESPONSE_GETSERVERVERSION GetServerVersion(VERSION_SELECTOR versionType)
		{
			var result = new RESPONSE_GETSERVERVERSION();
			switch (versionType)
			{
				case VERSION_SELECTOR.API_VERSION:
					result.VersionText = "1.0.0";
					result.Success = true;
					break;
				case VERSION_SELECTOR.DATABASE_VERSION:
					result.VersionText = "1.0.0";
					result.Success = true;
					break;
				case VERSION_SELECTOR.SERVICE_VERSION:
					result.VersionText = "1.0.0";
					result.Success = true;
					break;
				default:
					result.Success = false;
					break;
			}

			return result;
		}

		public void Login()
		{
			
		}

		public void Logout()
		{
			
		}

		#endregion メソッド
	}
}

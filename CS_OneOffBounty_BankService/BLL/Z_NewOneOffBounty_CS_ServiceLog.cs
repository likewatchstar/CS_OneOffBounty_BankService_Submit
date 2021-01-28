/**  版本信息模板在安装目录下，可自行修改。
* Z_NewOneOffBounty_CS_ServiceLog.cs
*
* 功 能： N/A
* 类 名： Z_NewOneOffBounty_CS_ServiceLog
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2021-01-27 16:36:23   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Collections.Generic;
namespace CS_OneOffBounty_BankService.BLL
{
	/// <summary>
	/// Z_NewOneOffBounty_CS_ServiceLog
	/// </summary>
	public partial class Z_NewOneOffBounty_CS_ServiceLog
	{
		private readonly DAL.Z_NewOneOffBounty_CS_ServiceLog dal=new DAL.Z_NewOneOffBounty_CS_ServiceLog();
		public Z_NewOneOffBounty_CS_ServiceLog()
		{}
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(Model.Z_NewOneOffBounty_CS_ServiceLog model)
		{
			 dal.Add(model);
		}

		

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Model.Z_NewOneOffBounty_CS_ServiceLog GetModel(string Guid)
		{
			return dal.GetModel(Guid);
		}
	}
}


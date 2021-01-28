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
namespace CS_OneOffBounty_BankService.Model
{
	/// <summary>
	/// Z_NewOneOffBounty_CS_ServiceLog:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Z_NewOneOffBounty_CS_ServiceLog
	{
		public Z_NewOneOffBounty_CS_ServiceLog()
		{}
		#region Model
		private string _guid;
		private string _requesttype;
		private string _requestmessage;
		private DateTime? _requesttime;
		private bool? _success;
		private string _responsemessage;
		private string _addby;
		private DateTime? _addtime;
		/// <summary>
		/// 
		/// </summary>
		public string Guid
		{
			set{ _guid=value;}
			get{return _guid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RequestType
		{
			set{ _requesttype=value;}
			get{return _requesttype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RequestMessage
		{
			set{ _requestmessage=value;}
			get{return _requestmessage;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? RequestTime
		{
			set{ _requesttime=value;}
			get{return _requesttime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool? Success
		{
			set{ _success=value;}
			get{return _success;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ResponseMessage
		{
			set{ _responsemessage=value;}
			get{return _responsemessage;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AddBy
		{
			set{ _addby=value;}
			get{return _addby;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? AddTime
		{
			set{ _addtime=value;}
			get{return _addtime;}
		}
		#endregion Model

	}
}


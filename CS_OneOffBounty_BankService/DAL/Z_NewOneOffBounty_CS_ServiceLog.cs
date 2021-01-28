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
using System.Text;
using System.Data.SqlClient;
namespace CS_OneOffBounty_BankService.DAL
{
	/// <summary>
	/// 数据访问类:Z_NewOneOffBounty_CS_ServiceLog
	/// </summary>
	public partial class Z_NewOneOffBounty_CS_ServiceLog
	{
		public Z_NewOneOffBounty_CS_ServiceLog()
		{}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(Model.Z_NewOneOffBounty_CS_ServiceLog model)
		{
			StringBuilder strSql=new StringBuilder();
			StringBuilder strSql1=new StringBuilder();
			StringBuilder strSql2=new StringBuilder();
			if (model.Guid != null)
			{
				strSql1.Append("Guid,");
				strSql2.Append("'"+model.Guid+"',");
			}
			if (model.RequestType != null)
			{
				strSql1.Append("RequestType,");
				strSql2.Append("'"+model.RequestType+"',");
			}
			if (model.RequestMessage != null)
			{
				strSql1.Append("RequestMessage,");
				strSql2.Append("'"+model.RequestMessage+"',");
			}
			if (model.RequestTime != null)
			{
				strSql1.Append("RequestTime,");
				strSql2.Append("'"+model.RequestTime+"',");
			}
			if (model.Success != null)
			{
				strSql1.Append("Success,");
				strSql2.Append(""+(bool.Parse(model.Success.ToString()) ? 1 : 0) +",");
			}
			if (model.ResponseMessage != null)
			{
				strSql1.Append("ResponseMessage,");
				strSql2.Append("'"+model.ResponseMessage+"',");
			}
			if (model.AddBy != null)
			{
				strSql1.Append("AddBy,");
				strSql2.Append("'"+model.AddBy+"',");
			}
			if (model.AddTime != null)
			{
				strSql1.Append("AddTime,");
				strSql2.Append("'"+model.AddTime+"',");
			}
			strSql.Append("insert into Z_NewOneOffBounty_CS_ServiceLog(");
			strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
			strSql.Append(")");
			strSql.Append(" values (");
			strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
			strSql.Append(")");
			SqlHelper.ExecuteNonQuery(strSql.ToString());
		}



		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Model.Z_NewOneOffBounty_CS_ServiceLog GetModel(string Guid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1  ");
			strSql.Append(" * ");
			strSql.Append(" from Z_NewOneOffBounty_CS_ServiceLog ");
			strSql.Append(" where Guid='"+Guid+"' " );
			DataSet ds=SqlHelper.ExecuteDataset(strSql.ToString());
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Model.Z_NewOneOffBounty_CS_ServiceLog DataRowToModel(DataRow row)
		{
			Model.Z_NewOneOffBounty_CS_ServiceLog model=new Model.Z_NewOneOffBounty_CS_ServiceLog();
			if (row != null)
			{
				if(row["Guid"]!=null)
				{
					model.Guid=row["Guid"].ToString();
				}
				if(row["RequestType"]!=null)
				{
					model.RequestType=row["RequestType"].ToString();
				}
				if(row["RequestMessage"]!=null)
				{
					model.RequestMessage=row["RequestMessage"].ToString();
				}
				if(row["RequestTime"]!=null && row["RequestTime"].ToString()!="")
				{
					model.RequestTime=DateTime.Parse(row["RequestTime"].ToString());
				}
				if(row["Success"]!=null && row["Success"].ToString()!="")
				{
					if((row["Success"].ToString()=="1")||(row["Success"].ToString().ToLower()=="true"))
					{
						model.Success=true;
					}
					else
					{
						model.Success=false;
					}
				}
				if(row["ResponseMessage"]!=null)
				{
					model.ResponseMessage=row["ResponseMessage"].ToString();
				}
				if(row["AddBy"]!=null)
				{
					model.AddBy=row["AddBy"].ToString();
				}
				if(row["AddTime"]!=null && row["AddTime"].ToString()!="")
				{
					model.AddTime=DateTime.Parse(row["AddTime"].ToString());
				}
			}
			return model;
		}

	}
}


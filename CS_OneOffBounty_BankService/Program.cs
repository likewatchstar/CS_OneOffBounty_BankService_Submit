using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using LibBatchPlatform;
using System.Net;
using System.Data;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Configuration;

namespace CS_OneOffBounty_BankService
{
    class Program
    {
        static void Main()
        {
            string RequestMessage = "";
            var NowTime = DateTime.Now;
            DataTable GobalDt = null;
            try
            {
                Console.WriteLine("Running...");
                var dt = SqlHelper.ExecuteDataset("select * from Z_NewOneOffbounty_CS where SendState='1' and  isnull(AgentBankNumber,'')<>BankNUmber and isnull(Survival,'')<>''  and SubmitBatch is  null  ").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    GobalDt = dt;
                    ABC_SB_Submit submit = new ABC_SB_Submit
                    {
                        trcode = "submit",
                        trdate = NowTime.ToString("yyyyMMdd"),
                        traddr = "U",
                        unitid = "3734",
                        trtype = "",
                        trname = "",
                        settdate = NowTime.ToString("yyyyMMdd"),
                        serial = NowTime.ToString("yyyy").Substring(2, 2) + NowTime.ToString("MMddHHmmss"),
                        sum = (dt.Rows.Count * 3600).ToString(),
                        total = dt.Rows.Count.ToString()
                    };
                    List<ABC_SB_Item> items = new List<ABC_SB_Item>();
                    foreach (DataRow row in dt.Rows)
                    {
                        ABC_SB_Item item = new ABC_SB_Item
                        {
                            idno = dt.Rows.IndexOf(row).ToString(),
                            accno1 = "",
                            accname1 = "",
                            bankno1 = "",
                            bankname1 = "",
                            accno2 = row["Survival"].ToString() == "1" ? row["BankNumber"].ToString() : row["AgentBankNumber"].ToString(),
                            accname2 = row["Survival"].ToString() == "1" ? row["Name"].ToString() : row["AgentName"].ToString(),
                            bankno2 = "",
                            bankname2 = row["Survival"].ToString() == "1" ? GetNameByCodeAndModule(row["BankName"].ToString(), "cs_ycx_txry_yhmc") : GetNameByCodeAndModule(row["AgentBankName"].ToString(), "cs_ycx_txry_yhmc"),
                            custno = row["Survival"].ToString() == "1" ? row["Idcard"].ToString() : row["AgentIdcard"].ToString(),
                            custname = row["Survival"].ToString() == "1" ? row["Name"].ToString() : row["AgentName"].ToString(),
                            tramt = "3600",
                            summary = "测试"
                        };
                        items.Add(item);
                    }
                    submit.list = items.ToArray();
                    RequestMessage = JsonConvert.SerializeObject(submit);

                    var Url = SqlHelper.InterfaceUrl;
                    WebClient webClient = new WebClient();
                    webClient.Headers["Content-Type"] = "application/json";

                    foreach (DataRow row in dt.Rows)
                    {
                        SqlHelper.ExecuteNonQuery("update Z_NewOneOffBounty_CS set SubmitBatch='" + NowTime.ToString() + "' where Guid='" + row["Guid"].ToString() + "'");
                    }
                    var Response = webClient.UploadData(Url, "POST", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(submit)));
                    string StringResponse = "";
                    ABC_SB_Submit_Res ObjectResponse = null;

                    try
                    {
                         StringResponse = Encoding.UTF8.GetString(Response);
                         ObjectResponse = JsonConvert.DeserializeObject<ABC_SB_Submit_Res>(StringResponse);
                    }
                    catch (Exception ex)
                    {
                        
                    }
                    if (ObjectResponse.RetCode == "0000")
                    {
                        var Model = GetLogModel(RequestMessage, NowTime, true, ObjectResponse.RetMsg);
                        InsertLog(Model);
                        foreach (DataRow row in dt.Rows)
                        {
                            SqlHelper.ExecuteNonQuery("update Z_NewOneOffBounty_CS set SendState='3',SubmitTime='"+NowTime.ToString()+"' where Guid='"+row["Guid"].ToString()+"'");
                        }
                    }
                    else
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            SqlHelper.ExecuteNonQuery("update Z_NewOneOffBounty_CS set SubmitBatch=null where Guid='" + row["Guid"].ToString() + "'");
                        }
                        var Model=GetLogModel(RequestMessage, NowTime, false, ObjectResponse.RetMsg);
                        InsertLog(Model);
                    }
                }
                else
                {
                    var Model=GetLogModel("没有需要提交银行的数据", NowTime, false,"");
                    InsertLog(Model);
                }
            }
            catch (Exception ex)
            {
                if (GobalDt != null)
                {
                    foreach (DataRow row in GobalDt.Rows)
                    {
                        SqlHelper.ExecuteNonQuery("update Z_NewOneOffBounty_CS set SubmitBatch=null where Guid='" + row["Guid"].ToString() + "'");
                    }
                }
                var Model = GetLogModel(RequestMessage, NowTime, false, ex.Message);
                try
                {
                    InsertLog(Model);
                }
                catch (Exception ex2)
                {
                    AddLogToTxt(JsonConvert.SerializeObject(Model)+":报错:"+ex2.Message);
                }
            }
        }

        static string GetNameByCodeAndModule(string code, string module)
        {
            string res = "";
            var dt = SqlHelper.ExecuteDataset("select * from Z_T_CommonTable where code='" + code + "' and module='" + module + "'").Tables[0];
            if (dt.Rows.Count > 0)
            {
                res = dt.Rows[0]["name1"].ToString();
            }
            return res;
        }

        static Model.Z_NewOneOffBounty_CS_ServiceLog GetLogModel(string RequestMessage,DateTime NowTime,bool Success,string ResponseMessage)
        {
            Model.Z_NewOneOffBounty_CS_ServiceLog Model = new Model.Z_NewOneOffBounty_CS_ServiceLog();
            Model.Guid = Guid.NewGuid().ToString();
            Model.RequestType = "Submit";
            Model.RequestMessage = RequestMessage;
            Model.RequestTime = NowTime;
            Model.Success = Success;
            Model.ResponseMessage = ResponseMessage;
            Model.AddBy = "定时提交脚本";
            Model.AddTime = NowTime;
            return Model;
        }


        static void InsertLog(Model.Z_NewOneOffBounty_CS_ServiceLog Model)
        {
            BLL.Z_NewOneOffBounty_CS_ServiceLog bll = new BLL.Z_NewOneOffBounty_CS_ServiceLog();
            bll.Add(Model);
        }


         static void AddLogToTxt(string str)
        {
            string fileName = Process.GetCurrentProcess().MainModule.FileName;
            fileName = fileName.Substring(0, fileName.LastIndexOf(@"\") + 1) + "日志Log";
            if (!Directory.Exists(fileName))
            {
                Directory.CreateDirectory(fileName);
            }
            string path = fileName + @"\" + ConfigurationManager.AppSettings["LogFileName"] + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            if (!File.Exists(path))
            {
                StreamWriter writer = File.CreateText(path);
                writer.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + "  " + str);
                writer.Close();
            }
            else
            {
                StreamWriter writer2 = File.AppendText(path);
                writer2.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + "  " + str);
                writer2.Close();
            }

        }
    }
}

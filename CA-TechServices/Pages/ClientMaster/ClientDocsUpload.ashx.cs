using CA_TechService.Common.Generic;
using CA_TechService.Common.Transport.ClientMaster;
using CA_TechService.Data.DataSource.ClientMaster;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
namespace CA_TechServices.Pages.ClientMaster
{
    /// <summary>
    /// Summary description for ClientDocsUpload
    /// </summary>
    public class ClientDocsUpload : IHttpHandler
    {
        string folderpath;
        string actiontype;
        string org_file_name;
        string phy_file_name;
        string clientid;
        int mFileSize = 0;
        string remarks;

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.QueryString["action"] != null)
            {
                actiontype = context.Request.QueryString["action"].ToString();
                folderpath = System.Configuration.ConfigurationManager.AppSettings["ClientFolderPath"];
                clientid = context.Request.QueryString["clientid"].ToString();
                phy_file_name = context.Request.QueryString["phy_file_name"].ToString();
                if (actiontype.Trim().ToUpper() == "UPLOAD")
                {
                    //for uploading new File                    
                    var postedFile = context.Request.Files[0];
                    string filesize = System.Configuration.ConfigurationManager.AppSettings["FileSize"];
                    mFileSize = postedFile.ContentLength / 1048576;
                    string Savepath = context.Server.MapPath("~//" + folderpath);
                    if (mFileSize <= Convert.ToInt32(filesize))
                    {
                        org_file_name = context.Request.QueryString["org_file_name"].ToString();
                        remarks = context.Request.QueryString["remarks"].ToString();

                        if (!Directory.Exists(Savepath))
                            Directory.CreateDirectory(Savepath);

                        postedFile.SaveAs(Savepath + "\\" + phy_file_name);

                        ClientDocsEntity objdocs = new ClientDocsEntity();
                        objdocs.CLIENT_ID = Convert.ToInt64(clientid);
                        objdocs.REMARKS = remarks;
                        objdocs.ORG_FILE_NAME = org_file_name;
                        objdocs.PHY_FILE_NAME = phy_file_name;
                        List<DbStatusEntity> details = new List<DbStatusEntity>();

                        details.Add(new ClientMasterDAO().InsertClientDocs(objdocs));

                        //Set response message
                        string msg = "{";
                        msg += string.Format("error:'{0}',\n", string.Empty);
                        msg += string.Format("upfile:'{0}'\n", phy_file_name);
                        msg += "}";
                        context.Response.Write(msg);
                    }
                }
                else if (actiontype.Trim().ToUpper() == "DELETE")
                {
                    string Savepath = context.Server.MapPath("~//" + folderpath);

                    if (File.Exists(Savepath + "\\" + phy_file_name))
                    {
                        File.Delete(Savepath + "\\" + phy_file_name);
                    }

                    List<DbStatusEntity> details = new List<DbStatusEntity>();

                    details.Add(new ClientMasterDAO().DeleteClientDocs(Convert.ToInt64(clientid), phy_file_name));
                }
                else if (actiontype.Trim().ToUpper() == "DOWNLOAD")
                {

                    org_file_name = context.Request.QueryString["org_file_name"].ToString();
                    string Savepath = context.Server.MapPath("~//" + folderpath);
                    if (File.Exists(Savepath + "\\" + phy_file_name))
                    {
                        context.Response.Clear();
                        context.Response.ContentType = "application/octet-stream";
                        context.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=\"{0}\"", org_file_name));
                        context.Response.WriteFile(Savepath + "\\" + phy_file_name);
                        context.Response.Flush();
                    }
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;

namespace GISLSB
{
    public partial class FormExtractMsg : Form
    {
        /// <summary>
        /// MainFrame
        /// </summary>
        private MainFrame mainFrame = null;

        public FormExtractMsg()
        {
            InitializeComponent();
        }

        public FormExtractMsg(MainFrame mainframe)
        {
            InitializeComponent();
            mainFrame = mainframe;
        }

        private void buttonSetPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbDlg = new FolderBrowserDialog();
            fbDlg.Description = "选择保存路径";
            fbDlg.ShowNewFolderButton = true;
            if (fbDlg.ShowDialog() == DialogResult.OK)
            {
                textPath.Text = fbDlg.SelectedPath;
            }
        }

        private void buttonCommitExtract_Click(object sender, EventArgs e)
        {
            if (textPath.Text == "")
            {
                MessageBox.Show("请指定有效的文件保存路径！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            MDBManager mdbManager = new MDBManager(mainFrame.accessPath);
            string strSQL = string.Format("select distinct(infoId) from StreetLine_Info_Index ");
            DataTable dtResult = mdbManager.GetTableBySQL(strSQL);
            if (dtResult == null || dtResult.Rows.Count == 0)
            {
                MessageBox.Show("当前地图没有隐藏信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            for (int i = 0; i < dtResult.Rows.Count; i++)
            {
                //a row stands for one file, text or picture
                string infoId = dtResult.Rows[i][0].ToString();
                string strGetObjetSQL = string.Format("select lineId,type from StreetLine_Info_Index where infoId='{0}' order by id asc", infoId);
                DataTable dtLineId = mdbManager.GetTableBySQL(strGetObjetSQL);
                if (dtLineId == null || dtLineId.Rows.Count == 0) continue;
                //judge file type
                int fileType = Convert.ToInt32(dtLineId.Rows[0][1].ToString());
                string strResult = "";
                for (int j = 0; j < dtLineId.Rows.Count; j++)
                {
                    string lineId = dtLineId.Rows[j][0].ToString();
                    //read hidden information
                    IQueryFilter queryFilter = new QueryFilterClass();
                    queryFilter.WhereClause = string.Format(" ID='{0}'", lineId);
                    IFeatureCursor featureCursor = mainFrame.m_FeatureClass.Search(queryFilter, false);
                    IFeature feature = featureCursor.NextFeature();
                    while (feature != null)
                    {
                        IPolyline polyLine = feature.Shape as IPolyline;
                        IPointCollection pointCollection = polyLine as IPointCollection;
                        for (int k = 0; k < pointCollection.PointCount; k++)
                        {
                            // get hidden data in X
                            string sxdata = pointCollection.get_Point(k).X.ToString().Split('.')[1].ToString();
                            if (sxdata.Length == 11)
                            {
                                if (sxdata.Substring(10, 1) == "8")//check if peak has hidden info
                                {
                                    string strTemp = sxdata.Substring(7, 3);
                                    string temp = "";
                                    if (strTemp.Substring(0, 1) == "0")
                                    {
                                        if (strTemp.Substring(1, 1) == "0")
                                        {
                                            temp = strTemp.Substring(2, 1);
                                        }
                                        else
                                        {
                                            temp = strTemp.Substring(1, 2);
                                        }
                                    }
                                    else
                                    {
                                        temp = strTemp;
                                    }
                                    strResult += temp + " ";
                                }
                            }
                            // get hidden data in Y
                            string sydata = pointCollection.get_Point(k).Y.ToString().Split('.')[1].ToString();
                            if (sydata.Length == 11)
                            {
                                if (sydata.Substring(10, 1) == "8")
                                {
                                    string strTemp = sydata.Substring(7, 3);
                                    string temp = "";
                                    if (strTemp.Substring(0, 1) == "0")
                                    {
                                        if (strTemp.Substring(1, 1) == "0")
                                        {
                                            temp = strTemp.Substring(2, 1);
                                        }
                                        else
                                        {
                                            temp = strTemp.Substring(1, 2);
                                        }
                                    }
                                    else
                                    {
                                        temp = strTemp;
                                    }
                                    strResult += temp + " ";
                                }
                            }
                            else
                            {
                                continue;
                            }
                        }
                        feature = featureCursor.NextFeature();
                    }
                }

                if (fileType == 1)//picture
                {
                    //convert decimal numbers into byte stream and save
                    string[] strImg = strResult.Trim().Split(' ');
                    byte[] bytes = new byte[strImg.Length];
                    for (int p = 0; p < strImg.Length; p++)
                    {
                        bytes[p] = Convert.ToByte(strImg[p]);
                    }
                    FileStream fs = new FileStream(textPath.Text + "\\" + i.ToString() + ".jpg", FileMode.Create, FileAccess.Write);
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Flush();
                    fs.Close();
                }
                else
                {
                    string[] strTxts = strResult.Split(' ');
                    StringBuilder sBuilder = new StringBuilder();
                    for (int m = 0; m < strTxts.Length; m++)
                    {
                        if (strTxts[m].Length > 0)
                        {
                            // convert decimal to hex
                            string strHex = Convertor.DecToHexString(Convert.ToInt32(strTxts[m]));
                            sBuilder.Append(strHex);
                        }
                    }
                    // convert hex to character, Chinese supported
                    string strContent = Convertor.UnHex(sBuilder.ToString(), "gb2312");
                    StreamWriter writer = File.CreateText(textPath.Text + "\\" + i.ToString() + ".txt");
                    writer.Write(strContent);
                    writer.Close();
                }
            }
            MessageBox.Show("提取信息成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void buttonCancelHide_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

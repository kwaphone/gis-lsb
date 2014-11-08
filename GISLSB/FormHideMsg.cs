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
    public partial class FormHideMsg : Form
    {
        /// <summary>
        /// MainFrame
        /// </summary>
        private MainFrame mainFrame;

        /// <summary>
        /// hide information type,0:text,1:picture
        /// </summary>
        private int infoType = -1;

        public FormHideMsg()
        {
            InitializeComponent();
        }

        public FormHideMsg(MainFrame mainframe)
        {
            InitializeComponent();
            setInfoType.SelectedIndex = 0;
            mainFrame = mainframe;     
        }

        private void setInfoType_SelectedIndexChanged(object sender, EventArgs e)
        {
            infoType = setInfoType.SelectedIndex;
        }

        private void buttonSetPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgOpen = new OpenFileDialog();
            dlgOpen.RestoreDirectory = true;
            dlgOpen.Multiselect = false;
            dlgOpen.Title = "选择文件";
            switch (infoType)
            {
                case 0:
                    dlgOpen.Filter = "txt files (*.txt)|*.txt";
                    break;
                case 1:
                    dlgOpen.Filter = "bmp文件(*.bmp)|*.bmp|jpg文件(*.jpg)|*.jpg|png文件(*.png)|*.png";
                    break;
            }
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                textPath.Text = dlgOpen.FileName;
            }
        }

        private void buttonCommitHide_Click(object sender, EventArgs e)
        {
            if (textPath.Text == "")
            {
                MessageBox.Show("请选择要隐藏的信息对象", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            MDBManager mdbManager = new MDBManager(mainFrame.accessPath);
            string[] formatedInfo;

            if (infoType == 0)
            {
                //read text information
                string strResult = string.Empty;
                FileStream textFileStream = new FileStream(textPath.Text, FileMode.Open, FileAccess.Read);
                StreamReader srText = new StreamReader(textFileStream, Encoding.Default);
                srText.BaseStream.Seek(0, SeekOrigin.Begin);
                string s1 = string.Empty;
                //skip empty rows
                while ((s1 = srText.ReadLine()) != null)
                {
                    strResult += s1;
                }

                if (strResult.Length == 0)
                {
                    MessageBox.Show("文本为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }

                //Convert context to decimal number
                string strTemp = Convertor.StringToHexString(strResult, Encoding.Default);
                string[] strs = strTemp.Split(' ');
                formatedInfo = new string[strs.Length];
                for (int i = 0; i < strs.Length; i++)
                {
                    //format the converted hexstring
                    //1.format number length to 3
                    //2.'8'only stands for ending of the number
                    string temp = Convert.ToInt32(strs[i], 16).ToString();
                    string temp1 = "";
                    if (temp.Length == 1)
                    {
                        temp1 = "00" + temp;
                    }
                    else if (temp.Length == 2)
                    {
                        temp1 = "0" + temp;
                    }
                    else
                    {
                        temp1 = temp;
                    }
                    formatedInfo[i] = temp1 + "8";
                }
            } 
            else
            {
                //read picture information
                byte[] bytes = System.IO.File.ReadAllBytes(textPath.Text);
                formatedInfo = new string[bytes.Length];
                for (int i = 0; i < bytes.Length; i++)
                {
                    byte by = bytes[i];
                    string temp = "0";
                    if (by.ToString() != "0")
                    {
                        temp = Convert.ToInt32(by.ToString()).ToString();
                    }
                    //format the converted hexstring
                    //1.format number length to 3
                    //2.'8'only stands for ending of the number
                    string temp1 = "";
                    if (temp.Length == 1)
                    {
                        temp1 = "00" + temp;
                    }
                    else if (temp.Length == 2)
                    {
                        temp1 = "0" + temp;
                    }
                    else
                    {
                        temp1 = temp;
                    }

                    formatedInfo[i] = temp1 + "8";
                }
            }


            string guid = Guid.NewGuid().ToString();

            //HIDE formated information with LSB
            // get all peaks in each line feature
            // for each peak, hide formatedInfo into X,Y after the decimal point 8-11
            IQueryFilter queryFilter = new QueryFilterClass();
            queryFilter.WhereClause = " flag=0";
            IFeatureCursor featureCursor = mainFrame.m_FeatureLayer.Search(queryFilter, false);
            IFeature feature = featureCursor.NextFeature();
            int count = 0;//amounts of peaks
            while (feature != null)
            {
                string guid1 = Guid.NewGuid().ToString();
                IPolyline polyLine = feature.Shape as IPolyline;
                IPointCollection pointCollection = polyLine as IPointCollection;
                IFeature newFeature = mainFrame.m_FeatureClass.CreateFeature();
                IPointCollection newPointCollection = new PolylineClass();

                for (int i = 0; i < pointCollection.PointCount; i++)
                {
                    IPoint point = pointCollection.get_Point(i);
                    IPoint newPoint = new PointClass();
                    double x = point.X;
                    double y = point.Y;

                    if (count < formatedInfo.Length)
                    {
                        string sxdata = x.ToString().Split('.')[1].Substring(0, 7);
                        sxdata += formatedInfo[count];
                        long xvalue = Convert.ToInt64(sxdata, 10);
                        double xfm = Math.Pow(10, sxdata.Length);
                        double dblxafter = xvalue / xfm;
                        double dblX = Convert.ToInt32(x.ToString().Split('.')[0]) + dblxafter;
                        newPoint.X = dblX;

                        count += 1;
                        if (count < formatedInfo.Length)
                        {
                            string sydata = y.ToString().Split('.')[1].Substring(0, 7);
                            sydata += formatedInfo[count];
                            long yvalue = Convert.ToInt64(sydata, 10);
                            double yfm = Math.Pow(10, sydata.Length);
                            double dblyafter = yvalue / yfm;
                            double dblY = Convert.ToInt32(y.ToString().Split('.')[0]) + dblyafter;
                            newPoint.Y = dblY;

                            count += 1;
                        }
                        else
                        {
                            newPoint.Y = y;
                            count += 1;
                        }
                    }
                    else
                    {
                        newPoint.X = x;
                        newPoint.Y = y;
                    }
                    object obj = Type.Missing;
                    newPointCollection.AddPoint(newPoint, ref obj, ref obj);
                }

                newFeature.Shape = newPointCollection as IGeometry;
                // hidden info flag
                newFeature.set_Value(feature.Fields.FindField("flag"), 1);
                newFeature.set_Value(feature.Fields.FindField("ID"), guid1);
                newFeature.Store();
                feature.Delete();
                //2.type stands for the file type hidden, 0:text, 1:picture
                string strSQL = string.Format("insert into StreetLine_Info_Index(infoID,lineid,type) values('{0}','{1}',{2})", guid, guid1, infoType);
                mdbManager.excute(strSQL);
                //break when all points had hidden msg
                if (count >= formatedInfo.Length)
                {
                    break;
                }
                feature = featureCursor.NextFeature();
            }
            (mainFrame.m_FeaturenWorkspace as IWorkspaceEdit).StopEditing(true);
            MessageBox.Show("隐藏信息成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

        }

        private void buttonCancelHide_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        

    }
}

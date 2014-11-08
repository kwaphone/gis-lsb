using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
    public partial class MainFrame : Form
    {
        // IWorkspaceFactory
        private IWorkspaceFactory m_AccessWorkspaceFactory = null;

        // FeaturWorkspace
        public IFeatureWorkspace m_FeaturenWorkspace = null;

        // CurrentFeatureClass
        public IFeatureClass m_FeatureClass = null;

        // MayLayer
        public IFeatureLayer m_FeatureLayer = null;

        // Path of mdb map
        public string accessPath = "";
        public MainFrame()
        {
            InitializeComponent();
        }

        private void LoadDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgOpen = new OpenFileDialog();
            dlgOpen.RestoreDirectory = true;
            dlgOpen.Title = "请选择一张地图(mdb)";
            dlgOpen.Filter = "mdb库 (*.mdb)|*.mdb";
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    //initialize gis workspace
                    m_AccessWorkspaceFactory = new AccessWorkspaceFactoryClass();
                    accessPath = dlgOpen.FileName;
                    IWorkspace workspace = m_AccessWorkspaceFactory.OpenFromFile(accessPath, 0);
                    m_FeaturenWorkspace = workspace as IFeatureWorkspace;
                    IWorkspaceEdit workspaceEdit = m_FeaturenWorkspace as IWorkspaceEdit;
                    workspaceEdit.StartEditing(true);
                    m_FeatureClass = m_FeaturenWorkspace.OpenFeatureClass("StreetLine");
                    m_FeatureLayer = new FeatureLayerClass();
                    m_FeatureLayer.FeatureClass = m_FeatureClass;
                    m_FeatureLayer.Name = m_FeatureClass.AliasName;
                    axMapControl1.Map.AddLayer(m_FeatureLayer as ILayer);
                    axMapControl1.Refresh();
                    ICommand command = new ESRI.ArcGIS.Controls.ControlsMapPanToolClass();
                    command.OnCreate(axMapControl1.Object);
                    axMapControl1.CurrentTool = command as ITool;
                }
                catch (System.Runtime.InteropServices.COMException ex)
                {
                    MessageBox.Show("此地图不包含程序可用的要素，请尝试其它地图！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                
            }
        }

        private void HideInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_FeatureLayer == null) return; // return unless map is loaded
            FormHideMsg frm_hide = new FormHideMsg(this);
            frm_hide.ShowDialog();
        }

        private void GetInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_FeatureLayer == null) return; // return unless map is loaded
            FormExtractMsg frm_extract = new FormExtractMsg(this);
            frm_extract.ShowDialog();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


    }
}
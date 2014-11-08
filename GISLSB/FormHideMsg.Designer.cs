namespace GISLSB
{
    partial class FormHideMsg
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHideMsg));
            this.setInfoType = new System.Windows.Forms.ComboBox();
            this.textPath = new System.Windows.Forms.TextBox();
            this.buttonSetPath = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonCommitHide = new System.Windows.Forms.Button();
            this.buttonCancelHide = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // setInfoType
            // 
            this.setInfoType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.setInfoType.FormattingEnabled = true;
            this.setInfoType.Items.AddRange(new object[] {
            "文本",
            "图片"});
            this.setInfoType.Location = new System.Drawing.Point(109, 25);
            this.setInfoType.Name = "setInfoType";
            this.setInfoType.Size = new System.Drawing.Size(121, 20);
            this.setInfoType.TabIndex = 0;
            this.setInfoType.SelectedIndexChanged += new System.EventHandler(this.setInfoType_SelectedIndexChanged);
            // 
            // textPath
            // 
            this.textPath.Location = new System.Drawing.Point(109, 76);
            this.textPath.Name = "textPath";
            this.textPath.Size = new System.Drawing.Size(226, 21);
            this.textPath.TabIndex = 1;
            // 
            // buttonSetPath
            // 
            this.buttonSetPath.Image = ((System.Drawing.Image)(resources.GetObject("buttonSetPath.Image")));
            this.buttonSetPath.Location = new System.Drawing.Point(341, 71);
            this.buttonSetPath.Name = "buttonSetPath";
            this.buttonSetPath.Size = new System.Drawing.Size(42, 29);
            this.buttonSetPath.TabIndex = 2;
            this.buttonSetPath.UseVisualStyleBackColor = true;
            this.buttonSetPath.Click += new System.EventHandler(this.buttonSetPath_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F);
            this.label1.Location = new System.Drawing.Point(21, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "隐藏信息类型";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F);
            this.label2.Location = new System.Drawing.Point(21, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "选择文件路径";
            // 
            // buttonCommitHide
            // 
            this.buttonCommitHide.Location = new System.Drawing.Point(123, 132);
            this.buttonCommitHide.Name = "buttonCommitHide";
            this.buttonCommitHide.Size = new System.Drawing.Size(75, 23);
            this.buttonCommitHide.TabIndex = 5;
            this.buttonCommitHide.Text = "嵌入信息";
            this.buttonCommitHide.UseVisualStyleBackColor = true;
            this.buttonCommitHide.Click += new System.EventHandler(this.buttonCommitHide_Click);
            // 
            // buttonCancelHide
            // 
            this.buttonCancelHide.Location = new System.Drawing.Point(235, 132);
            this.buttonCancelHide.Name = "buttonCancelHide";
            this.buttonCancelHide.Size = new System.Drawing.Size(75, 23);
            this.buttonCancelHide.TabIndex = 6;
            this.buttonCancelHide.Text = "取消";
            this.buttonCancelHide.UseVisualStyleBackColor = true;
            this.buttonCancelHide.Click += new System.EventHandler(this.buttonCancelHide_Click);
            // 
            // FormHideMsg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 190);
            this.ControlBox = false;
            this.Controls.Add(this.buttonCancelHide);
            this.Controls.Add(this.buttonCommitHide);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonSetPath);
            this.Controls.Add(this.textPath);
            this.Controls.Add(this.setInfoType);
            this.Name = "FormHideMsg";
            this.Text = "嵌入信息";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox setInfoType;
        private System.Windows.Forms.TextBox textPath;
        private System.Windows.Forms.Button buttonSetPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonCommitHide;
        private System.Windows.Forms.Button buttonCancelHide;
    }
}
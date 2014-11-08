namespace GISLSB
{
    partial class FormExtractMsg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormExtractMsg));
            this.buttonCancelHide = new System.Windows.Forms.Button();
            this.buttonCommitExtract = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonSetPath = new System.Windows.Forms.Button();
            this.textPath = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonCancelHide
            // 
            this.buttonCancelHide.Location = new System.Drawing.Point(247, 114);
            this.buttonCancelHide.Name = "buttonCancelHide";
            this.buttonCancelHide.Size = new System.Drawing.Size(75, 23);
            this.buttonCancelHide.TabIndex = 11;
            this.buttonCancelHide.Text = "取消";
            this.buttonCancelHide.UseVisualStyleBackColor = true;
            this.buttonCancelHide.Click += new System.EventHandler(this.buttonCancelHide_Click);
            // 
            // buttonCommitExtract
            // 
            this.buttonCommitExtract.Location = new System.Drawing.Point(135, 114);
            this.buttonCommitExtract.Name = "buttonCommitExtract";
            this.buttonCommitExtract.Size = new System.Drawing.Size(75, 23);
            this.buttonCommitExtract.TabIndex = 10;
            this.buttonCommitExtract.Text = "提取信息";
            this.buttonCommitExtract.UseVisualStyleBackColor = true;
            this.buttonCommitExtract.Click += new System.EventHandler(this.buttonCommitExtract_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F);
            this.label2.Location = new System.Drawing.Point(12, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "选择目标路径";
            // 
            // buttonSetPath
            // 
            this.buttonSetPath.Image = ((System.Drawing.Image)(resources.GetObject("buttonSetPath.Image")));
            this.buttonSetPath.Location = new System.Drawing.Point(353, 53);
            this.buttonSetPath.Name = "buttonSetPath";
            this.buttonSetPath.Size = new System.Drawing.Size(42, 29);
            this.buttonSetPath.TabIndex = 8;
            this.buttonSetPath.UseVisualStyleBackColor = true;
            this.buttonSetPath.Click += new System.EventHandler(this.buttonSetPath_Click);
            // 
            // textPath
            // 
            this.textPath.Location = new System.Drawing.Point(95, 58);
            this.textPath.Name = "textPath";
            this.textPath.Size = new System.Drawing.Size(252, 21);
            this.textPath.TabIndex = 7;
            // 
            // FormExtractMsg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 190);
            this.Controls.Add(this.buttonCancelHide);
            this.Controls.Add(this.buttonCommitExtract);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonSetPath);
            this.Controls.Add(this.textPath);
            this.Name = "FormExtractMsg";
            this.Text = "提取信息";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancelHide;
        private System.Windows.Forms.Button buttonCommitExtract;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonSetPath;
        private System.Windows.Forms.TextBox textPath;

    }
}
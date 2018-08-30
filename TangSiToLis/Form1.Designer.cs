namespace TangSiToLis
{
    partial class frmTangSiToLis
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvDaan = new System.Windows.Forms.DataGridView();
            this.lblMessage = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxOperator = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.tbxInputFile = new System.Windows.Forms.TextBox();
            this.btnInputLisFile = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tboxEndSerialNumber = new System.Windows.Forms.TextBox();
            this.tboxBeginSerialNumber = new System.Windows.Forms.TextBox();
            this.lblBeginSerialNumber = new System.Windows.Forms.Label();
            this.btnQueryLis = new System.Windows.Forms.Button();
            this.dgvLIS = new System.Windows.Forms.DataGridView();
            this.btnClose_1 = new System.Windows.Forms.Button();
            this.btnImputDaan = new System.Windows.Forms.Button();
            this.lblMessageLis = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpInputData = new System.Windows.Forms.DateTimePicker();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxYBID = new System.Windows.Forms.TextBox();
            this.dgvQueryData = new System.Windows.Forms.DataGridView();
            this.btnQueryLisLog = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpQueryLis = new System.Windows.Forms.DateTimePicker();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDaan)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLIS)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQueryData)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(0, -1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1003, 497);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvDaan);
            this.tabPage1.Controls.Add(this.lblMessage);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.tbxOperator);
            this.tabPage1.Controls.Add(this.btnClose);
            this.tabPage1.Controls.Add(this.tbxInputFile);
            this.tabPage1.Controls.Add(this.btnInputLisFile);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(995, 471);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "达瑞->Lis";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvDaan
            // 
            this.dgvDaan.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvDaan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDaan.Location = new System.Drawing.Point(19, 73);
            this.dgvDaan.Name = "dgvDaan";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.dgvDaan.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDaan.RowTemplate.Height = 23;
            this.dgvDaan.Size = new System.Drawing.Size(853, 392);
            this.dgvDaan.TabIndex = 8;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(235, 15);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(89, 12);
            this.lblMessage.TabIndex = 14;
            this.lblMessage.Text = "提示信息......";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(33, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "操作人员：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbxOperator
            // 
            this.tbxOperator.Location = new System.Drawing.Point(109, 15);
            this.tbxOperator.Name = "tbxOperator";
            this.tbxOperator.Size = new System.Drawing.Size(100, 21);
            this.tbxOperator.TabIndex = 12;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(628, 41);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click_1);
            // 
            // tbxInputFile
            // 
            this.tbxInputFile.Location = new System.Drawing.Point(107, 44);
            this.tbxInputFile.Name = "tbxInputFile";
            this.tbxInputFile.Size = new System.Drawing.Size(455, 21);
            this.tbxInputFile.TabIndex = 10;
            // 
            // btnInputLisFile
            // 
            this.btnInputLisFile.Location = new System.Drawing.Point(20, 43);
            this.btnInputLisFile.Name = "btnInputLisFile";
            this.btnInputLisFile.Size = new System.Drawing.Size(75, 23);
            this.btnInputLisFile.TabIndex = 9;
            this.btnInputLisFile.Text = "导入文件";
            this.btnInputLisFile.UseVisualStyleBackColor = true;
            this.btnInputLisFile.Click += new System.EventHandler(this.btnInputFile_Click_1);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tboxEndSerialNumber);
            this.tabPage2.Controls.Add(this.tboxBeginSerialNumber);
            this.tabPage2.Controls.Add(this.lblBeginSerialNumber);
            this.tabPage2.Controls.Add(this.btnQueryLis);
            this.tabPage2.Controls.Add(this.dgvLIS);
            this.tabPage2.Controls.Add(this.btnClose_1);
            this.tabPage2.Controls.Add(this.btnImputDaan);
            this.tabPage2.Controls.Add(this.lblMessageLis);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.dtpInputData);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(995, 471);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Lis->达瑞";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tboxEndSerialNumber
            // 
            this.tboxEndSerialNumber.ForeColor = System.Drawing.Color.Red;
            this.tboxEndSerialNumber.Location = new System.Drawing.Point(382, 34);
            this.tboxEndSerialNumber.Name = "tboxEndSerialNumber";
            this.tboxEndSerialNumber.Size = new System.Drawing.Size(307, 21);
            this.tboxEndSerialNumber.TabIndex = 10;
            this.tboxEndSerialNumber.Text = "连续编号用1-10，分开编号1,3,5。用半角逗号,分割";
            // 
            // tboxBeginSerialNumber
            // 
            this.tboxBeginSerialNumber.Location = new System.Drawing.Point(163, 34);
            this.tboxBeginSerialNumber.Name = "tboxBeginSerialNumber";
            this.tboxBeginSerialNumber.Size = new System.Drawing.Size(213, 21);
            this.tboxBeginSerialNumber.TabIndex = 9;
            this.tboxBeginSerialNumber.Text = "1806063001";
            // 
            // lblBeginSerialNumber
            // 
            this.lblBeginSerialNumber.AutoSize = true;
            this.lblBeginSerialNumber.Location = new System.Drawing.Point(163, 12);
            this.lblBeginSerialNumber.Name = "lblBeginSerialNumber";
            this.lblBeginSerialNumber.Size = new System.Drawing.Size(65, 12);
            this.lblBeginSerialNumber.TabIndex = 7;
            this.lblBeginSerialNumber.Text = "标本编号：";
            this.lblBeginSerialNumber.Click += new System.EventHandler(this.lblBeginSerialNumber_Click);
            // 
            // btnQueryLis
            // 
            this.btnQueryLis.Location = new System.Drawing.Point(695, 29);
            this.btnQueryLis.Name = "btnQueryLis";
            this.btnQueryLis.Size = new System.Drawing.Size(75, 23);
            this.btnQueryLis.TabIndex = 6;
            this.btnQueryLis.Text = "查 询";
            this.btnQueryLis.UseVisualStyleBackColor = true;
            this.btnQueryLis.Click += new System.EventHandler(this.btnQueryLis_Click);
            // 
            // dgvLIS
            // 
            this.dgvLIS.AllowUserToAddRows = false;
            this.dgvLIS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLIS.Location = new System.Drawing.Point(21, 63);
            this.dgvLIS.Name = "dgvLIS";
            this.dgvLIS.RowTemplate.Height = 23;
            this.dgvLIS.Size = new System.Drawing.Size(951, 402);
            this.dgvLIS.TabIndex = 5;
            // 
            // btnClose_1
            // 
            this.btnClose_1.Location = new System.Drawing.Point(884, 29);
            this.btnClose_1.Name = "btnClose_1";
            this.btnClose_1.Size = new System.Drawing.Size(75, 23);
            this.btnClose_1.TabIndex = 4;
            this.btnClose_1.Text = "关 闭";
            this.btnClose_1.UseVisualStyleBackColor = true;
            this.btnClose_1.Click += new System.EventHandler(this.btnClose_1_Click);
            // 
            // btnImputDaan
            // 
            this.btnImputDaan.Location = new System.Drawing.Point(776, 29);
            this.btnImputDaan.Name = "btnImputDaan";
            this.btnImputDaan.Size = new System.Drawing.Size(75, 23);
            this.btnImputDaan.TabIndex = 3;
            this.btnImputDaan.Text = "导 入";
            this.btnImputDaan.UseVisualStyleBackColor = true;
            this.btnImputDaan.Click += new System.EventHandler(this.btnImputDaan_Click);
            // 
            // lblMessageLis
            // 
            this.lblMessageLis.AutoSize = true;
            this.lblMessageLis.Location = new System.Drawing.Point(693, 9);
            this.lblMessageLis.Name = "lblMessageLis";
            this.lblMessageLis.Size = new System.Drawing.Size(65, 12);
            this.lblMessageLis.TabIndex = 2;
            this.lblMessageLis.Text = "提示信息！";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "导入LIS数据日期";
            // 
            // dtpInputData
            // 
            this.dtpInputData.CustomFormat = "yyyy-MM-dd";
            this.dtpInputData.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpInputData.Location = new System.Drawing.Point(23, 34);
            this.dtpInputData.MinDate = new System.DateTime(2010, 1, 1, 0, 0, 0, 0);
            this.dtpInputData.Name = "dtpInputData";
            this.dtpInputData.Size = new System.Drawing.Size(118, 21);
            this.dtpInputData.TabIndex = 0;
            this.dtpInputData.ValueChanged += new System.EventHandler(this.dtpInputData_ValueChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.tbxYBID);
            this.tabPage3.Controls.Add(this.dgvQueryData);
            this.tabPage3.Controls.Add(this.btnQueryLisLog);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.dtpQueryLis);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(995, 471);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "查询Lis更新";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(292, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "查询条码";
            // 
            // tbxYBID
            // 
            this.tbxYBID.Location = new System.Drawing.Point(366, 21);
            this.tbxYBID.Name = "tbxYBID";
            this.tbxYBID.Size = new System.Drawing.Size(158, 21);
            this.tbxYBID.TabIndex = 5;
            // 
            // dgvQueryData
            // 
            this.dgvQueryData.AllowUserToAddRows = false;
            this.dgvQueryData.AllowUserToDeleteRows = false;
            this.dgvQueryData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvQueryData.Location = new System.Drawing.Point(23, 63);
            this.dgvQueryData.Name = "dgvQueryData";
            this.dgvQueryData.ReadOnly = true;
            this.dgvQueryData.RowTemplate.Height = 23;
            this.dgvQueryData.Size = new System.Drawing.Size(950, 381);
            this.dgvQueryData.TabIndex = 4;
            // 
            // btnQueryLisLog
            // 
            this.btnQueryLisLog.Location = new System.Drawing.Point(582, 22);
            this.btnQueryLisLog.Name = "btnQueryLisLog";
            this.btnQueryLisLog.Size = new System.Drawing.Size(75, 23);
            this.btnQueryLisLog.TabIndex = 3;
            this.btnQueryLisLog.Text = "查 询";
            this.btnQueryLisLog.UseVisualStyleBackColor = true;
            this.btnQueryLisLog.Click += new System.EventHandler(this.btnQueryLisLog_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "查询LIS数据日期";
            // 
            // dtpQueryLis
            // 
            this.dtpQueryLis.CustomFormat = "yyyy-MM-dd";
            this.dtpQueryLis.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpQueryLis.Location = new System.Drawing.Point(145, 21);
            this.dtpQueryLis.MinDate = new System.DateTime(2010, 1, 1, 0, 0, 0, 0);
            this.dtpQueryLis.Name = "dtpQueryLis";
            this.dtpQueryLis.Size = new System.Drawing.Size(118, 21);
            this.dtpQueryLis.TabIndex = 1;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // frmTangSiToLis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1007, 518);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmTangSiToLis";
            this.Text = "达瑞唐氏导入-ELIMS-报告版";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDaan)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLIS)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQueryData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dgvDaan;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxOperator;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox tbxInputFile;
        private System.Windows.Forms.Button btnInputLisFile;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnClose_1;
        private System.Windows.Forms.Button btnImputDaan;
        private System.Windows.Forms.Label lblMessageLis;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpInputData;
        private System.Windows.Forms.DataGridView dgvLIS;
        private System.Windows.Forms.Button btnQueryLis;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dgvQueryData;
        private System.Windows.Forms.Button btnQueryLisLog;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpQueryLis;
        private System.Windows.Forms.TextBox tbxYBID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.TextBox tboxBeginSerialNumber;
        private System.Windows.Forms.Label lblBeginSerialNumber;
        private System.Windows.Forms.TextBox tboxEndSerialNumber;
    }
}


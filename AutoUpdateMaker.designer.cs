partial class AutoUpdateMaker
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
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoUpdateMaker));
        this.textBox9 = new System.Windows.Forms.TextBox();
        this.button4 = new System.Windows.Forms.Button();
        this.label9 = new System.Windows.Forms.Label();
        this.dGV1 = new System.Windows.Forms.DataGridView();
        this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.Column4 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
        this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.button1 = new System.Windows.Forms.Button();
        ((System.ComponentModel.ISupportInitialize)(this.dGV1)).BeginInit();
        this.SuspendLayout();
        // 
        // textBox9
        // 
        this.textBox9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
        | System.Windows.Forms.AnchorStyles.Right)));
        this.textBox9.Location = new System.Drawing.Point(125, 19);
        this.textBox9.Margin = new System.Windows.Forms.Padding(4);
        this.textBox9.Name = "textBox9";
        this.textBox9.ReadOnly = true;
        this.textBox9.Size = new System.Drawing.Size(496, 26);
        this.textBox9.TabIndex = 0;
        this.textBox9.Text = "D:\\Result\\";
        // 
        // button4
        // 
        this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.button4.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
        this.button4.Location = new System.Drawing.Point(763, 14);
        this.button4.Margin = new System.Windows.Forms.Padding(4);
        this.button4.Name = "button4";
        this.button4.Size = new System.Drawing.Size(113, 37);
        this.button4.TabIndex = 3;
        this.button4.Text = "创建更新";
        this.button4.UseVisualStyleBackColor = true;
        this.button4.Click += new System.EventHandler(this.button4_Click);
        // 
        // label9
        // 
        this.label9.AutoSize = true;
        this.label9.Location = new System.Drawing.Point(13, 24);
        this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        this.label9.Name = "label9";
        this.label9.Size = new System.Drawing.Size(104, 16);
        this.label9.TabIndex = 4;
        this.label9.Text = "文件夹路径：";
        // 
        // dGV1
        // 
        this.dGV1.AllowUserToAddRows = false;
        this.dGV1.AllowUserToDeleteRows = false;
        this.dGV1.AllowUserToResizeRows = false;
        dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
        dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
        this.dGV1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
        this.dGV1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
        | System.Windows.Forms.AnchorStyles.Left)
        | System.Windows.Forms.AnchorStyles.Right)));
        this.dGV1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
        this.dGV1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
        this.dGV1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
        dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
        dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
        dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
        dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        this.dGV1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
        this.dGV1.ColumnHeadersHeight = 32;
        this.dGV1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        this.dGV1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column4,
            this.Column5});
        dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
        dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
        dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
        dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
        dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
        this.dGV1.DefaultCellStyle = dataGridViewCellStyle4;
        this.dGV1.ImeMode = System.Windows.Forms.ImeMode.Off;
        this.dGV1.Location = new System.Drawing.Point(10, 62);
        this.dGV1.MultiSelect = false;
        this.dGV1.Name = "dGV1";
        this.dGV1.RowHeadersVisible = false;
        dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
        this.dGV1.RowsDefaultCellStyle = dataGridViewCellStyle5;
        this.dGV1.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
        this.dGV1.RowTemplate.Height = 26;
        this.dGV1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
        this.dGV1.Size = new System.Drawing.Size(866, 387);
        this.dGV1.TabIndex = 12;
        this.dGV1.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dGV1_ColumnHeaderMouseClick);
        // 
        // Column3
        // 
        this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
        dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        this.Column3.DefaultCellStyle = dataGridViewCellStyle3;
        this.Column3.FillWeight = 12.69036F;
        this.Column3.HeaderText = "文件";
        this.Column3.Name = "Column3";
        this.Column3.ReadOnly = true;
        this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
        // 
        // Column4
        // 
        this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
        this.Column4.FillWeight = 12.69036F;
        this.Column4.HeaderText = "全选";
        this.Column4.Name = "Column4";
        this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
        this.Column4.Width = 80;
        // 
        // Column5
        // 
        this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
        this.Column5.FillWeight = 12.69036F;
        this.Column5.HeaderText = "更新结果";
        this.Column5.Name = "Column5";
        this.Column5.ReadOnly = true;
        this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
        this.Column5.Width = 120;
        // 
        // button1
        // 
        this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.button1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
        this.button1.Location = new System.Drawing.Point(629, 14);
        this.button1.Margin = new System.Windows.Forms.Padding(4);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(111, 37);
        this.button1.TabIndex = 3;
        this.button1.Text = "查找文件";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(this.button1_Click);
        // 
        // AutoUpdateMaker
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
        this.ClientSize = new System.Drawing.Size(884, 461);
        this.Controls.Add(this.button1);
        this.Controls.Add(this.button4);
        this.Controls.Add(this.textBox9);
        this.Controls.Add(this.label9);
        this.Controls.Add(this.dGV1);
        this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.Margin = new System.Windows.Forms.Padding(4);
        this.MinimumSize = new System.Drawing.Size(600, 500);
        this.Name = "AutoUpdateMaker";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "自动更新程序";
        this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        if (APP.autoUpd)
        {
            this.MinimumSize = new System.Drawing.Size(0, 0);
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.ControlBox = false;
            this.Text = "发现新版本，等待自动更新完成";
            this.ClientSize = new System.Drawing.Size(600, 220);
            this.Size = new System.Drawing.Size(600, 220);
            this.Controls.Clear();
        }
        this.Load += new System.EventHandler(this.Form1_Load);
        ((System.ComponentModel.ISupportInitialize)(this.dGV1)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.TextBox textBox9;
    private System.Windows.Forms.Button button4;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.DataGridView dGV1;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    private System.Windows.Forms.DataGridViewCheckBoxColumn Column4;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
}


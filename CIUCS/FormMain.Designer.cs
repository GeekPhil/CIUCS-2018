namespace MasterComputer
{
    partial class FormMain
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.hWindowControl = new HalconDotNet.HWindowControl();
            this.textBoxStatusMsg = new System.Windows.Forms.TextBox();
            this.groupBoxStatusMsg = new System.Windows.Forms.GroupBox();
            this.btnCleanMsg = new System.Windows.Forms.Button();
            this.checkBoxAllowShow = new System.Windows.Forms.CheckBox();
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.QuitAPPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CalculatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NotePadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PortConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BaudRateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemBPS115200 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemBPS57600 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemBPS38400 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemBPS19200 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemBPS9600 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemBPS4800 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemBPS2400 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemBPS1200 = new System.Windows.Forms.ToolStripMenuItem();
            this.ParityBitsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PRTnoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PRToddToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PRTevenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PRTmarkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PRTspaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DataBitsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDTB8 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDTB7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDTB6 = new System.Windows.Forms.ToolStripMenuItem();
            this.StopBitsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSTP1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSTP2 = new System.Windows.Forms.ToolStripMenuItem();
            this.CameraConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenCloseCameraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutMeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBoxComNum = new System.Windows.Forms.ToolStripComboBox();
            this.DeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.XBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.XHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GSQToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SWWToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.YMQToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.YLDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TextileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AppleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LicensePlateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.QRCodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OthersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnGallery = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnTakePhoto = new System.Windows.Forms.Button();
            this.labelVersion = new System.Windows.Forms.Label();
            this.groupBoxStatusMsg.SuspendLayout();
            this.menuStripMain.SuspendLayout();
            this.statusStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // hWindowControl
            // 
            this.hWindowControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hWindowControl.BackColor = System.Drawing.Color.Black;
            this.hWindowControl.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 360);
            this.hWindowControl.Location = new System.Drawing.Point(1, 28);
            this.hWindowControl.Name = "hWindowControl";
            this.hWindowControl.Size = new System.Drawing.Size(640, 360);
            this.hWindowControl.TabIndex = 1;
            this.hWindowControl.WindowSize = new System.Drawing.Size(640, 360);
            // 
            // textBoxStatusMsg
            // 
            this.textBoxStatusMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxStatusMsg.Location = new System.Drawing.Point(6, 20);
            this.textBoxStatusMsg.Multiline = true;
            this.textBoxStatusMsg.Name = "textBoxStatusMsg";
            this.textBoxStatusMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxStatusMsg.Size = new System.Drawing.Size(615, 64);
            this.textBoxStatusMsg.TabIndex = 0;
            // 
            // groupBoxStatusMsg
            // 
            this.groupBoxStatusMsg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxStatusMsg.Controls.Add(this.btnCleanMsg);
            this.groupBoxStatusMsg.Controls.Add(this.checkBoxAllowShow);
            this.groupBoxStatusMsg.Controls.Add(this.textBoxStatusMsg);
            this.groupBoxStatusMsg.Location = new System.Drawing.Point(1, 391);
            this.groupBoxStatusMsg.Name = "groupBoxStatusMsg";
            this.groupBoxStatusMsg.Size = new System.Drawing.Size(709, 90);
            this.groupBoxStatusMsg.TabIndex = 5;
            this.groupBoxStatusMsg.TabStop = false;
            this.groupBoxStatusMsg.Text = "状态消息区";
            // 
            // btnCleanMsg
            // 
            this.btnCleanMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCleanMsg.BackColor = System.Drawing.Color.Thistle;
            this.btnCleanMsg.FlatAppearance.BorderSize = 0;
            this.btnCleanMsg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCleanMsg.Location = new System.Drawing.Point(627, 54);
            this.btnCleanMsg.Name = "btnCleanMsg";
            this.btnCleanMsg.Size = new System.Drawing.Size(75, 23);
            this.btnCleanMsg.TabIndex = 2;
            this.btnCleanMsg.Text = "清空消息";
            this.btnCleanMsg.UseVisualStyleBackColor = false;
            this.btnCleanMsg.Click += new System.EventHandler(this.btnCleanMsg_Click);
            // 
            // checkBoxAllowShow
            // 
            this.checkBoxAllowShow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxAllowShow.AutoSize = true;
            this.checkBoxAllowShow.Checked = true;
            this.checkBoxAllowShow.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAllowShow.Enabled = false;
            this.checkBoxAllowShow.Location = new System.Drawing.Point(627, 27);
            this.checkBoxAllowShow.Name = "checkBoxAllowShow";
            this.checkBoxAllowShow.Size = new System.Drawing.Size(72, 16);
            this.checkBoxAllowShow.TabIndex = 1;
            this.checkBoxAllowShow.Text = "允许显示";
            this.checkBoxAllowShow.UseVisualStyleBackColor = true;
            // 
            // menuStripMain
            // 
            this.menuStripMain.BackColor = System.Drawing.Color.White;
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.ToolsToolStripMenuItem,
            this.ConfigToolStripMenuItem,
            this.HelpToolStripMenuItem,
            this.toolStripComboBoxComNum,
            this.DeviceToolStripMenuItem,
            this.TaskToolStripMenuItem});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(715, 29);
            this.menuStripMain.TabIndex = 0;
            this.menuStripMain.Text = "menuStripMain";
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveDataToolStripMenuItem,
            this.QuitAPPToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(44, 25);
            this.FileToolStripMenuItem.Text = "文件";
            // 
            // SaveDataToolStripMenuItem
            // 
            this.SaveDataToolStripMenuItem.Name = "SaveDataToolStripMenuItem";
            this.SaveDataToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.SaveDataToolStripMenuItem.Tag = "0";
            this.SaveDataToolStripMenuItem.Text = "保存数据";
            this.SaveDataToolStripMenuItem.Click += new System.EventHandler(this.SaveDataToolStripMenuItem_Click);
            // 
            // QuitAPPToolStripMenuItem
            // 
            this.QuitAPPToolStripMenuItem.Name = "QuitAPPToolStripMenuItem";
            this.QuitAPPToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.QuitAPPToolStripMenuItem.Text = "退出";
            this.QuitAPPToolStripMenuItem.Click += new System.EventHandler(this.QuitAPPToolStripMenuItem_Click);
            // 
            // ToolsToolStripMenuItem
            // 
            this.ToolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CalculatorToolStripMenuItem,
            this.NotePadToolStripMenuItem});
            this.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem";
            this.ToolsToolStripMenuItem.Size = new System.Drawing.Size(44, 25);
            this.ToolsToolStripMenuItem.Text = "工具";
            // 
            // CalculatorToolStripMenuItem
            // 
            this.CalculatorToolStripMenuItem.Name = "CalculatorToolStripMenuItem";
            this.CalculatorToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.CalculatorToolStripMenuItem.Text = "计算器";
            this.CalculatorToolStripMenuItem.Click += new System.EventHandler(this.CalculatorToolStripMenuItem_Click);
            // 
            // NotePadToolStripMenuItem
            // 
            this.NotePadToolStripMenuItem.Name = "NotePadToolStripMenuItem";
            this.NotePadToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.NotePadToolStripMenuItem.Text = "记事本";
            this.NotePadToolStripMenuItem.Click += new System.EventHandler(this.NotePadToolStripMenuItem_Click);
            // 
            // ConfigToolStripMenuItem
            // 
            this.ConfigToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PortConfigToolStripMenuItem,
            this.CameraConfigToolStripMenuItem});
            this.ConfigToolStripMenuItem.Name = "ConfigToolStripMenuItem";
            this.ConfigToolStripMenuItem.Size = new System.Drawing.Size(44, 25);
            this.ConfigToolStripMenuItem.Text = "设置";
            // 
            // PortConfigToolStripMenuItem
            // 
            this.PortConfigToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BaudRateToolStripMenuItem,
            this.ParityBitsToolStripMenuItem,
            this.DataBitsToolStripMenuItem,
            this.StopBitsToolStripMenuItem});
            this.PortConfigToolStripMenuItem.Name = "PortConfigToolStripMenuItem";
            this.PortConfigToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.PortConfigToolStripMenuItem.Text = "端口配置";
            // 
            // BaudRateToolStripMenuItem
            // 
            this.BaudRateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemBPS115200,
            this.toolStripMenuItemBPS57600,
            this.toolStripMenuItemBPS38400,
            this.toolStripMenuItemBPS19200,
            this.toolStripMenuItemBPS9600,
            this.toolStripMenuItemBPS4800,
            this.toolStripMenuItemBPS2400,
            this.toolStripMenuItemBPS1200});
            this.BaudRateToolStripMenuItem.Name = "BaudRateToolStripMenuItem";
            this.BaudRateToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.BaudRateToolStripMenuItem.Text = "波特率";
            // 
            // toolStripMenuItemBPS115200
            // 
            this.toolStripMenuItemBPS115200.Name = "toolStripMenuItemBPS115200";
            this.toolStripMenuItemBPS115200.Size = new System.Drawing.Size(118, 22);
            this.toolStripMenuItemBPS115200.Text = "115200";
            this.toolStripMenuItemBPS115200.Click += new System.EventHandler(this.toolStripMenuItemBPS115200_Click);
            // 
            // toolStripMenuItemBPS57600
            // 
            this.toolStripMenuItemBPS57600.Name = "toolStripMenuItemBPS57600";
            this.toolStripMenuItemBPS57600.Size = new System.Drawing.Size(118, 22);
            this.toolStripMenuItemBPS57600.Text = "57600";
            this.toolStripMenuItemBPS57600.Click += new System.EventHandler(this.toolStripMenuItemBPS57600_Click);
            // 
            // toolStripMenuItemBPS38400
            // 
            this.toolStripMenuItemBPS38400.Name = "toolStripMenuItemBPS38400";
            this.toolStripMenuItemBPS38400.Size = new System.Drawing.Size(118, 22);
            this.toolStripMenuItemBPS38400.Text = "38400";
            this.toolStripMenuItemBPS38400.Click += new System.EventHandler(this.toolStripMenuItemBPS38400_Click);
            // 
            // toolStripMenuItemBPS19200
            // 
            this.toolStripMenuItemBPS19200.Name = "toolStripMenuItemBPS19200";
            this.toolStripMenuItemBPS19200.Size = new System.Drawing.Size(118, 22);
            this.toolStripMenuItemBPS19200.Text = "19200";
            this.toolStripMenuItemBPS19200.Click += new System.EventHandler(this.toolStripMenuItemBPS19200_Click);
            // 
            // toolStripMenuItemBPS9600
            // 
            this.toolStripMenuItemBPS9600.Name = "toolStripMenuItemBPS9600";
            this.toolStripMenuItemBPS9600.Size = new System.Drawing.Size(118, 22);
            this.toolStripMenuItemBPS9600.Text = "9600";
            this.toolStripMenuItemBPS9600.Click += new System.EventHandler(this.toolStripMenuItemBPS9600_Click);
            // 
            // toolStripMenuItemBPS4800
            // 
            this.toolStripMenuItemBPS4800.Name = "toolStripMenuItemBPS4800";
            this.toolStripMenuItemBPS4800.Size = new System.Drawing.Size(118, 22);
            this.toolStripMenuItemBPS4800.Text = "4800";
            this.toolStripMenuItemBPS4800.Click += new System.EventHandler(this.toolStripMenuItemBPS4800_Click);
            // 
            // toolStripMenuItemBPS2400
            // 
            this.toolStripMenuItemBPS2400.Name = "toolStripMenuItemBPS2400";
            this.toolStripMenuItemBPS2400.Size = new System.Drawing.Size(118, 22);
            this.toolStripMenuItemBPS2400.Text = "2400";
            this.toolStripMenuItemBPS2400.Click += new System.EventHandler(this.toolStripMenuItemBPS2400_Click);
            // 
            // toolStripMenuItemBPS1200
            // 
            this.toolStripMenuItemBPS1200.Name = "toolStripMenuItemBPS1200";
            this.toolStripMenuItemBPS1200.Size = new System.Drawing.Size(118, 22);
            this.toolStripMenuItemBPS1200.Text = "1200";
            this.toolStripMenuItemBPS1200.Click += new System.EventHandler(this.toolStripMenuItemBPS1200_Click);
            // 
            // ParityBitsToolStripMenuItem
            // 
            this.ParityBitsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PRTnoneToolStripMenuItem,
            this.PRToddToolStripMenuItem,
            this.PRTevenToolStripMenuItem,
            this.PRTmarkToolStripMenuItem,
            this.PRTspaceToolStripMenuItem});
            this.ParityBitsToolStripMenuItem.Name = "ParityBitsToolStripMenuItem";
            this.ParityBitsToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.ParityBitsToolStripMenuItem.Text = "校验位";
            // 
            // PRTnoneToolStripMenuItem
            // 
            this.PRTnoneToolStripMenuItem.Name = "PRTnoneToolStripMenuItem";
            this.PRTnoneToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.PRTnoneToolStripMenuItem.Tag = "0";
            this.PRTnoneToolStripMenuItem.Text = "None";
            this.PRTnoneToolStripMenuItem.Click += new System.EventHandler(this.PRTnoneToolStripMenuItem_Click);
            // 
            // PRToddToolStripMenuItem
            // 
            this.PRToddToolStripMenuItem.Name = "PRToddToolStripMenuItem";
            this.PRToddToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.PRToddToolStripMenuItem.Tag = "1";
            this.PRToddToolStripMenuItem.Text = "Odd";
            this.PRToddToolStripMenuItem.Click += new System.EventHandler(this.PRToddToolStripMenuItem_Click);
            // 
            // PRTevenToolStripMenuItem
            // 
            this.PRTevenToolStripMenuItem.Name = "PRTevenToolStripMenuItem";
            this.PRTevenToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.PRTevenToolStripMenuItem.Tag = "2";
            this.PRTevenToolStripMenuItem.Text = "Even";
            this.PRTevenToolStripMenuItem.Click += new System.EventHandler(this.PRTevenToolStripMenuItem_Click);
            // 
            // PRTmarkToolStripMenuItem
            // 
            this.PRTmarkToolStripMenuItem.Name = "PRTmarkToolStripMenuItem";
            this.PRTmarkToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.PRTmarkToolStripMenuItem.Tag = "3";
            this.PRTmarkToolStripMenuItem.Text = "Mark";
            this.PRTmarkToolStripMenuItem.Click += new System.EventHandler(this.PRTmarkToolStripMenuItem_Click);
            // 
            // PRTspaceToolStripMenuItem
            // 
            this.PRTspaceToolStripMenuItem.Name = "PRTspaceToolStripMenuItem";
            this.PRTspaceToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.PRTspaceToolStripMenuItem.Tag = "4";
            this.PRTspaceToolStripMenuItem.Text = "Space";
            this.PRTspaceToolStripMenuItem.Click += new System.EventHandler(this.PRTspaceToolStripMenuItem_Click);
            // 
            // DataBitsToolStripMenuItem
            // 
            this.DataBitsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemDTB8,
            this.toolStripMenuItemDTB7,
            this.toolStripMenuItemDTB6});
            this.DataBitsToolStripMenuItem.Name = "DataBitsToolStripMenuItem";
            this.DataBitsToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.DataBitsToolStripMenuItem.Text = "数据位";
            // 
            // toolStripMenuItemDTB8
            // 
            this.toolStripMenuItemDTB8.Name = "toolStripMenuItemDTB8";
            this.toolStripMenuItemDTB8.Size = new System.Drawing.Size(83, 22);
            this.toolStripMenuItemDTB8.Text = "8";
            this.toolStripMenuItemDTB8.Click += new System.EventHandler(this.toolStripMenuItemDTB8_Click);
            // 
            // toolStripMenuItemDTB7
            // 
            this.toolStripMenuItemDTB7.Name = "toolStripMenuItemDTB7";
            this.toolStripMenuItemDTB7.Size = new System.Drawing.Size(83, 22);
            this.toolStripMenuItemDTB7.Text = "7";
            this.toolStripMenuItemDTB7.Click += new System.EventHandler(this.toolStripMenuItemDTB7_Click);
            // 
            // toolStripMenuItemDTB6
            // 
            this.toolStripMenuItemDTB6.Name = "toolStripMenuItemDTB6";
            this.toolStripMenuItemDTB6.Size = new System.Drawing.Size(83, 22);
            this.toolStripMenuItemDTB6.Text = "6";
            this.toolStripMenuItemDTB6.Click += new System.EventHandler(this.toolStripMenuItemDTB6_Click);
            // 
            // StopBitsToolStripMenuItem
            // 
            this.StopBitsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemSTP1,
            this.toolStripMenuItemSTP2});
            this.StopBitsToolStripMenuItem.Name = "StopBitsToolStripMenuItem";
            this.StopBitsToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.StopBitsToolStripMenuItem.Text = "停止位";
            // 
            // toolStripMenuItemSTP1
            // 
            this.toolStripMenuItemSTP1.Name = "toolStripMenuItemSTP1";
            this.toolStripMenuItemSTP1.Size = new System.Drawing.Size(83, 22);
            this.toolStripMenuItemSTP1.Text = "1";
            this.toolStripMenuItemSTP1.Click += new System.EventHandler(this.toolStripMenuItemSTP1_Click);
            // 
            // toolStripMenuItemSTP2
            // 
            this.toolStripMenuItemSTP2.Name = "toolStripMenuItemSTP2";
            this.toolStripMenuItemSTP2.Size = new System.Drawing.Size(83, 22);
            this.toolStripMenuItemSTP2.Text = "2";
            this.toolStripMenuItemSTP2.Click += new System.EventHandler(this.toolStripMenuItemSTP2_Click);
            // 
            // CameraConfigToolStripMenuItem
            // 
            this.CameraConfigToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenCloseCameraToolStripMenuItem});
            this.CameraConfigToolStripMenuItem.Name = "CameraConfigToolStripMenuItem";
            this.CameraConfigToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.CameraConfigToolStripMenuItem.Text = "摄像头";
            // 
            // OpenCloseCameraToolStripMenuItem
            // 
            this.OpenCloseCameraToolStripMenuItem.Name = "OpenCloseCameraToolStripMenuItem";
            this.OpenCloseCameraToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.OpenCloseCameraToolStripMenuItem.Tag = "0";
            this.OpenCloseCameraToolStripMenuItem.Text = "打开/关闭";
            this.OpenCloseCameraToolStripMenuItem.Click += new System.EventHandler(this.OpenCloseCameraToolStripMenuItem_Click);
            // 
            // HelpToolStripMenuItem
            // 
            this.HelpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AboutMeToolStripMenuItem1});
            this.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
            this.HelpToolStripMenuItem.Size = new System.Drawing.Size(44, 25);
            this.HelpToolStripMenuItem.Text = "帮助";
            // 
            // AboutMeToolStripMenuItem1
            // 
            this.AboutMeToolStripMenuItem1.Name = "AboutMeToolStripMenuItem1";
            this.AboutMeToolStripMenuItem1.Size = new System.Drawing.Size(100, 22);
            this.AboutMeToolStripMenuItem1.Text = "关于";
            this.AboutMeToolStripMenuItem1.Click += new System.EventHandler(this.AboutMeToolStripMenuItem1_Click);
            // 
            // toolStripComboBoxComNum
            // 
            this.toolStripComboBoxComNum.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripComboBoxComNum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxComNum.Name = "toolStripComboBoxComNum";
            this.toolStripComboBoxComNum.Size = new System.Drawing.Size(75, 25);
            // 
            // DeviceToolStripMenuItem
            // 
            this.DeviceToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.DeviceToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.DeviceToolStripMenuItem.Name = "DeviceToolStripMenuItem";
            this.DeviceToolStripMenuItem.Size = new System.Drawing.Size(56, 25);
            this.DeviceToolStripMenuItem.Text = "设备：";
            this.DeviceToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.DeviceToolStripMenuItem.Click += new System.EventHandler(this.DeviceToolStripMenuItem_Click);
            // 
            // TaskToolStripMenuItem
            // 
            this.TaskToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.TaskToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.XBToolStripMenuItem,
            this.XHToolStripMenuItem,
            this.GSQToolStripMenuItem,
            this.SWWToolStripMenuItem,
            this.YMQToolStripMenuItem,
            this.YLDToolStripMenuItem,
            this.TextileToolStripMenuItem,
            this.AppleToolStripMenuItem,
            this.LicensePlateToolStripMenuItem,
            this.QRCodeToolStripMenuItem,
            this.OthersToolStripMenuItem});
            this.TaskToolStripMenuItem.Name = "TaskToolStripMenuItem";
            this.TaskToolStripMenuItem.Size = new System.Drawing.Size(68, 25);
            this.TaskToolStripMenuItem.Text = "设置任务";
            // 
            // XBToolStripMenuItem
            // 
            this.XBToolStripMenuItem.CheckOnClick = true;
            this.XBToolStripMenuItem.Name = "XBToolStripMenuItem";
            this.XBToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.XBToolStripMenuItem.Text = "雪碧";
            // 
            // XHToolStripMenuItem
            // 
            this.XHToolStripMenuItem.CheckOnClick = true;
            this.XHToolStripMenuItem.Name = "XHToolStripMenuItem";
            this.XHToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.XHToolStripMenuItem.Text = "雪花";
            // 
            // GSQToolStripMenuItem
            // 
            this.GSQToolStripMenuItem.CheckOnClick = true;
            this.GSQToolStripMenuItem.Name = "GSQToolStripMenuItem";
            this.GSQToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.GSQToolStripMenuItem.Text = "钢丝球";
            // 
            // SWWToolStripMenuItem
            // 
            this.SWWToolStripMenuItem.CheckOnClick = true;
            this.SWWToolStripMenuItem.Name = "SWWToolStripMenuItem";
            this.SWWToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.SWWToolStripMenuItem.Text = "爽歪歪";
            // 
            // YMQToolStripMenuItem
            // 
            this.YMQToolStripMenuItem.CheckOnClick = true;
            this.YMQToolStripMenuItem.Name = "YMQToolStripMenuItem";
            this.YMQToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.YMQToolStripMenuItem.Text = "羽毛球";
            // 
            // YLDToolStripMenuItem
            // 
            this.YLDToolStripMenuItem.CheckOnClick = true;
            this.YLDToolStripMenuItem.Name = "YLDToolStripMenuItem";
            this.YLDToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.YLDToolStripMenuItem.Text = "养乐多";
            // 
            // TextileToolStripMenuItem
            // 
            this.TextileToolStripMenuItem.Name = "TextileToolStripMenuItem";
            this.TextileToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.TextileToolStripMenuItem.Text = "织物";
            // 
            // AppleToolStripMenuItem
            // 
            this.AppleToolStripMenuItem.Name = "AppleToolStripMenuItem";
            this.AppleToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.AppleToolStripMenuItem.Text = "苹果";
            // 
            // LicensePlateToolStripMenuItem
            // 
            this.LicensePlateToolStripMenuItem.Name = "LicensePlateToolStripMenuItem";
            this.LicensePlateToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.LicensePlateToolStripMenuItem.Text = "车牌";
            // 
            // QRCodeToolStripMenuItem
            // 
            this.QRCodeToolStripMenuItem.Name = "QRCodeToolStripMenuItem";
            this.QRCodeToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.QRCodeToolStripMenuItem.Text = "二维码";
            // 
            // OthersToolStripMenuItem
            // 
            this.OthersToolStripMenuItem.Name = "OthersToolStripMenuItem";
            this.OthersToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.OthersToolStripMenuItem.Text = "其他";
            // 
            // statusStripMain
            // 
            this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelTime});
            this.statusStripMain.Location = new System.Drawing.Point(0, 485);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.Size = new System.Drawing.Size(715, 22);
            this.statusStripMain.TabIndex = 6;
            this.statusStripMain.Text = "statusStripMain";
            // 
            // toolStripStatusLabelTime
            // 
            this.toolStripStatusLabelTime.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabelTime.Name = "toolStripStatusLabelTime";
            this.toolStripStatusLabelTime.Size = new System.Drawing.Size(84, 17);
            this.toolStripStatusLabelTime.Text = "                   ";
            // 
            // btnGallery
            // 
            this.btnGallery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGallery.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGallery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGallery.Location = new System.Drawing.Point(646, 337);
            this.btnGallery.Name = "btnGallery";
            this.btnGallery.Size = new System.Drawing.Size(64, 48);
            this.btnGallery.TabIndex = 4;
            this.btnGallery.UseVisualStyleBackColor = true;
            this.btnGallery.Click += new System.EventHandler(this.btnGallery_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConnect.BackgroundImage = global::MasterComputer.Properties.Resources.usb_off;
            this.btnConnect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnConnect.FlatAppearance.BorderSize = 0;
            this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConnect.ForeColor = System.Drawing.Color.White;
            this.btnConnect.Location = new System.Drawing.Point(646, 32);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(64, 64);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Tag = "0";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnCom_Click);
            // 
            // btnTakePhoto
            // 
            this.btnTakePhoto.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnTakePhoto.BackgroundImage = global::MasterComputer.Properties.Resources.TakePhoto1;
            this.btnTakePhoto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnTakePhoto.FlatAppearance.BorderSize = 0;
            this.btnTakePhoto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTakePhoto.ForeColor = System.Drawing.Color.White;
            this.btnTakePhoto.Location = new System.Drawing.Point(646, 181);
            this.btnTakePhoto.Name = "btnTakePhoto";
            this.btnTakePhoto.Size = new System.Drawing.Size(64, 56);
            this.btnTakePhoto.TabIndex = 3;
            this.btnTakePhoto.Tag = "0";
            this.btnTakePhoto.UseVisualStyleBackColor = true;
            this.btnTakePhoto.Click += new System.EventHandler(this.btnTakePhoto_Click);
            // 
            // labelVersion
            // 
            this.labelVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelVersion.AutoSize = true;
            this.labelVersion.BackColor = System.Drawing.SystemColors.Control;
            this.labelVersion.Location = new System.Drawing.Point(610, 491);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(41, 12);
            this.labelVersion.TabIndex = 7;
            this.labelVersion.Text = "版本：";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(715, 507);
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.btnGallery);
            this.Controls.Add(this.statusStripMain);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.btnTakePhoto);
            this.Controls.Add(this.groupBoxStatusMsg);
            this.Controls.Add(this.hWindowControl);
            this.Controls.Add(this.menuStripMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStripMain;
            this.MinimumSize = new System.Drawing.Size(540, 380);
            this.Name = "FormMain";
            this.Text = "MasterComputer--用于商品特征识别的上位机软件";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_Closing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.groupBoxStatusMsg.ResumeLayout(false);
            this.groupBoxStatusMsg.PerformLayout();
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.statusStripMain.ResumeLayout(false);
            this.statusStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private HalconDotNet.HWindowControl hWindowControl;
        private System.Windows.Forms.Button btnTakePhoto;
        private System.Windows.Forms.TextBox textBoxStatusMsg;
        private System.Windows.Forms.GroupBox groupBoxStatusMsg;
        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem ConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PortConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CameraConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HelpToolStripMenuItem;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ToolStripMenuItem AboutMeToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem BaudRateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ParityBitsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DataBitsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem StopBitsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem QuitAPPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CalculatorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NotePadToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxComNum;
        private System.Windows.Forms.StatusStrip statusStripMain;
        private System.Windows.Forms.Button btnCleanMsg;
        private System.Windows.Forms.Button btnGallery;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemBPS115200;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemBPS57600;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemBPS38400;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemBPS19200;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemBPS9600;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemBPS4800;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemBPS2400;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemBPS1200;
        private System.Windows.Forms.ToolStripMenuItem PRTnoneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PRToddToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PRTevenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PRTmarkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PRTspaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDTB8;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDTB7;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDTB6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSTP1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSTP2;
        private System.Windows.Forms.CheckBox checkBoxAllowShow;
        private System.Windows.Forms.ToolStripMenuItem TaskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem XBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem XHToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GSQToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SWWToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem YMQToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem YLDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenCloseCameraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeviceToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelTime;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.ToolStripMenuItem TextileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AppleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LicensePlateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem QRCodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OthersToolStripMenuItem;
    }
}


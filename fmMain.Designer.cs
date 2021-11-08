
namespace OMSSigner
{
    partial class fmMain
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fmMain));
            this.trIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmiOpenWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.cmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btModuleSettings = new System.Windows.Forms.Button();
            this.btMonitor = new System.Windows.Forms.Button();
            this.btSign = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbModuleLocker = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbModule = new System.Windows.Forms.ComboBox();
            this.rtLog = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.cmTray.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // trIcon
            // 
            this.trIcon.ContextMenuStrip = this.cmTray;
            this.trIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("trIcon.Icon")));
            this.trIcon.Text = "OMS Signer";
            this.trIcon.Visible = true;
            // 
            // cmTray
            // 
            this.cmTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmiOpenWindow,
            this.toolStripMenuItem2,
            this.cmiExit});
            this.cmTray.Name = "cmTray";
            this.cmTray.Size = new System.Drawing.Size(221, 70);
            // 
            // cmiOpenWindow
            // 
            this.cmiOpenWindow.Name = "cmiOpenWindow";
            this.cmiOpenWindow.Size = new System.Drawing.Size(220, 22);
            this.cmiOpenWindow.Text = "Открыть окно программы";
            this.cmiOpenWindow.Click += new System.EventHandler(this.cmiOpenWindow_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Enabled = false;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(220, 22);
            this.toolStripMenuItem2.Text = "-----------";
            // 
            // cmiExit
            // 
            this.cmiExit.Name = "cmiExit";
            this.cmiExit.Size = new System.Drawing.Size(220, 22);
            this.cmiExit.Text = "Выход";
            this.cmiExit.Click += new System.EventHandler(this.cmiExit_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(743, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.выходToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.cmiExit_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btModuleSettings);
            this.panel1.Controls.Add(this.btMonitor);
            this.panel1.Controls.Add(this.btSign);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(743, 73);
            this.panel1.TabIndex = 5;
            // 
            // btModuleSettings
            // 
            this.btModuleSettings.Image = global::OMSSigner.Properties.Resources.settings32;
            this.btModuleSettings.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btModuleSettings.Location = new System.Drawing.Point(658, 6);
            this.btModuleSettings.Name = "btModuleSettings";
            this.btModuleSettings.Size = new System.Drawing.Size(73, 60);
            this.btModuleSettings.TabIndex = 6;
            this.btModuleSettings.Text = "Настройки модуля";
            this.btModuleSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btModuleSettings.UseVisualStyleBackColor = true;
            this.btModuleSettings.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // btMonitor
            // 
            this.btMonitor.Image = global::OMSSigner.Properties.Resources.observe_32;
            this.btMonitor.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btMonitor.Location = new System.Drawing.Point(554, 6);
            this.btMonitor.Name = "btMonitor";
            this.btMonitor.Size = new System.Drawing.Size(98, 60);
            this.btMonitor.TabIndex = 5;
            this.btMonitor.Text = "Автообработка";
            this.btMonitor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btMonitor.UseVisualStyleBackColor = true;
            this.btMonitor.Click += new System.EventHandler(this.tsMonitorToggle_Click);
            // 
            // btSign
            // 
            this.btSign.Image = global::OMSSigner.Properties.Resources.action_sign_32;
            this.btSign.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btSign.Location = new System.Drawing.Point(255, 6);
            this.btSign.Name = "btSign";
            this.btSign.Size = new System.Drawing.Size(73, 60);
            this.btSign.TabIndex = 4;
            this.btSign.Text = "Подписать";
            this.btSign.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btSign.UseVisualStyleBackColor = true;
            this.btSign.Click += new System.EventHandler(this.tsSign_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cbModuleLocker);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.cbModule);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(249, 73);
            this.panel2.TabIndex = 3;
            // 
            // cbModuleLocker
            // 
            this.cbModuleLocker.AutoSize = true;
            this.cbModuleLocker.Location = new System.Drawing.Point(12, 49);
            this.cbModuleLocker.Name = "cbModuleLocker";
            this.cbModuleLocker.Size = new System.Drawing.Size(139, 17);
            this.cbModuleLocker.TabIndex = 5;
            this.cbModuleLocker.Text = "Заблокировать выбор";
            this.cbModuleLocker.UseVisualStyleBackColor = true;
            this.cbModuleLocker.Click += new System.EventHandler(this.cbModuleLocker_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Модуль";
            // 
            // cbModule
            // 
            this.cbModule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbModule.FormattingEnabled = true;
            this.cbModule.Items.AddRange(new object[] {
            "Реестры ОМС",
            "Пакеты Согаз"});
            this.cbModule.Location = new System.Drawing.Point(12, 22);
            this.cbModule.Name = "cbModule";
            this.cbModule.Size = new System.Drawing.Size(223, 21);
            this.cbModule.TabIndex = 3;
            this.cbModule.SelectedIndexChanged += new System.EventHandler(this.cbModule_SelectedIndexChanged);
            // 
            // rtLog
            // 
            this.rtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtLog.Location = new System.Drawing.Point(0, 97);
            this.rtLog.Name = "rtLog";
            this.rtLog.ReadOnly = true;
            this.rtLog.Size = new System.Drawing.Size(743, 345);
            this.rtLog.TabIndex = 6;
            this.rtLog.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(377, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 31);
            this.button1.TabIndex = 7;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // fmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 442);
            this.Controls.Add(this.rtLog);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "fmMain";
            this.Text = "OMS Signer";
            this.MinimumSizeChanged += new System.EventHandler(this.fmMain_MinimumSizeChanged);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.fmMain_FormClosed);
            this.Load += new System.EventHandler(this.fmMain_Load);
            this.Resize += new System.EventHandler(this.fmMain_Resize);
            this.cmTray.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.NotifyIcon trIcon;
        private System.Windows.Forms.ContextMenuStrip cmTray;
        private System.Windows.Forms.ToolStripMenuItem cmiOpenWindow;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem cmiExit;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btMonitor;
        private System.Windows.Forms.Button btSign;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox cbModuleLocker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbModule;
        private System.Windows.Forms.Button btModuleSettings;
        private System.Windows.Forms.RichTextBox rtLog;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.Button button1;
    }
}


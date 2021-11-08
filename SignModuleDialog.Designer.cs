
namespace OMSSigner
{
    partial class SignModuleDialog
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
            this.components = new System.ComponentModel.Container();
            this.pRoot = new System.Windows.Forms.Panel();
            this.tlpRoot = new System.Windows.Forms.TableLayoutPanel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.dlgCancel = new System.Windows.Forms.Button();
            this.dlgApply = new System.Windows.Forms.Button();
            this.dlgOK = new System.Windows.Forms.Button();
            this.iSignModuleBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pRoot.SuspendLayout();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iSignModuleBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // pRoot
            // 
            this.pRoot.AutoSize = true;
            this.pRoot.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pRoot.Controls.Add(this.panel8);
            this.pRoot.Controls.Add(this.tlpRoot);
            this.pRoot.Dock = System.Windows.Forms.DockStyle.Top;
            this.pRoot.Location = new System.Drawing.Point(0, 0);
            this.pRoot.Name = "pRoot";
            this.pRoot.Padding = new System.Windows.Forms.Padding(5);
            this.pRoot.Size = new System.Drawing.Size(371, 565);
            this.pRoot.TabIndex = 3;
            // 
            // tlpRoot
            // 
            this.tlpRoot.AutoScroll = true;
            this.tlpRoot.ColumnCount = 1;
            this.tlpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRoot.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpRoot.Location = new System.Drawing.Point(5, 5);
            this.tlpRoot.Name = "tlpRoot";
            this.tlpRoot.RowCount = 1;
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpRoot.Size = new System.Drawing.Size(361, 516);
            this.tlpRoot.TabIndex = 0;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.dlgCancel);
            this.panel8.Controls.Add(this.dlgApply);
            this.panel8.Controls.Add(this.dlgOK);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(5, 521);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(361, 39);
            this.panel8.TabIndex = 3;
            // 
            // dlgCancel
            // 
            this.dlgCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dlgCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.dlgCancel.Location = new System.Drawing.Point(258, 3);
            this.dlgCancel.Name = "dlgCancel";
            this.dlgCancel.Size = new System.Drawing.Size(100, 28);
            this.dlgCancel.TabIndex = 2;
            this.dlgCancel.Text = "Отмена";
            this.dlgCancel.UseVisualStyleBackColor = true;
            this.dlgCancel.Click += new System.EventHandler(this.dlgCancel_Click);
            // 
            // dlgApply
            // 
            this.dlgApply.Location = new System.Drawing.Point(109, 3);
            this.dlgApply.Name = "dlgApply";
            this.dlgApply.Size = new System.Drawing.Size(100, 28);
            this.dlgApply.TabIndex = 1;
            this.dlgApply.Text = "Применить";
            this.dlgApply.UseVisualStyleBackColor = true;
            this.dlgApply.Click += new System.EventHandler(this.dlgApply_Click);
            // 
            // dlgOK
            // 
            this.dlgOK.Location = new System.Drawing.Point(3, 3);
            this.dlgOK.Name = "dlgOK";
            this.dlgOK.Size = new System.Drawing.Size(100, 28);
            this.dlgOK.TabIndex = 0;
            this.dlgOK.Text = "ОК";
            this.dlgOK.UseVisualStyleBackColor = true;
            this.dlgOK.Click += new System.EventHandler(this.dlgOK_Click);
            // 
            // iSignModuleBindingSource
            // 
            this.iSignModuleBindingSource.DataSource = typeof(OMSSigner.ISignModule);
            // 
            // SignModuleDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(371, 629);
            this.Controls.Add(this.pRoot);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MinimumSize = new System.Drawing.Size(350, 0);
            this.Name = "SignModuleDialog";
            this.Text = "Настройка модуля";
            this.pRoot.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.iSignModuleBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.BindingSource iSignModuleBindingSource;
        private System.Windows.Forms.Panel pRoot;
        private System.Windows.Forms.TableLayoutPanel tlpRoot;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Button dlgCancel;
        private System.Windows.Forms.Button dlgApply;
        private System.Windows.Forms.Button dlgOK;
    }
}
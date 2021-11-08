
namespace OMSSigner
{
    partial class PathSelectionControl
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

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbValue = new System.Windows.Forms.TextBox();
            this.btSelect = new System.Windows.Forms.Button();
            this.fbDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // tbValue
            // 
            this.tbValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbValue.Location = new System.Drawing.Point(1, 1);
            this.tbValue.Name = "tbValue";
            this.tbValue.Size = new System.Drawing.Size(512, 20);
            this.tbValue.TabIndex = 0;
            // 
            // btSelect
            // 
            this.btSelect.Dock = System.Windows.Forms.DockStyle.Right;
            this.btSelect.Location = new System.Drawing.Point(224, 0);
            this.btSelect.Name = "btSelect";
            this.btSelect.Size = new System.Drawing.Size(26, 22);
            this.btSelect.TabIndex = 1;
            this.btSelect.Text = "...";
            this.btSelect.UseVisualStyleBackColor = true;
            this.btSelect.Click += new System.EventHandler(this.btSelect_Click);
            // 
            // fbDialog
            // 
            this.fbDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // PathSelectionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btSelect);
            this.Controls.Add(this.tbValue);
            this.MaximumSize = new System.Drawing.Size(0, 22);
            this.Name = "PathSelectionControl";
            this.Size = new System.Drawing.Size(250, 22);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbValue;
        private System.Windows.Forms.Button btSelect;
        private System.Windows.Forms.FolderBrowserDialog fbDialog;
    }
}

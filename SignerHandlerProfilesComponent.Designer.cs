
namespace OMSSigner
{
    partial class SignerHandlerProfilesComponent
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
            this.tlpRoot = new System.Windows.Forms.TableLayoutPanel();
            this.pControls = new System.Windows.Forms.Panel();
            this.btAdd = new System.Windows.Forms.Button();
            this.btChange = new System.Windows.Forms.Button();
            this.btDelete = new System.Windows.Forms.Button();
            this.pControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpRoot
            // 
            this.tlpRoot.AutoSize = true;
            this.tlpRoot.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tlpRoot.ColumnCount = 1;
            this.tlpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRoot.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpRoot.Location = new System.Drawing.Point(0, 0);
            this.tlpRoot.Name = "tlpRoot";
            this.tlpRoot.RowCount = 1;
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpRoot.Size = new System.Drawing.Size(179, 2);
            this.tlpRoot.TabIndex = 0;
            // 
            // pControls
            // 
            this.pControls.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pControls.Controls.Add(this.btDelete);
            this.pControls.Controls.Add(this.btChange);
            this.pControls.Controls.Add(this.btAdd);
            this.pControls.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pControls.Location = new System.Drawing.Point(0, 108);
            this.pControls.Name = "pControls";
            this.pControls.Size = new System.Drawing.Size(179, 33);
            this.pControls.TabIndex = 1;
            // 
            // btAdd
            // 
            this.btAdd.BackgroundImage = global::OMSSigner.Properties.Resources.add_icon_32;
            this.btAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btAdd.Location = new System.Drawing.Point(3, 2);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(28, 28);
            this.btAdd.TabIndex = 0;
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // btChange
            // 
            this.btChange.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btChange.BackgroundImage = global::OMSSigner.Properties.Resources.edit_icon_32;
            this.btChange.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btChange.Location = new System.Drawing.Point(73, 2);
            this.btChange.MaximumSize = new System.Drawing.Size(28, 28);
            this.btChange.Name = "btChange";
            this.btChange.Size = new System.Drawing.Size(28, 28);
            this.btChange.TabIndex = 1;
            this.btChange.UseVisualStyleBackColor = true;
            this.btChange.Click += new System.EventHandler(this.btChange_Click);
            // 
            // btDelete
            // 
            this.btDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btDelete.BackgroundImage = global::OMSSigner.Properties.Resources.delete_icon_32;
            this.btDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btDelete.Location = new System.Drawing.Point(146, 2);
            this.btDelete.Name = "btDelete";
            this.btDelete.Size = new System.Drawing.Size(28, 28);
            this.btDelete.TabIndex = 2;
            this.btDelete.UseVisualStyleBackColor = true;
            this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
            // 
            // SignerHandlerProfilesComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.pControls);
            this.Controls.Add(this.tlpRoot);
            this.Name = "SignerHandlerProfilesComponent";
            this.Size = new System.Drawing.Size(179, 141);
            this.pControls.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpRoot;
        private System.Windows.Forms.Panel pControls;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.Button btDelete;
        private System.Windows.Forms.Button btChange;
    }
}

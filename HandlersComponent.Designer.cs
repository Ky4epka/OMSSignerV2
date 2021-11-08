
namespace OMSSigner
{
    partial class HandlersComponent
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
            this.lbEmpty = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tlpRoot
            // 
            this.tlpRoot.AutoScroll = true;
            this.tlpRoot.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tlpRoot.ColumnCount = 1;
            this.tlpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRoot.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpRoot.Location = new System.Drawing.Point(0, 0);
            this.tlpRoot.Name = "tlpRoot";
            this.tlpRoot.RowCount = 1;
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpRoot.Size = new System.Drawing.Size(159, 281);
            this.tlpRoot.TabIndex = 0;
            // 
            // lbEmpty
            // 
            this.lbEmpty.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbEmpty.Location = new System.Drawing.Point(0, 281);
            this.lbEmpty.Name = "lbEmpty";
            this.lbEmpty.Size = new System.Drawing.Size(159, 22);
            this.lbEmpty.TabIndex = 1;
            this.lbEmpty.Text = "Пусто";
            this.lbEmpty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HandlersComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lbEmpty);
            this.Controls.Add(this.tlpRoot);
            this.Name = "HandlersComponent";
            this.Size = new System.Drawing.Size(159, 379);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpRoot;
        private System.Windows.Forms.Label lbEmpty;
    }
}

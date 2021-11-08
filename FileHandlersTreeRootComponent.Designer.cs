
namespace OMSSigner
{
    partial class FileHandlersTreeRootComponent
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
            this.tvRoot = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // tvRoot
            // 
            this.tvRoot.Location = new System.Drawing.Point(3, 3);
            this.tvRoot.Name = "tvRoot";
            this.tvRoot.Size = new System.Drawing.Size(227, 209);
            this.tvRoot.TabIndex = 0;
            // 
            // FileHandlersTreeRootComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.tvRoot);
            this.Name = "FileHandlersTreeRootComponent";
            this.Size = new System.Drawing.Size(233, 229);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvRoot;
    }
}

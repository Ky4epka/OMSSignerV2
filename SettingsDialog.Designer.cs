
namespace OMSSigner
{
    partial class SettingsDialog
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbHandlerCont = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel10 = new System.Windows.Forms.Panel();
            this.btHandlerRemove = new System.Windows.Forms.Button();
            this.btHandlerAdd = new System.Windows.Forms.Button();
            this.valDeleteFileAfterSign = new System.Windows.Forms.CheckBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.btPFTSEDelete = new System.Windows.Forms.Button();
            this.btPFTSEChange = new System.Windows.Forms.Button();
            this.btPFTSEAdd = new System.Windows.Forms.Button();
            this.tbPacketFilesToSignExtensionsEdit = new System.Windows.Forms.TextBox();
            this.valPacketFilesToSignExtensions = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.valJoinSignatures = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btSignaturesProfilesDelete = new System.Windows.Forms.Button();
            this.btSignaturesProfilesChange = new System.Windows.Forms.Button();
            this.btSignaturesProfilesAdd = new System.Windows.Forms.Button();
            this.tbSignaturesProfilesEdit = new System.Windows.Forms.TextBox();
            this.valSignaturesProfiles = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.valHandlerName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.valHandlerType = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.btvalHandledPacketsRepositorySelector = new System.Windows.Forms.Button();
            this.valHandledPacketsRepository = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btOutPathSelector = new System.Windows.Forms.Button();
            this.valOutPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btInPathSelector = new System.Windows.Forms.Button();
            this.valInPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.dlgCancel = new System.Windows.Forms.Button();
            this.dlgApply = new System.Windows.Forms.Button();
            this.dlgOK = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.panel1.SuspendLayout();
            this.gbHandlerCont.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gbHandlerCont);
            this.panel1.Controls.Add(this.panel9);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(10, 10);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(454, 691);
            this.panel1.TabIndex = 0;
            // 
            // gbHandlerCont
            // 
            this.gbHandlerCont.Controls.Add(this.groupBox2);
            this.gbHandlerCont.Controls.Add(this.valDeleteFileAfterSign);
            this.gbHandlerCont.Controls.Add(this.panel6);
            this.gbHandlerCont.Controls.Add(this.panel4);
            this.gbHandlerCont.Controls.Add(this.valHandlerName);
            this.gbHandlerCont.Controls.Add(this.label9);
            this.gbHandlerCont.Controls.Add(this.valHandlerType);
            this.gbHandlerCont.Controls.Add(this.label8);
            this.gbHandlerCont.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbHandlerCont.Location = new System.Drawing.Point(5, 125);
            this.gbHandlerCont.Name = "gbHandlerCont";
            this.gbHandlerCont.Padding = new System.Windows.Forms.Padding(3, 7, 3, 3);
            this.gbHandlerCont.Size = new System.Drawing.Size(444, 561);
            this.gbHandlerCont.TabIndex = 13;
            this.gbHandlerCont.TabStop = false;
            this.gbHandlerCont.Text = "Настройки обработчиков";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel10);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(3, 489);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(438, 65);
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Управление профилями";
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.btHandlerRemove);
            this.panel10.Controls.Add(this.btHandlerAdd);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel10.Location = new System.Drawing.Point(3, 16);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(432, 41);
            this.panel10.TabIndex = 18;
            // 
            // btHandlerRemove
            // 
            this.btHandlerRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btHandlerRemove.Location = new System.Drawing.Point(332, 6);
            this.btHandlerRemove.Name = "btHandlerRemove";
            this.btHandlerRemove.Size = new System.Drawing.Size(96, 30);
            this.btHandlerRemove.TabIndex = 2;
            this.btHandlerRemove.Text = "Удалить";
            this.btHandlerRemove.UseVisualStyleBackColor = true;
            this.btHandlerRemove.Click += new System.EventHandler(this.btHandlerRemove_Click);
            // 
            // btHandlerAdd
            // 
            this.btHandlerAdd.Location = new System.Drawing.Point(3, 6);
            this.btHandlerAdd.Name = "btHandlerAdd";
            this.btHandlerAdd.Size = new System.Drawing.Size(96, 30);
            this.btHandlerAdd.TabIndex = 0;
            this.btHandlerAdd.Text = "Добавить";
            this.btHandlerAdd.UseVisualStyleBackColor = true;
            this.btHandlerAdd.Click += new System.EventHandler(this.btHandlerAdd_Click);
            // 
            // valDeleteFileAfterSign
            // 
            this.valDeleteFileAfterSign.Dock = System.Windows.Forms.DockStyle.Top;
            this.valDeleteFileAfterSign.Location = new System.Drawing.Point(3, 452);
            this.valDeleteFileAfterSign.Name = "valDeleteFileAfterSign";
            this.valDeleteFileAfterSign.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.valDeleteFileAfterSign.Size = new System.Drawing.Size(438, 37);
            this.valDeleteFileAfterSign.TabIndex = 27;
            this.valDeleteFileAfterSign.Text = "Удалять файлы после подписи";
            this.valDeleteFileAfterSign.UseVisualStyleBackColor = true;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.label5);
            this.panel6.Controls.Add(this.panel7);
            this.panel6.Controls.Add(this.tbPacketFilesToSignExtensionsEdit);
            this.panel6.Controls.Add(this.valPacketFilesToSignExtensions);
            this.panel6.Controls.Add(this.label6);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(3, 279);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(5);
            this.panel6.Size = new System.Drawing.Size(438, 173);
            this.panel6.TabIndex = 25;
            // 
            // label5
            // 
            this.label5.CausesValidation = false;
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(5, 148);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(428, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Вводить только название расширения без точки и без маски";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.btPFTSEDelete);
            this.panel7.Controls.Add(this.btPFTSEChange);
            this.panel7.Controls.Add(this.btPFTSEAdd);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(5, 107);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(428, 41);
            this.panel7.TabIndex = 7;
            // 
            // btPFTSEDelete
            // 
            this.btPFTSEDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btPFTSEDelete.Location = new System.Drawing.Point(328, 6);
            this.btPFTSEDelete.Name = "btPFTSEDelete";
            this.btPFTSEDelete.Size = new System.Drawing.Size(96, 30);
            this.btPFTSEDelete.TabIndex = 2;
            this.btPFTSEDelete.Text = "Удалить";
            this.btPFTSEDelete.UseVisualStyleBackColor = true;
            this.btPFTSEDelete.Click += new System.EventHandler(this.btPFTSEDelete_Click);
            // 
            // btPFTSEChange
            // 
            this.btPFTSEChange.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btPFTSEChange.Location = new System.Drawing.Point(105, 6);
            this.btPFTSEChange.Name = "btPFTSEChange";
            this.btPFTSEChange.Size = new System.Drawing.Size(217, 30);
            this.btPFTSEChange.TabIndex = 1;
            this.btPFTSEChange.Text = "Изменить";
            this.btPFTSEChange.UseVisualStyleBackColor = true;
            this.btPFTSEChange.Click += new System.EventHandler(this.btPFTSEChange_Click);
            // 
            // btPFTSEAdd
            // 
            this.btPFTSEAdd.Location = new System.Drawing.Point(3, 6);
            this.btPFTSEAdd.Name = "btPFTSEAdd";
            this.btPFTSEAdd.Size = new System.Drawing.Size(96, 30);
            this.btPFTSEAdd.TabIndex = 0;
            this.btPFTSEAdd.Text = "Добавить";
            this.btPFTSEAdd.UseVisualStyleBackColor = true;
            this.btPFTSEAdd.Click += new System.EventHandler(this.btPFTSEAdd_Click);
            // 
            // tbPacketFilesToSignExtensionsEdit
            // 
            this.tbPacketFilesToSignExtensionsEdit.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbPacketFilesToSignExtensionsEdit.Location = new System.Drawing.Point(5, 87);
            this.tbPacketFilesToSignExtensionsEdit.Name = "tbPacketFilesToSignExtensionsEdit";
            this.tbPacketFilesToSignExtensionsEdit.Size = new System.Drawing.Size(428, 20);
            this.tbPacketFilesToSignExtensionsEdit.TabIndex = 6;
            // 
            // valPacketFilesToSignExtensions
            // 
            this.valPacketFilesToSignExtensions.Dock = System.Windows.Forms.DockStyle.Top;
            this.valPacketFilesToSignExtensions.FormattingEnabled = true;
            this.valPacketFilesToSignExtensions.Location = new System.Drawing.Point(5, 18);
            this.valPacketFilesToSignExtensions.Name = "valPacketFilesToSignExtensions";
            this.valPacketFilesToSignExtensions.Size = new System.Drawing.Size(428, 69);
            this.valPacketFilesToSignExtensions.TabIndex = 4;
            this.valPacketFilesToSignExtensions.SelectedIndexChanged += new System.EventHandler(this.valPacketFilesToSignExtensions_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Location = new System.Drawing.Point(5, 5);
            this.label6.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(428, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Расширения файлов в пакете для подписи руководителем и главбухом";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.valJoinSignatures);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Controls.Add(this.tbSignaturesProfilesEdit);
            this.panel4.Controls.Add(this.valSignaturesProfiles);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(3, 87);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(5);
            this.panel4.Size = new System.Drawing.Size(438, 192);
            this.panel4.TabIndex = 24;
            // 
            // valJoinSignatures
            // 
            this.valJoinSignatures.Dock = System.Windows.Forms.DockStyle.Top;
            this.valJoinSignatures.Location = new System.Drawing.Point(5, 165);
            this.valJoinSignatures.Name = "valJoinSignatures";
            this.valJoinSignatures.Size = new System.Drawing.Size(428, 24);
            this.valJoinSignatures.TabIndex = 28;
            this.valJoinSignatures.Text = "Обьединить подписи";
            this.valJoinSignatures.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.CausesValidation = false;
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(5, 148);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(428, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "Профили подписей АРМ настраиваются в оснастке КриптоАРМ";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btSignaturesProfilesDelete);
            this.panel5.Controls.Add(this.btSignaturesProfilesChange);
            this.panel5.Controls.Add(this.btSignaturesProfilesAdd);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(5, 107);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(428, 41);
            this.panel5.TabIndex = 7;
            // 
            // btSignaturesProfilesDelete
            // 
            this.btSignaturesProfilesDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btSignaturesProfilesDelete.Location = new System.Drawing.Point(328, 6);
            this.btSignaturesProfilesDelete.Name = "btSignaturesProfilesDelete";
            this.btSignaturesProfilesDelete.Size = new System.Drawing.Size(96, 30);
            this.btSignaturesProfilesDelete.TabIndex = 2;
            this.btSignaturesProfilesDelete.Text = "Удалить";
            this.btSignaturesProfilesDelete.UseVisualStyleBackColor = true;
            this.btSignaturesProfilesDelete.Click += new System.EventHandler(this.btSignaturesProfilesDelete_Click);
            // 
            // btSignaturesProfilesChange
            // 
            this.btSignaturesProfilesChange.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btSignaturesProfilesChange.Location = new System.Drawing.Point(105, 6);
            this.btSignaturesProfilesChange.Name = "btSignaturesProfilesChange";
            this.btSignaturesProfilesChange.Size = new System.Drawing.Size(217, 30);
            this.btSignaturesProfilesChange.TabIndex = 1;
            this.btSignaturesProfilesChange.Text = "Изменить";
            this.btSignaturesProfilesChange.UseVisualStyleBackColor = true;
            this.btSignaturesProfilesChange.Click += new System.EventHandler(this.btSignaturesProfilesChange_Click);
            // 
            // btSignaturesProfilesAdd
            // 
            this.btSignaturesProfilesAdd.Location = new System.Drawing.Point(3, 6);
            this.btSignaturesProfilesAdd.Name = "btSignaturesProfilesAdd";
            this.btSignaturesProfilesAdd.Size = new System.Drawing.Size(96, 30);
            this.btSignaturesProfilesAdd.TabIndex = 0;
            this.btSignaturesProfilesAdd.Text = "Добавить";
            this.btSignaturesProfilesAdd.UseVisualStyleBackColor = true;
            this.btSignaturesProfilesAdd.Click += new System.EventHandler(this.btSignaturesProfilesAdd_Click);
            // 
            // tbSignaturesProfilesEdit
            // 
            this.tbSignaturesProfilesEdit.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbSignaturesProfilesEdit.Location = new System.Drawing.Point(5, 87);
            this.tbSignaturesProfilesEdit.Name = "tbSignaturesProfilesEdit";
            this.tbSignaturesProfilesEdit.Size = new System.Drawing.Size(428, 20);
            this.tbSignaturesProfilesEdit.TabIndex = 6;
            // 
            // valSignaturesProfiles
            // 
            this.valSignaturesProfiles.Dock = System.Windows.Forms.DockStyle.Top;
            this.valSignaturesProfiles.FormattingEnabled = true;
            this.valSignaturesProfiles.Location = new System.Drawing.Point(5, 18);
            this.valSignaturesProfiles.Name = "valSignaturesProfiles";
            this.valSignaturesProfiles.Size = new System.Drawing.Size(428, 69);
            this.valSignaturesProfiles.TabIndex = 4;
            this.valSignaturesProfiles.SelectedIndexChanged += new System.EventHandler(this.valSignaturesProfiles_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(5, 5);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(428, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Профили подписей АРМ";
            // 
            // valHandlerName
            // 
            this.valHandlerName.Dock = System.Windows.Forms.DockStyle.Top;
            this.valHandlerName.Location = new System.Drawing.Point(3, 67);
            this.valHandlerName.Name = "valHandlerName";
            this.valHandlerName.Size = new System.Drawing.Size(438, 20);
            this.valHandlerName.TabIndex = 21;
            // 
            // label9
            // 
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.Location = new System.Drawing.Point(3, 54);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(438, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "Имя обработчика";
            // 
            // valHandlerType
            // 
            this.valHandlerType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.valHandlerType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.valHandlerType.Dock = System.Windows.Forms.DockStyle.Top;
            this.valHandlerType.FormattingEnabled = true;
            this.valHandlerType.Location = new System.Drawing.Point(3, 33);
            this.valHandlerType.Name = "valHandlerType";
            this.valHandlerType.Size = new System.Drawing.Size(438, 21);
            this.valHandlerType.TabIndex = 14;
            this.valHandlerType.SelectedIndexChanged += new System.EventHandler(this.valHandlerType_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Location = new System.Drawing.Point(3, 20);
            this.label8.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(438, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "Вид обработчика";
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.btvalHandledPacketsRepositorySelector);
            this.panel9.Controls.Add(this.valHandledPacketsRepository);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel9.Location = new System.Drawing.Point(5, 98);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(444, 27);
            this.panel9.TabIndex = 9;
            // 
            // btvalHandledPacketsRepositorySelector
            // 
            this.btvalHandledPacketsRepositorySelector.Dock = System.Windows.Forms.DockStyle.Right;
            this.btvalHandledPacketsRepositorySelector.Location = new System.Drawing.Point(419, 0);
            this.btvalHandledPacketsRepositorySelector.Name = "btvalHandledPacketsRepositorySelector";
            this.btvalHandledPacketsRepositorySelector.Size = new System.Drawing.Size(25, 27);
            this.btvalHandledPacketsRepositorySelector.TabIndex = 1;
            this.btvalHandledPacketsRepositorySelector.Text = "...";
            this.btvalHandledPacketsRepositorySelector.UseVisualStyleBackColor = true;
            this.btvalHandledPacketsRepositorySelector.Click += new System.EventHandler(this.btvalHandledPacketsRepositorySelector_Click);
            // 
            // valHandledPacketsRepository
            // 
            this.valHandledPacketsRepository.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.valHandledPacketsRepository.Location = new System.Drawing.Point(3, 3);
            this.valHandledPacketsRepository.Name = "valHandledPacketsRepository";
            this.valHandledPacketsRepository.Size = new System.Drawing.Size(410, 20);
            this.valHandledPacketsRepository.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.Location = new System.Drawing.Point(5, 85);
            this.label7.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(444, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Папка для хранения обработанных пакетов";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btOutPathSelector);
            this.panel3.Controls.Add(this.valOutPath);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(5, 58);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(444, 27);
            this.panel3.TabIndex = 3;
            // 
            // btOutPathSelector
            // 
            this.btOutPathSelector.Dock = System.Windows.Forms.DockStyle.Right;
            this.btOutPathSelector.Location = new System.Drawing.Point(419, 0);
            this.btOutPathSelector.Name = "btOutPathSelector";
            this.btOutPathSelector.Size = new System.Drawing.Size(25, 27);
            this.btOutPathSelector.TabIndex = 1;
            this.btOutPathSelector.Text = "...";
            this.btOutPathSelector.UseVisualStyleBackColor = true;
            this.btOutPathSelector.Click += new System.EventHandler(this.btOutPathSelector_Click);
            // 
            // valOutPath
            // 
            this.valOutPath.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.valOutPath.Location = new System.Drawing.Point(3, 3);
            this.valOutPath.Name = "valOutPath";
            this.valOutPath.Size = new System.Drawing.Size(410, 20);
            this.valOutPath.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(5, 45);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(444, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Папка выходных файлов для подписи";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btInPathSelector);
            this.panel2.Controls.Add(this.valInPath);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(5, 18);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(444, 27);
            this.panel2.TabIndex = 1;
            // 
            // btInPathSelector
            // 
            this.btInPathSelector.Dock = System.Windows.Forms.DockStyle.Right;
            this.btInPathSelector.Location = new System.Drawing.Point(419, 0);
            this.btInPathSelector.Name = "btInPathSelector";
            this.btInPathSelector.Size = new System.Drawing.Size(25, 27);
            this.btInPathSelector.TabIndex = 1;
            this.btInPathSelector.Text = "...";
            this.btInPathSelector.UseVisualStyleBackColor = true;
            this.btInPathSelector.Click += new System.EventHandler(this.btInPathSelector_Click);
            // 
            // valInPath
            // 
            this.valInPath.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.valInPath.Location = new System.Drawing.Point(3, 3);
            this.valInPath.Name = "valInPath";
            this.valInPath.Size = new System.Drawing.Size(410, 20);
            this.valInPath.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(444, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Папка входных файлов для подписи";
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.dlgCancel);
            this.panel8.Controls.Add(this.dlgApply);
            this.panel8.Controls.Add(this.dlgOK);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel8.Location = new System.Drawing.Point(10, 703);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(454, 35);
            this.panel8.TabIndex = 1;
            // 
            // dlgCancel
            // 
            this.dlgCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dlgCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.dlgCancel.Location = new System.Drawing.Point(351, 3);
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
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.Description = "Выберите директорию";
            this.folderBrowserDialog.SelectedPath = "%USERPROFILE%\\Documents";
            // 
            // SettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.dlgCancel;
            this.ClientSize = new System.Drawing.Size(474, 748);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SettingsDialog";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Настройки";
            this.Shown += new System.EventHandler(this.SettingsDialog_Shown);
            this.VisibleChanged += new System.EventHandler(this.SettingsDialog_VisibleChanged);
            this.panel1.ResumeLayout(false);
            this.gbHandlerCont.ResumeLayout(false);
            this.gbHandlerCont.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btOutPathSelector;
        private System.Windows.Forms.TextBox valOutPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btInPathSelector;
        private System.Windows.Forms.TextBox valInPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Button dlgCancel;
        private System.Windows.Forms.Button dlgApply;
        private System.Windows.Forms.Button dlgOK;
        public System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Button btvalHandledPacketsRepositorySelector;
        private System.Windows.Forms.TextBox valHandledPacketsRepository;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox gbHandlerCont;
        private System.Windows.Forms.ComboBox valHandlerType;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button btPFTSEDelete;
        private System.Windows.Forms.Button btPFTSEChange;
        private System.Windows.Forms.Button btPFTSEAdd;
        private System.Windows.Forms.TextBox tbPacketFilesToSignExtensionsEdit;
        private System.Windows.Forms.ListBox valPacketFilesToSignExtensions;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btSignaturesProfilesDelete;
        private System.Windows.Forms.Button btSignaturesProfilesChange;
        private System.Windows.Forms.Button btSignaturesProfilesAdd;
        private System.Windows.Forms.TextBox tbSignaturesProfilesEdit;
        private System.Windows.Forms.ListBox valSignaturesProfiles;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox valHandlerName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Button btHandlerRemove;
        private System.Windows.Forms.Button btHandlerAdd;
        private System.Windows.Forms.CheckBox valDeleteFileAfterSign;
        private System.Windows.Forms.CheckBox valJoinSignatures;
    }
}
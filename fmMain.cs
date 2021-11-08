using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLog;
using Microsoft.Toolkit.Uwp.Notifications;
using System.IO;
using Windows.UI.Notifications;

namespace OMSSigner
{
    public partial class fmMain : Form
    {
        protected StringBuilder UILogSwapBuffer = new StringBuilder(Globals.UI_LOG_BUFFER_LEN);
        protected StringBuilder UILogBuffer = new StringBuilder(Globals.UI_LOG_BUFFER_LEN);
        protected static ILogger cLogger = null;
        protected FormWindowState PrevWindowState;
        protected ISignModule iCurrentModule = null;
        protected bool iUIUpdating = false;

        protected static ILogger Logger
        {
            get
            {
                if (cLogger == null) cLogger = LogManager.GetCurrentClassLogger();

                return cLogger;
            }
        }

        public void UILogApplyBuffer()
        {
            rtLog.Text = UILogBuffer.ToString();
        }

        public void UILog(string message)
        {
            if (message.Length >= UILogBuffer.Capacity)
            {
                UILogBuffer.Remove(0, message.Length);
            }

            UILogBuffer.AppendLine(message);
            UILogApplyBuffer();
        }

        public void UILogClear()
        {
            UILogBuffer.Clear();
        }

        public void UpdateUI()
        {
            iUIUpdating = true;
            try
            {
                bool has_curModule = CurrentModule != null;
                bool lock_moduleSelector = cbModuleLocker.Checked;

                cbModule.Enabled = !lock_moduleSelector;
                btSign.Enabled = has_curModule && !CurrentModule.MonitorActivity;
                btMonitor.Enabled = has_curModule;

                if (has_curModule)
                {
                    if (CurrentModule.MonitorActivity)
                    {
                        btMonitor.Text = "Остановить";
                        btMonitor.Image = Properties.Resources.not_observe_32;
                    }
                    else
                    {
                        btMonitor.Text = "Автообработка";
                        btMonitor.Image = Properties.Resources.observe_32;
                    }
                }

                cbModule.Items.Clear();
                foreach (ISignModule module in ProgramConfig.Instance.ModuleManager.Modules)
                {
                    cbModule.Items.Add(module.Name);
                }

                cbModule.SelectedItem = ProgramConfig.Instance.ModuleName;
                btModuleSettings.Enabled = has_curModule;
            }
            finally
            {
                iUIUpdating = false;
            }
        }

        public bool IsInTray
        {
            get
            {
                return !ShowInTaskbar && !Visible;
            }
            set
            {
                ShowInTaskbar = !value;

                if (value)
                    WindowState = FormWindowState.Minimized;
                else
                    WindowState = FormWindowState.Normal;
            }
        }

        public void PushNotify(string title, string body)
        {
            if (title == "")
                title = Text;
            else
                title = Text + " - " + title;

            ToastContentBuilder cb = new ToastContentBuilder();
                cb.AddText(Text + " - " + title)
                .AddText(body)
                .Show();
        }


        public ISignModule CurrentModule
        {
            get
            {
                return ProgramConfig.Instance.ModuleManager.GetModule(cbModule.Text);
            }
        }

        public fmMain()
        {
            InitializeComponent();
            Log.LogHandlers += (string message, LogLevel level) => { Logger.Log(level, message); };
            Log.LogHandlers += (string message, LogLevel level) => { UILog(message); };
            LoadConfig();

            if (ProgramConfig.Instance.ModuleManager.GetModule(typeof(OMSRegistrSigner)) == null)
            {
                ProgramConfig.Instance.ModuleManager.AddModule(new OMSRegistrSigner());
            }

            if (ProgramConfig.Instance.ModuleManager.GetModule(typeof(TargetOrganizationSignerModule)) == null)
            {
                ProgramConfig.Instance.ModuleManager.AddModule(new TargetOrganizationSignerModule());
            }

            UpdateUI();
            // For test
            return;
            SignModule smodule = new SignModule();
            smodule.Name = "HEllo ";
            smodule.InPath = "ToHell";

            IFileHandler handler = new SignerHandler();
            handler.Name = "123";
            smodule.AddHandler(handler);

//            ProgramConfig.Instance.Modules = new ISignModule[1] { smodule };
            ProgramConfig.SaveConfigToFile(Globals.CONFIG_FILE, ProgramConfig.Instance);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (CurrentModule != null)
            {
                SignModuleDialog dlg = new SignModuleDialog(CurrentModule);
                dlg.ShowDialog(this);
            }
        }

        private void tsSign_Click(object sender, EventArgs e)
        {
            CurrentModule?.Sign();
        }

        public void PushProgramConfig(ProgramConfig config)
        {
            cbModule.Text = config.ModuleName;
            cbModuleLocker.Checked = config.LockModuleSelect;

            foreach (SignModule module in ProgramConfig.Instance.ModuleManager.Modules)
            {
                BindableString pb_string = new BindableString("pb_title");
                BindableProgressBarValue pb_value = new BindableProgressBarValue("pb_progressValue");
                BindableString pb_valueProgressString = new BindableString("pb_progressValueString");
                BindableString pb_valueStatusString = new BindableString("pb_statusString");

                module.OnSignStart +=
                    (ISignModule sender, int fileCount) =>
                    {
                        ToastContentBuilder cb = new ToastContentBuilder();
                        ToastContent tc = cb.AddText(Text + " - " + sender.Name)
                        .AddVisualChild(new AdaptiveProgressBar()
                        {
                            Title = pb_string,
                            Value = pb_value,
                            ValueStringOverride = pb_valueProgressString,
                            Status = pb_valueStatusString
                        })
                        .GetToastContent();

                        ToastNotification s = new ToastNotification(tc.GetXml());
                        s.Tag = "pb_progress";
                        s.Group = "pb_fileProgress";
                        s.Data = new NotificationData();
                        s.Data.Values["pb_title"] = Text + " - " + sender.Name;
                        s.Data.Values["pb_progressValue"] = "0";
                        s.Data.Values["pb_progressValueString"] = "";
                        s.Data.Values["pb_statusString"] = "";
                        s.Data.SequenceNumber = 1;
                        ToastNotificationManager.CreateToastNotifier().Show(s);
                    };
                module.OnSignProgress += 
                    (ISignModule sender, int current, int count, float progress, string fileName) =>
                    {
                        NotificationData data = new NotificationData() { SequenceNumber = 2 };

                        data.Values["pb_progressValue"] = progress.ToString();
                        data.Values["pb_progressValueString"] = "Файл " + current.ToString() + "/" + count.ToString();
                        data.Values["pb_statusString"] = "Обработка: " + Path.GetFileNameWithoutExtension(fileName) + "...";

                        ToastNotificationManager.CreateToastNotifier().Update(data, "pb_progress", "pb_fileProgress");
                    };
            }
        }

        public void DropProgramConfig(ProgramConfig config)
        {
            config.ModuleName = cbModule.Text;
            config.LockModuleSelect = cbModuleLocker.Checked;
        }

        public void LoadConfig()
        {
            ProgramConfig.LoadConfigFromFile(Globals.CONFIG_FILE, ProgramConfig.Instance);
            PushProgramConfig(ProgramConfig.Instance);
            UpdateUI();
        }

        public void SaveConfig()
        {
            DropProgramConfig(ProgramConfig.Instance);
            ProgramConfig.SaveConfigToFile(Globals.CONFIG_FILE, ProgramConfig.Instance);
            UpdateUI();
        }

        private void tsMonitorToggle_Click(object sender, EventArgs e)
        {
            if (CurrentModule != null)
                CurrentModule.MonitorActivity = !CurrentModule.MonitorActivity;

            UpdateUI();
        }

        private void cmiExit_Click(object sender, EventArgs e)
        {
            if (CurrentModule != null)
                CurrentModule.MonitorActivity = false;
            Close();
        }

        private void cmiOpenWindow_Click(object sender, EventArgs e)
        {
            IsInTray = false;
            Activate();
        }

        private void Event_OnSignModuleMonitorStateChagned(ISignModule sender)
        {
            if (iUIUpdating) return;

            if (sender.Equals(CurrentModule))
                UpdateUI();
        }

        private void fmMain_MinimumSizeChanged(object sender, EventArgs e)
        {
        }


        private void fmMain_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                IsInTray = true;
            }
        }

        private void fmMain_Load(object sender, EventArgs e)
        {

        }

        private void cbModuleLocker_CheckedChanged(object sender, EventArgs e)
        {
            if (iUIUpdating) return;

            UpdateUI();
        }

        private void fmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveConfig();
        }

        private void cbModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (iUIUpdating) return;

            int index = cbModule.SelectedIndex;

            if (index != -1)
                ProgramConfig.Instance.ModuleName = cbModule.SelectedItem as string;
            else
                ProgramConfig.Instance.ModuleName = "";

            UpdateUI();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }
    }
}

using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;
using NLog;
using SevenZip;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

namespace OMSSigner
{


    public interface IARMProfile: IAssignable, ICloneable
    {
        string ProfileName { get; set; }
        string ProfileTitle { get; set; }
    }

    [XmlType]
    [Serializable]
    public class ARMProfile : IARMProfile
    {
        [UIBinder(UIClass = typeof(TextBox),
            bindedPropertyName = "Text",
            bindedPropertyType = typeof(string),
            useLabel = true,
            LabelText = "Имя профиля")]
        [XmlElement]
        public string ProfileName { get; set; }
        [UIBinder(UIClass = typeof(TextBox),
            bindedPropertyName = "Text",
            bindedPropertyType = typeof(string),
            useLabel = true,
            LabelText = "Примечание")]
        [XmlElement]
        public string ProfileTitle { get; set; }

        public void Assign(IAssignable source)
        {
            ARMProfile profile = source as ARMProfile;
            ProfileName = profile.ProfileName;
            ProfileTitle = profile.ProfileTitle;
        }

        public object Clone()
        {
            ARMProfile result = new ARMProfile();
            result.Assign(this);
            return result;
        }
    }

    [XmlType]
    [XmlInclude(typeof(FileHandlerBase))]
    [Serializable]
    public class SignerHandler: FileHandlerBase
    {
        [UIBinder(UIClass = typeof(TextBox),
            bindedPropertyName = "Text",
            bindedPropertyType = typeof(string),
            useLabel = true,
            LabelText = "Расширение файла подписи")]
        [XmlElement]
        public virtual string SignatureExtension { get; set; }
        [UIBinder(UIClass = typeof(CheckBox),
            bindedPropertyName = "Checked",
            bindedPropertyType = typeof(bool),
            useLabel = false,
            Control_Text = "Объединять подписи файла")]
        [XmlElement]
        public virtual bool JoinSignaturesProfiles { get; set; }
        [UIBinder(UIClass = typeof(CheckBox),
            bindedPropertyName = "Checked",
            bindedPropertyType = typeof(bool),
            useLabel = false,
            Control_Text = "Удалять исходный файл после подписи")]
        [XmlElement]
        public virtual bool DeleteFileAfterSign { get; set; }

        [UIBinder(UIClass = typeof(SignerHandlerProfilesComponent),
            bindedPropertyName = "Profiles",
            bindedPropertyType = typeof(IARMProfile),
            useLabel = true,
            LabelText = "АРМ-профили подписей")]
        [XmlIgnore]
        public virtual IARMProfile[] Profiles
        {
            get => iProfiles.ToArray();
            set
            {
                iProfiles.Clear();
                iProfiles.AddRange(value);
            }
        }

        [XmlArray("ProfilesWrapper")]
        [XmlArrayItem("Item")]
        public virtual XmlAnything<IARMProfile>[] ArmProfilesWrapper
        {
            get => XmlAnything<IARMProfile>.WrapFromArray(Profiles);
            set => Profiles = XmlAnything<IARMProfile>.WrapToArray(value);
        }

        protected List<IARMProfile> iProfiles = new List<IARMProfile>();

        public override void Assign(IAssignable source)
        {
            base.Assign(source);
            SignerHandler handler = source as SignerHandler;
            Profiles = handler.Profiles;
            SignatureExtension = handler.SignatureExtension;
            JoinSignaturesProfiles = handler.JoinSignaturesProfiles;
            DeleteFileAfterSign = handler.DeleteFileAfterSign;
        }

        public SignerHandler() : base()
        {

        }

        public SignerHandler(IFileHandler owner) : base(owner)
        {

        }

        public override object Clone()
        {
            return base.Clone();
        }

        public override void ToDefault()
        {
            base.ToDefault();
        }

        public virtual string SignatureFileNameHandler(string signatureFileName)
        {
            return signatureFileName;
        }

        public override bool Handle(string fileName, string outputDir)
        {
            if (!base.Handle(fileName, outputDir)) return false;

            string ext = ExtractFileExtWithoutPoint(fileName);
            bool allowed_ext = ContainsFileToSignExt(ext);

            string inputFileName = Path.GetFileName(fileName);
            string inputFileNameNoExt = Path.GetFileNameWithoutExtension(inputFileName);
            string dest_file = outputDir + "\\" + inputFileName;

            if (allowed_ext)
            {
                foreach (ARMProfile profile in Profiles)
                {
                    SignFile(fileName, outputDir, profile.ProfileName, JoinSignaturesProfiles);
                }

                string src_sign_file = outputDir + "\\" + inputFileName + ".sig";
                if (File.Exists(src_sign_file) && (!SignatureExtension.ToLower().Equals(".sig")))
                {
                    File.Move(src_sign_file, outputDir + "\\" + SignatureFileNameHandler(inputFileName) + "." + ext + "." + SignatureExtension);
                }
            }

            if (!DeleteFileAfterSign && (!fileName.ToUpper().Equals(dest_file.ToUpper())))
                File.Copy(fileName, dest_file, true);

            return !allowed_ext;
        }

        public static string ExtractFileExtWithoutPoint(string FilePath)
        {
            string extension = Path.GetExtension(FilePath);

            if (extension.Length > 0)
            {
                if (extension[0] == '.')
                    extension = extension.Remove(0, 1);
            }
            else
            {
                return "";
            }

            return extension;
        }

        public static bool MatchFileExtension(string extension, string[] extensions)
        {
            extension = extension.ToLower();

            foreach (string ext in extensions)
            {
                if (extension.Equals(ext.ToLower())) return true;
            }

            return false;
        }

        public static void ExecProcess(string exec_file, string[] param, int wait_timeout_ms)
        {
            string param_str = "";

            foreach (string p in param)
            {
                param_str += " " + p;
            }

            Process s = Process.Start(exec_file, param_str);
            s.WaitForExit(wait_timeout_ms);
        }

        public static void MakeTestFile(string fileName)
        {
            File.WriteAllText(fileName, "Test");
        }

        public void SignFile(string InputSigningFile, string OutputSignFile, string SignProfileName, bool joinSignatures)
        {
            string inputFileName = Path.GetFileName(InputSigningFile);
            string inputFilePath = InputSigningFile;
            string outputFileDir = OutputSignFile;
            string profileName = SignProfileName;
            string opName = "s";
            string signed_file = OutputSignFile + "\\" + inputFileName + ".sig";

            if (File.Exists(signed_file) && joinSignatures)
            {
                inputFilePath = signed_file;
                outputFileDir = OutputSignFile;
                profileName = SignProfileName;
                opName = "a";
            }

            Log._("Подпись файла \"" + inputFileName + "\" профилем \"" + SignProfileName + "\"", LogLevel.Info);
            ExecProcess(Globals.TDHELPER_SCRIPT, new string[4] {
            "\"" + inputFilePath + "\"",
            "\"" + outputFileDir + "\"",
            "\"" + profileName + "\"",
            opName},
            10000);
        }
    }

    [XmlType]
    [XmlInclude(typeof(SignerHandler))]
    [Serializable]
    public class PacketHandlerBase: SignerHandler
    {
        public OutArchiveFormat OutputArchiveFormat { get; set; }
        [UIBinder(UIClass = typeof(TextBox),
            bindedPropertyName = "Text",
            bindedPropertyType = typeof(string),
            useLabel = true,
            LabelText = "Расширение итогового пакета")]
        [XmlElement]
        public string PacketOutputExtension { get; set; }
        [UIBinder(UIClass = typeof(CheckBox),
            bindedPropertyName = "Checked",
            bindedPropertyType = typeof(string),
            useLabel = false,
            Control_Text = "Подписывать пакет")]
        [XmlElement]
        public bool SignPacket { get; set; }

        public override string TempPathRelative { get => GetType().Name; set => base.TempPathRelative = value; }

        [XmlIgnore]
        public string TempRoot { get => TempPathAbsolute; }
        [XmlIgnore]
        public string TempPacketSource { get => TempRoot + "\\packet_source"; }
        [XmlIgnore]
        public string TempPacketBuild { get => TempRoot + "\\packet_build"; }
        [XmlIgnore]
        public string TempPacketOutputBuild { get => TempRoot + "\\packet_output_build"; }

        /// <summary>
        /// Шаблон имени обрабатываемого файла по регулярному выражению
        /// </summary>
        [XmlElement]
        public string InputFileNameTemplate { get; set; }



        public PacketHandlerBase() : base()
        {

        }

        public PacketHandlerBase(IFileHandler owner) : base(owner)
        {
            
        }

        public override void Assign(IAssignable source)
        {
            base.Assign(source);

            PacketHandlerBase handler = source as PacketHandlerBase;
            OutputArchiveFormat = handler.OutputArchiveFormat;
            PacketOutputExtension = handler.PacketOutputExtension;
            SignPacket = handler.SignPacket;
        }

        public override object Clone()
        {
            PacketHandlerBase handler = base.Clone() as PacketHandlerBase;
            handler.Assign(this);
            return handler;
        }

        public override void ToDefault()
        {
            base.ToDefault();
        }

        public override bool Handle(string fileName, string outputDir)
        {
            string ext = ExtractFileExtWithoutPoint(fileName);

            if (!ContainsFileToSignExt(ext)) return true;

            ClearTempPath();
            ProvideDirectory(TempRoot);
            ProvideDirectory(TempPacketSource);
            ProvideDirectory(TempPacketBuild);
            ProvideDirectory(TempPacketOutputBuild);
            string packetFileName = Path.GetFileName(fileName);
            string packetFileNameNoExt = Path.GetFileNameWithoutExtension(fileName);

            Log._("Извлечение файлов из пакета...", LogLevel.Info);

            using (SevenZipExtractor SevenExtr = new SevenZipExtractor(fileName))
            {
                SevenExtr.ExtractArchive(TempPacketSource);
            }

            Log._("Подписывание файлов...", LogLevel.Info);
            SignDirEx(TempPacketSource, TempPacketBuild);

            Log._("Упаковка файлов в OMS-архив...", LogLevel.Info);
            SevenZipCompressor SevenCompr = new SevenZipCompressor();

            SevenCompr.ArchiveFormat = OutputArchiveFormat;

            string outputPacketFile = TempPacketOutputBuild + "\\" + Path.GetFileName(fileName);
            SevenCompr.CompressDirectory(TempPacketBuild, outputPacketFile, true);

            Log._("Подпись пакета: " + outputPacketFile, LogLevel.Info);
            try
            {
                base.Handle(outputPacketFile, TempPacketOutputBuild);
            }
            catch (Exception e)
            {
                Log._(e, "Не удалось подписать пакет по причине: ");
            }

            SevenCompr.CompressDirectory(TempPacketOutputBuild, outputDir + "\\" + packetFileNameNoExt + "." +  PacketOutputExtension, true);
            ClearTempPath();
            return false;
        }

        public static void ProvideDirectory(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public void ClearTempPath()
        {
            try
            {
                Log._("Очистка временной директории...", LogLevel.Info);
                if (Directory.Exists(TempRoot))
                    Directory.Delete(TempRoot, true);
            }
            catch (Exception e)
            {
                Log._(e, "Не удалось очистить временную директорию по причине: ");
            }
        }


        public void SignDirEx(string InputSigningDir, string OutputSignDir)
        {
            foreach (string file in Directory.EnumerateFiles(InputSigningDir))
            {
                try
                {
                    foreach (IFileHandler handler in Handlers)
                    {
                        if (handler.Equals(this)) continue;

                        if (!handler.Handle(file, OutputSignDir)) break;
                    }    
                }
                catch (Exception e)
                {
                    Log._(e, "Не удалось подписать файл \"" + file + "\" по причине: ");
                }
            }
        }



    }

    public struct SignModuleSignCompleteData
    {
        public int TotalCount;
        public int SuccessfullCount;
        public int NotHandledCount;
        public int ErrorCount;
    }

    [XmlType]
    [XmlInclude(typeof(SignModuleBase))]
    [Serializable]
    public class SignModule: SignModuleBase
    {
        public event Action<ISignModule> OnMonitorStarted;
        public event Action<ISignModule> OnMonitorCancelled;
        public event Action<ISignModule> OnMonitorFinished;

        public event Action<ISignModule, int> OnSignStart;
        /// <summary>
        /// [ISignModule] sender
        /// [int] current
        /// [int] count
        /// [string] fileName
        /// </summary>
        public event Action<ISignModule, int, int, float, string> OnSignProgress;
        public event Action<ISignModule, SignModuleSignCompleteData> OnSignCompleted;

        public override bool MonitorActivity 
        { 
            get => iMonitorActivity; 
            set
            {
                if (value == MonitorActivity) return;

                try
                {
                    if ((iMonitorTask != null) && (!iMonitorTask.IsCanceled))
                    {
                        iMonitorTokenSource.Cancel();
                        iMonitorTask = null;
                    }

                    if (value)
                    {
                        iMonitorTokenSource = new CancellationTokenSource();
                        iMonitorToken = iMonitorTokenSource.Token;
                        iMonitorTask = MonitorHandler(iMonitorToken);
                    }

                }
                catch (Exception e)
                {
                    Log._(e, "Не удалось запустить задачу мониторинга из-за ошибки: ");
                }

                iMonitorActivity = value;
            }
        }

        protected bool iMonitorActivity = false;
        protected CancellationTokenSource iMonitorTokenSource = null;
        protected CancellationToken iMonitorToken = default;
        protected Task iMonitorTask = null;

        public override void ToDefault()
        {
            base.ToDefault();
            Globals.DefaultSignModule(this);
        }

        public override void Sign()
        {
            SignModuleSignCompleteData signEventData = new SignModuleSignCompleteData();
            base.Sign();
            if (!Directory.Exists(InPath))
            {
                Log._("Путь ко входной директории не найден: " + InPath, LogLevel.Fatal);
                return;
            }

            if (!Directory.Exists(OutPath))
            {
                Directory.CreateDirectory(OutPath);
                Log._("Создана отсутствующая выходная директория: " + OutPath, LogLevel.Info);
            }

            int packet_count = 0;

            foreach (string file in Directory.EnumerateFiles(InPath, "*.*", SearchOption.AllDirectories))
            {
                packet_count++;
            }

            signEventData.TotalCount = packet_count;

            if (packet_count == 0)
            {
                if (!MonitorActivity)
                {
                    Log._("Обрабатывать нечего...", LogLevel.Info);
                }

                if (OnSignStart != null)
                    OnSignStart.Invoke(this, packet_count);
            }

            int counter = 0;
            foreach (string file in Directory.EnumerateFiles(InPath, "*.*", SearchOption.AllDirectories))
            {
                counter++;
                string moving_file = HandledRepositoryPath + "\\" + Path.GetFileName(file);

                try
                {
                    Log._("Начата обработка пакета(" + counter + " из " + packet_count + ")..", LogLevel.Info);
                    bool handled = false;

                    if (OnSignProgress != null)
                        OnSignProgress.Invoke(this, counter, packet_count, counter / packet_count, file);
                    
                    foreach (IFileHandler handler in iHandlers)
                    {
                        if (!handler.Handle(file, OutPath))
                        {
                            handled = true;
                            break;
                        }
                    }

                    if (handled)
                    {
                        if (File.Exists(moving_file))
                            File.Delete(moving_file);

                        signEventData.SuccessfullCount++;
                        Log._(@"Пакет обработан успешно..", LogLevel.Info);
                    }
                    else
                    {
                        signEventData.NotHandledCount++;
                        Log._(@"Файл пропущен: не найден подходящий обработчик", LogLevel.Info);
                    }
                }
                catch (Exception e)
                {
                    signEventData.ErrorCount++;
                    Log._(e, "Не удалось обработать пакет \"" + Path.GetFileName(file) + "\" из-за ошибки: ");
                }
                finally
                {
                    try
                    {
                        File.Move(file, moving_file);
                    }
                    catch (Exception e)
                    {
                        Log._(e, "Не удалось переместить пакет \"" + Path.GetFileName(file) + "\" в обработанные по причине: ");
                    }

                    Log._(@"Обработка пакета завершена..", LogLevel.Info);
                }

            }

            if ((packet_count > 0) && (OnSignCompleted != null))
            {
                OnSignCompleted.Invoke(this, signEventData);
            }
        }

        public async Task MonitorHandler(CancellationToken token)
        {
            if (OnMonitorStarted != null) OnMonitorStarted.Invoke(this);

            Log._("Мониторинг '" + Name + "' запущен", LogLevel.Info);
            try
            {
                token.ThrowIfCancellationRequested();

                while (true)
                {
                    try
                    {
                        await Task.Delay(Globals.MONITOR_INTERVAL);
                        Sign();
                    }
                    catch (Exception e)
                    {
                        Log._(e, "Не удалось провести подписание по причине: ");
                    }
                    finally
                    {
                        if (token.IsCancellationRequested) token.ThrowIfCancellationRequested();
                    }
                }
            }
            catch (OperationCanceledException)
            {
                if (OnMonitorCancelled != null) OnMonitorCancelled.Invoke(this);

                Log._("Мониторинг '" + Name + "' отменен.", LogLevel.Info);
            }
            catch (Exception e)
            {
                Log._(e, "Произошла ошибка мониторинга: ");
            }
            finally
            {
                if (OnMonitorFinished != null) OnMonitorFinished.Invoke(this);
                Log._("Мониторинг '" + Name + "' завершен", LogLevel.Info);
            }
        }

    }


}

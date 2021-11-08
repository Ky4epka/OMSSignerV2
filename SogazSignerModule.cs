using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;
using SevenZip;
using System.Windows.Forms;
using System.IO;
using NLog;

namespace OMSSigner
{

    [Serializable]
    [XmlType]
    [XmlInclude(typeof(SignerHandler))]
    public class TargetOrganizationPacketFileHandler : SignerHandler
    {
        public override string Name
        {
            get => "Обработчик пакетов целевых организаций";
        }

        public OutArchiveFormat OutputArchiveFormat { get; set; }
        [UIBinder(UIClass = typeof(TextBox),
            bindedPropertyName = "Text",
            bindedPropertyType = typeof(string),
            useLabel = true,
            LabelText = "Расширение итогового пакета")]
        [XmlElement]
        public string PacketOutputExtension { get; set; }

        [XmlIgnore]
        public override string TempPathRelative { get => GetType().Name; set => base.TempPathRelative = value; }

        [UIBinder(UIClass = typeof(TextBox),
            bindedPropertyName = "Text",
            bindedPropertyType = typeof(string),
            useLabel = true,
            LabelText = "Код МО")]
        [XmlElement]
        public string MOCode{ get; set; }

        [UIBinder(UIClass = typeof(TextBox),
            bindedPropertyName = "Text",
            bindedPropertyType = typeof(string),
            useLabel = true,
            LabelText = "Код целевой организации")]
        [XmlElement]
        public string TargetOrganizationCode { get; set; }
        [UIBinder(UIClass = typeof(TextBox),
            bindedPropertyName = "Text",
            bindedPropertyType = typeof(string),
            useLabel = true,
            LabelText = "Шаблон имени входного пакета")]
        [XmlElement]
        public string InputPacketFileNameTemplate { get; set; }
        [UIBinder(UIClass = typeof(TextBox),
            bindedPropertyName = "Text",
            bindedPropertyType = typeof(string),
            useLabel = true,
            LabelText = "Шаблон кода пакета в имени входящего пакета\n(Использует регулярные выражения)")]
        [XmlElement]
        public string PacketCodeTemplate { get; set; }
        [UIBinder(UIClass = typeof(TextBox),
            bindedPropertyName = "Text",
            bindedPropertyType = typeof(string),
            useLabel = true,
            LabelText = "Шаблон имени исходящего пакета")]
        [XmlElement]
        public string OutputPacketFileNameTemplate { get; set; }


        [UIBinder(IgnoreInheritedAttr = true)]
        public override string SignatureExtension { get => base.SignatureExtension; set => base.SignatureExtension = value; }


        [UIBinder(IgnoreInheritedAttr = true)]
        public override string[] HandledExtensions { get => base.HandledExtensions; set => base.HandledExtensions = value; }


        [UIBinder(IgnoreInheritedAttr = true)]
        public override bool JoinSignaturesProfiles { get => base.JoinSignaturesProfiles; set => base.JoinSignaturesProfiles = value; }

        [UIBinder(IgnoreInheritedAttr = true)]
        public override bool DeleteFileAfterSign { get => base.DeleteFileAfterSign; set => base.DeleteFileAfterSign = value; }

        /*
        [UIBinder(IgnoreInheritedAttr = true)]
        //[XmlIgnore]
        public override IARMProfile[] Profiles 
        { 
            get => base.Profiles; 
            set => base.Profiles = value; 
        }

        [XmlArray("ProfilesWrapper")]
        [XmlArrayItem("Item")]
        public override XmlAnything<IARMProfile>[] ArmProfilesWrapper
        {
            get => base.ArmProfilesWrapper;
            set => base.ArmProfilesWrapper = value;
        }*/

        [XmlIgnore]
        public string TempRoot { get => TempPathAbsolute; }
        [XmlIgnore]
        public string TempPacketSource { get => TempRoot + "\\packet_source"; }
        [XmlIgnore]
        public string TempPacketBuild { get => TempRoot + "\\packet_build"; }

        public static string MOOrgCodeValue = "{MOCode}";
        public static string TargetOrgCodeValue = "{TargetCode}";
        public static string PacketCodeValue = "{PacketCode}";
        //[sS][0-9]+[mM][0-9]+_([0-9_]+).*

        [XmlElement]
        public bool SignPacket { get; set; }


        public TargetOrganizationPacketFileHandler() : base()
        {

        }

        public TargetOrganizationPacketFileHandler(IFileHandler owner) : base(owner)
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
            string packetFileName = Path.GetFileName(fileName);
            string packetFileNameNoExt = Path.GetFileNameWithoutExtension(fileName);
            string ext = ExtractFileExtWithoutPoint(fileName);
            Match code_match = Regex.Match(fileName, PacketCodeTemplate);
            string packetCode = (code_match?.Groups.Count >= 2) ? (code_match.Groups[1].Value) : "";

            bool expr1 = !ContainsFileToSignExt(ext);
            bool expr2 = !packetFileNameNoExt.ToLower().Equals(BuildOrgFileName(InputPacketFileNameTemplate, MOCode, TargetOrganizationCode, packetCode).ToLower());

            if (expr1 || 
                expr2) return true;

            ClearTempPath();
            ProvideDirectory(TempRoot);
            ProvideDirectory(TempPacketSource);
            ProvideDirectory(TempPacketBuild);

            Log._("Извлечение файлов из архива" + packetFileNameNoExt + "...", LogLevel.Info);

            using (SevenZipExtractor SevenExtr = new SevenZipExtractor(fileName))
            {
                SevenExtr.ExtractArchive(TempPacketSource);
            }

            Log._("Подписывание файлов...", LogLevel.Info);
            foreach (string file in Directory.EnumerateFiles(TempPacketSource))
            {
                try
                {
                    foreach (IFileHandler handler in Handlers)
                    {
                        if (handler is TargetOrganizationSignerFileHandler)
                        {
                            (handler as TargetOrganizationSignerFileHandler).TransferData = 
                                new TargetOrganizationSignerFileHandlerTransferData(
                                    MOCode, TargetOrganizationCode, packetCode, OutputPacketFileNameTemplate); ;
                        }

                        if (!handler.Handle(file, TempPacketBuild)) break;
                    }
                }
                catch (Exception e)
                {
                    Log._(e, "Не удалось подписать файл \"" + file + "\" по причине: ");
                }
            }

            Log._("Упаковка файлов в OMS-архив...", LogLevel.Info);
            SevenZipCompressor SevenCompr = new SevenZipCompressor();
            string outputPacketFileName = outputDir + "\\" + BuildOrgFileName(OutputPacketFileNameTemplate, MOCode, TargetOrganizationCode, packetCode) + "." + PacketOutputExtension;

            SevenCompr.ArchiveFormat = OutputArchiveFormat;
            SevenCompr.CompressDirectory(TempPacketBuild, outputPacketFileName, true);
            ClearTempPath();
            return false;
        }

        public static string BuildOrgFileName(string fileNameTemplate, string inputOrgCode, string outputOrgCode, string packetCode)
        {
            return ReplaceByValueMap(fileNameTemplate, new Dictionary<string, object> 
                { 
                    { MOOrgCodeValue, inputOrgCode },
                    { TargetOrgCodeValue, outputOrgCode },
                    { PacketCodeValue, packetCode }
                }); ;
        }

        public static string ReplaceByValueMap(string input, Dictionary<string, object> value_map)
        {
            string result = input;

            foreach (KeyValuePair<string, object> pair in value_map)
            {
                result = result.Replace(pair.Key, pair.Value.ToString());
            }

            return result;
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
        }
    }

    public struct TargetOrganizationSignerFileHandlerTransferData
    {
        public string MOCode;
        public string TargetCode;
        public string PacketCode;
        public string TargetFileNameTemplate;

        public TargetOrganizationSignerFileHandlerTransferData(string mo_code, string target_code, string packet_code, string target_file_name_template)
        {
            MOCode = mo_code;
            TargetCode = target_code;
            PacketCode = packet_code;
            TargetFileNameTemplate = target_file_name_template;
        }

        public void Zero()
        {
            MOCode = "";
            TargetCode = "";
            PacketCode = "";
        }
    }

    [Serializable]
    [XmlType]
    [XmlInclude(typeof(SignerHandler))]
    public class TargetOrganizationSignerFileHandler : SignerHandler
    {
        public override string Name { get => "Обработчик файлов"; }
        public TargetOrganizationSignerFileHandlerTransferData TransferData { get; set; }



        public TargetOrganizationSignerFileHandler() : base()
        {

        }

        public TargetOrganizationSignerFileHandler(IFileHandler owner) : base(owner)
        {

        }

        public override string SignatureFileNameHandler(string signatureFileName)
        {
            return TargetOrganizationPacketFileHandler.BuildOrgFileName(TransferData.TargetFileNameTemplate, TransferData.MOCode, TransferData.TargetCode, TransferData.PacketCode);
        }
    }

    [Serializable]
    [XmlType]
    [XmlInclude(typeof(SignModule))]
    public class TargetOrganizationSignerModule : SignModule
    {
        public override string Name
        {
            get => "Подпись пакетов целевых организаций";
        }

        protected TargetOrganizationPacketFileHandler iPacketHandler = null;
        protected TargetOrganizationSignerFileHandler iSignerHandler = null;

        public TargetOrganizationSignerModule() : base()
        {
            iPacketHandler = new TargetOrganizationPacketFileHandler(this);
            iSignerHandler = new TargetOrganizationSignerFileHandler(iPacketHandler);
        }
    }
}

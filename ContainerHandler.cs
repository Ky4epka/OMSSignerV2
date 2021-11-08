using System;
using System.IO;
using SevenZip;
using NLog;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace OMSSigner
{

    [XmlType]
    [XmlInclude(typeof(SignerHandler))]
    [Serializable]
    public class ContainerHandler : SignerHandler
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

        public override string TempPathRelative { get => GetType().Name; set => base.TempPathRelative = value; }

        [XmlIgnore]
        public string TempRoot { get => TempPathAbsolute; }
        [XmlIgnore]
        public string TempPacketSource { get => TempRoot + "\\packet_source"; }
        [XmlIgnore]
        public string TempPacketBuild { get => TempRoot + "\\packet_build"; }


        [XmlElement]
        public bool SignPacket { get; set; }


        public ContainerHandler() : base()
        {

        }

        public ContainerHandler(IFileHandler owner) : base(owner)
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
            string packetFileName = Path.GetFileName(fileName);
            string packetFileNameNoExt = Path.GetFileNameWithoutExtension(fileName);

            Log._("Извлечение файлов из архива" + packetFileNameNoExt + "...", LogLevel.Info);

            using (SevenZipExtractor SevenExtr = new SevenZipExtractor(fileName))
            {
                SevenExtr.ExtractArchive(TempPacketSource);
            }

            Log._("Подписывание файлов...", LogLevel.Info);
            SignDirEx(TempPacketSource, TempPacketBuild);

            Log._("Упаковка файлов в OMS-архив...", LogLevel.Info);
            SevenZipCompressor SevenCompr = new SevenZipCompressor();
            string outputPacketFileName = outputDir + "\\" + packetFileNameNoExt + "." + PacketOutputExtension;

            SevenCompr.ArchiveFormat = OutputArchiveFormat;
            SevenCompr.CompressDirectory(TempPacketBuild, outputPacketFileName, true);


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
}



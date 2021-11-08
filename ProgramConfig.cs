using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using NLog;
using System.Reflection;
using System.Diagnostics;

namespace OMSSigner
{

    public class Globals
    {
        public static string CONFIG_FILE { get => ExecutePath("settings.cfg"); }
        public static string TDHELPER_SCRIPT { get => ExecutePath("tdhelper.vbs"); }
        public static string SEVENZIP_EXEC { get => ExecutePath("7z.exe"); }
        public static string TEMP_WORKING_DIR { 
            get
            { 
                string dir = "C:\\Temp\\OMSSigner\\Temp";

                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

                return dir;
            }
        }
        public static string TEMP_PACKET_BUILD_DIR2 {
            get 
            { 
                string dir = TEMP_WORKING_DIR + "\\Build";

                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

                return dir;
            }
        }
        public static string TEMP_OUTPUT_PACKET_BUILD_DIR2
        {
            get
            {
                string dir = TEMP_WORKING_DIR + "\\OMSBuild";

                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

                return dir;
            }
        }

        public static string TEMP_PACKET_SOURCE_DIR2
        {
            get
            {
                string dir = TEMP_WORKING_DIR + "\\Source";

                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

                return dir;
            }
        }

        public static int MONITOR_INTERVAL = 1000;

        public static string OMS_PACKET_EXT = ".oms";
        public static int UI_LOG_BUFFER_LEN = 1048576;


        public static void DefaultFileHandler(IFileHandler handler)
        {
            handler.HandledExtensions = new string[1] { ".xlsx" };
        }

        public static void DefaultSignModuleBase(ISignModule module)
        {
            module.InPath = Environment.ExpandEnvironmentVariables("%USERPROFILE%\\Documents\\" + module.Name + "\\OMS_INPUT");
            module.OutPath = Environment.ExpandEnvironmentVariables("%USERPROFILE%\\Documents\\" + module.Name + "\\OMS_OUTPUT");
            module.HandledRepositoryPath = Environment.ExpandEnvironmentVariables("%USERPROFILE%\\Documents\\" + module.Name + "\\OMS_HANDLED");
        }

        public static void DefaultSignModule(SignModule module)
        {
            module.TempPathRelative = Environment.ExpandEnvironmentVariables("%USERPROFILE%\\Documents\\" + module.Name + "\\Temp");
        }

        public static ProgramConfig DefaultConfig = new ProgramConfig()
        {
            //InPath = Environment.ExpandEnvironmentVariables("%USERPROFILE%\\Documents\\OMS_INPUT"),
            //OutPath = Environment.ExpandEnvironmentVariables("%USERPROFILE%\\Documents\\OMS_OUTPUT"),
            //HandledRepositoryPath = Environment.ExpandEnvironmentVariables("%USERPROFILE%\\Documents\\OMS_HANDLED"),
            //PacketFilesToSignExtensions = new string[5] { "pdf", "xlsx", "doc", "docx", "csv"},
            //SignaturesProfiles = new string[0],
        };

        public static string ExecutePath(string rightPart)
        {
            return Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + "\\" + rightPart;
        }

    }

    [XmlRoot]
    [XmlInclude(typeof(HandlerType))]
    [Serializable]
    public class ModuleConfig
    {
        [XmlElement]
        public string InPath { get; set; }
        [XmlElement]
        public string OutPath { get; set; }
        [XmlElement]
        public string HandledRepositoryPath { get; set; }

        [XmlArray]
        [XmlArrayItem("HandlerType")]
        public HandlerType[] HandlerTypes { get; set; } = new HandlerType[0];

        [XmlIgnore]
        public static ProgramConfig Instance { get; } = new ProgramConfig();

        [Serializable]
        public class HandlerType
        {
            [XmlElement]
            public string Id { get; set; }
            [XmlElement]
            public bool DeleteFileAfterSign { get; set; }
            [XmlElement]
            public bool JoinProfiles { get; set; }

            [XmlArray]
            [XmlArrayItem("Item")]
            public string[] ArmProfiles { get; set; } = new string[0];
            [XmlArray]
            [XmlArrayItem("Item")]
            public string[] FilesToSignExtensions { get; set; } = new string[0];

            public HandlerType()
            {

            }

            public HandlerType(string id)
            {
                Id = id;
            }

            public void Assign(HandlerType source)
            {
                Id = source.Id;
                DeleteFileAfterSign = source.DeleteFileAfterSign;
                JoinProfiles = source.JoinProfiles;
                ArmProfiles = new string[source.ArmProfiles.Length];
                source.ArmProfiles.CopyTo(ArmProfiles, 0);
                FilesToSignExtensions = new string[source.FilesToSignExtensions.Length];
                source.FilesToSignExtensions.CopyTo(FilesToSignExtensions, 0);
            }

            public void Clear()
            {
                ArmProfiles = new string[0];
                FilesToSignExtensions = new string[0];
                DeleteFileAfterSign = false;
                JoinProfiles = false;
            }

            public bool ContainsFileToSignExt(string ext)
            {
                ext = ext.ToLower();

                foreach (string query_ext in FilesToSignExtensions)
                {
                    if (ext.Equals(query_ext.ToLower())) return true;
                }

                return false;
            }
        }

        public ModuleConfig()
        {
            Clear();
        }

        public void Assign(ModuleConfig source)
        {
            Clear();
            InPath = source.InPath;
            OutPath = source.OutPath;
            HandledRepositoryPath = source.HandledRepositoryPath;

            HandlerTypes = new HandlerType[source.HandlerTypes.Length];
            for (int i = 0; i < source.HandlerTypes.Length; i++)
            {
                HandlerTypes[i] = new HandlerType();
                HandlerTypes[i].Assign(source.HandlerTypes[i]);
            }

        }

        public void Clear()
        {
            InPath = "";
            OutPath = "";
            HandledRepositoryPath = "";
            foreach (HandlerType profile in HandlerTypes)
            {
                profile.Clear();
            }

            HandlerTypes = new HandlerType[0];
        }

        public static void LoadConfigFromFile(string fileName, ProgramConfig destination)
        {
            XmlSerializer xml_ser = new XmlSerializer(typeof(ProgramConfig));

            if (!File.Exists(fileName))
            {
                Log._("Файл конфигурации не найден. Загружены значения по умолчанию.", LogLevel.Info);
                ProgramConfig.Instance.Assign(Globals.DefaultConfig);
                return;
            }

            try
            {
                using (StreamReader fs = new StreamReader(fileName, false))
                {
                    destination.Clear();
                    try
                    {
                        ProgramConfig instance = xml_ser.Deserialize(fs) as ProgramConfig;
                        destination.Assign(instance);
                    }
                    catch
                    {
                        Log._("Произошла ошибка чтения конфигурационного файла. Загружены значения по умолчанию.", LogLevel.Fatal);
                    }
                }
            }
            catch (Exception e)
            {
                Log._(e);
            }
        }

        public static void SaveConfigToFile(string fileName, ProgramConfig source)
        {
            XmlSerializer xml_ser = new XmlSerializer(typeof(ProgramConfig));

            using (StreamWriter fs = new StreamWriter(fileName, false))
            {
                try
                {
                    xml_ser.Serialize(fs, source);
                }
                catch (Exception e)
                {
                    Log._(e, "Ошибка записи конфигурационного файла.");
                }
            }
        }

        public HandlerType AddNewHandler()
        {
            HandlerType[] new_array = new HandlerType[HandlerTypes.Length + 1];
            HandlerType handler = new HandlerType("Новый обработчик");
            HandlerTypes.CopyTo(new_array, 0);
            new_array[HandlerTypes.Length] = handler;
            HandlerTypes = new_array;
            return handler;
        }


        public bool DeleteHandler(string id)
        {
            int index = HandlerTypeIndex(id);

            if (IsValidHandlerTypeIndex(index))
            {
                HandlerTypes[index].Clear();
                HandlerType[] new_array = new HandlerType[HandlerTypes.Length - 1];
                int counter = 0;
                for (int i=0; i<HandlerTypes.Length; i++)
                {
                    if (i == index) continue;

                    new_array[counter++] = HandlerTypes[i];
                }

                HandlerTypes = new_array;
                return true;
            }

            return false;
        }

        public int HandlerTypeIndex(HandlerType handler)
        {
            for (int i=0; i<HandlerTypes.Length; i++)
            {
                if (HandlerTypes[i].Equals(handler))
                    return i;
            }

            return -1;
        }

        public int HandlerTypeIndex(string id)
        {
            for (int i = 0; i < HandlerTypes.Length; i++)
            {
                if (HandlerTypes[i].Id.ToLower().Equals(id.ToLower()))
                    return i;
            }

            return -1;
        }

        public int HandlerTypeIndexBySigningFileExt(string ext)
        {
            for (int i = 0; i < HandlerTypes.Length; i++)
            {
                if (HandlerTypes[i].ContainsFileToSignExt(ext))
                    return i;
            }

            return -1;
        }

        public bool IsValidHandlerTypeIndex(int index)
        {
            return (index >= 0) && (index < HandlerTypes.Length);
        }

        public HandlerType GetHandlerType(int index)
        {
            if (IsValidHandlerTypeIndex(index)) return HandlerTypes[index];

            return null;
        }

        public HandlerType GetHandlerType(string id)
        {
            return GetHandlerType(HandlerTypeIndex(id));
        }

        public HandlerType GetHandlerTypeBySigningFileExt(string ext)
        {
            return GetHandlerType(HandlerTypeIndexBySigningFileExt(ext));
        }
    }

    [XmlType]
    [Serializable]
    public class ModuleManager: IAssignable, ICloneable
    {
        [XmlIgnore]
        public ISignModule[] Modules 
        { 
            get => iModules.ToArray(); 
            set
            {
                iModules.Clear();
                iModules.AddRange(value);
            }
        }
        protected List<ISignModule> iModules = new List<ISignModule>();

        [XmlArray("ModulesWrapper")]
        [XmlArrayItem("Item")]
        public XmlAnything<ISignModule>[] ModulesWrapper
        {
            get => XmlAnything<ISignModule>.WrapFromArray(Modules);
            set => Modules = XmlAnything<ISignModule>.WrapToArray(value);
        }

        public ISignModule GetModule(string name)
        {
            name = name.ToLower();

            foreach (ISignModule module in Modules)
            {
                if (module.Name.ToLower().Equals(name))
                {
                    return module;
                }
            }

            return null;
        }

        public ISignModule GetModule(Type type)
        {
            foreach (ISignModule module in Modules)
            {
                if (module.GetType().Equals(type))
                {
                    return module;
                }
            }

            return null;
        }


        public bool AddModule(ISignModule module)
        {
            if (iModules.Contains(module)) return false;

            iModules.Add(module);
            return true;
        }

        public bool RemoveModule(ISignModule module)
        {
            return iModules.Remove(module);
        }

        public void Clear()
        {
            iModules.Clear();
        }

        public void Assign(IAssignable source)
        {
            ModuleManager smanager = source as ModuleManager;

            if (smanager == null) return;

            Modules = smanager.Modules;
        }

        public object Clone()
        {
            ModuleManager result = new ModuleManager();
            result.Assign(this);
            return result;
        }
    }

    [XmlRoot]
    [XmlInclude(typeof(ModuleManager))]
    [Serializable]
    public class ProgramConfig
    {
        [XmlIgnore]
        public static ProgramConfig Instance { get; } = new ProgramConfig();

        [XmlElement]
        public bool LockModuleSelect { get; set; } = false;
        [XmlElement]
        public string ModuleName{ get; set; } = "";
        [XmlElement]
        public ModuleManager ModuleManager { get; set; } = new ModuleManager();


        public ProgramConfig()
        {
            Clear();
        }

        public void Assign(ProgramConfig source)
        {
            Clear();

            LockModuleSelect = source.LockModuleSelect;
            ModuleName = source.ModuleName;
            ModuleManager = source.ModuleManager?.Clone() as ModuleManager;
        }

        public void Clear()
        {
            LockModuleSelect = false;
            ModuleName = string.Empty;
            ModuleManager?.Clear();
        }

        public static void LoadConfigFromFile(string fileName, ProgramConfig destination)
        {
            XmlSerializer xml_ser = new XmlSerializer(typeof(ProgramConfig));

            if (!File.Exists(fileName))
            {
                Log._("Файл конфигурации не найден. Загружены значения по умолчанию.", LogLevel.Info);
                ProgramConfig.Instance.Assign(Globals.DefaultConfig);
                return;
            }

            try
            {
                using (StreamReader fs = new StreamReader(fileName, false))
                {
                    destination.Clear();
                    try
                    {
                        ProgramConfig instance = xml_ser.Deserialize(fs) as ProgramConfig;
                        destination.Assign(instance);
                    }
                    catch
                    {
                        Log._("Произошла ошибка чтения конфигурационного файла. Загружены значения по умолчанию.", LogLevel.Fatal);
                    }
                }
            }
            catch (Exception e)
            {
                Log._(e);
            }
        }

        public static void SaveConfigToFile(string fileName, ProgramConfig source)
        {
            XmlSerializer xml_ser = new XmlSerializer(typeof(ProgramConfig));

            using (StreamWriter fs = new StreamWriter(fileName, false))
            {
                try
                {
                    xml_ser.Serialize(fs, source);
                }
                catch (Exception e)
                {
                    Log._(e, "Ошибка записи конфигурационного файла.");
                }
            }
        }

    }
}

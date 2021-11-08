using System;
using System.Collections.Generic;
using System.IO;
using NLog;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace OMSSigner
{
    [XmlType]
    [Serializable]
    public class SignModuleBase : FileHandlerBase, ISignModule
    {

        [UIBinder(UIClass = typeof(TextBox),
            bindedPropertyName = "Text",
            bindedPropertyType = typeof(string),
            useLabel = true,
            LabelText = "Имя модуля")]
        public override string Name => base.Name;
        [UIBinder(IgnoreInheritedAttr = true)]
        public override string[] HandledExtensions => base.HandledExtensions;
        [UIBinder(UIClass = typeof(PathSelectionControl),
            bindedPropertyName = "SelectedPath",
            bindedPropertyType = typeof(string),
            useLabel = true,
            LabelText = "Путь ко входящей директории")]
        public virtual string InPath { get; set; }
        [UIBinder(UIClass = typeof(PathSelectionControl),
            bindedPropertyName = "SelectedPath",
            bindedPropertyType = typeof(string),
            useLabel = true,
            LabelText = "Путь к исходящей директории")]
        public virtual string OutPath { get; set; }
        [UIBinder(UIClass = typeof(PathSelectionControl),
            bindedPropertyName = "SelectedPath",
            bindedPropertyType = typeof(string),
            useLabel = true,
            LabelText = "Путь к директории для обработанных файлов")]
        public virtual string HandledRepositoryPath { get; set; }

        [UIBinder(UIClass = typeof(PathSelectionControl),
            bindedPropertyName = "SelectedPath",
            bindedPropertyType = typeof(string),
            useLabel = true,
            LabelText = "Временная директория")
           ]
        public override string TempPathRelative { get => base.TempPathRelative; set => base.TempPathRelative = value; }

        [XmlElement]
        public virtual bool MonitorActivity { get; set; }



        public override void Assign(IAssignable source)
        {
            base.Assign(source);
            ISignModule module = source as ISignModule;
            InPath = module.InPath;
            OutPath = module.OutPath;
            HandledRepositoryPath = module.HandledRepositoryPath;
            Handlers = module.Handlers;
        }

        public override object Clone()
        {
            ISignModule module = new SignModuleBase();
            module.Assign(this);
            return module;
        }

        public override void ToDefault()
        {
            Globals.DefaultSignModuleBase(this);
        }

        public virtual void Sign()
        {

        }

        public void LoadFromFile(string fileName)
        {
            ToDefault();
            XmlSerializer xml_ser = new XmlSerializer(GetType());

            if (!File.Exists(fileName))
            {
                Log._("Файл конфигурации не найден. Загружены значения по умолчанию.", LogLevel.Info);
                return ;
            }

            try
            {
                using (StreamReader fs = new StreamReader(fileName, false))
                {
                    try
                    {
                        Assign(xml_ser.Deserialize(fs) as ISignModule);
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

        public void SaveToFile(string fileName)
        {
            XmlSerializer xml_ser = new XmlSerializer(GetType());

            using (StreamWriter fs = new StreamWriter(fileName, false))
            {
                try
                {
                    xml_ser.Serialize(fs, this);
                }
                catch (Exception e)
                {
                    Log._(e, "Ошибка записи конфигурационного файла.");
                }
            }
        }

        
    }


}

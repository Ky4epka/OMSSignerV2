using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.Drawing;

namespace OMSSigner
{
    [XmlType]
    [Serializable]
    public class FileHandlerBase : IFileHandler
    {

        public FileHandlerBase()
        {
        }

        public FileHandlerBase(IFileHandler owner)
        {
            Owner = owner;
        }

        // Индексы сортировки заданы для того, чтобы эти элементы отображались вверху списка свойств
        // (вынужденный костыль из-за особенностей получения свойств класса через GetProperties)
        [UIBinder(
            UIClass = typeof(TextBox),
            bindedPropertyName = "Text",
            useLabel = true,
            LabelText = "Имя обработчика",
            Control_SortIndex = 0)]
        public virtual string Name { get; set; }
        [XmlIgnore]
        public virtual string TempPathRelative
        {
            get => iTempPathRelative;
            set => iTempPathRelative = value; 
        }

        [XmlIgnore]
        public virtual string TempPathAbsolute
        {
            get
            {
                if (iOwner != null)
                    return iOwner.TempPathRelative + "\\" + TempPathRelative;
                else
                    return TempPathRelative;
            }
        }

        [UIBinder(
            UIClass = typeof(RichTextBox),
            bindedPropertyName = "Lines",
            useLabel = true,
            LabelText = "Обрабатываемые расширения файлов\n(без точек и пробелов, одно на строке)",
            Control_Height = 50,
            Control_SortIndex = 1)]
            
        public virtual string[] HandledExtensions 
        {
            get => iHandledExtensions.ToArray();

            set
            {
                ClearHandledExtensions();

                foreach (string ext in value)
                {
                    iHandledExtensions.Add(ext.ToLower());
                }
            }
        }

        [XmlIgnore]
        public virtual IFileHandler Owner 
        { 
            get => iOwner; 
            set
            {
                iOwner?.DeleteHandler(this);
                iOwner = value;
                iOwner?.AddHandler(this);
            } 
        }

        [UIBinder(UIClass = typeof(HandlersComponent),
            bindedPropertyName = "Handlers",
            bindedPropertyType = typeof(IFileHandler),
            useLabel = true,
            LabelText = "Обработчики")]

        [XmlIgnore]
        public virtual IFileHandler [] Handlers 
        { 
            get
            {
                return iHandlers.ToArray();
            }
            set
            {
                iHandlers.Clear();
                foreach (IFileHandler handler in value)
                    handler.Owner = this;
            }
        }

        public XmlAnything<IFileHandler>[] XmlHandlersWrapper 
        { 
            get
            {
                return XmlAnything<IFileHandler>.WrapFromArray(Handlers);
            }
            set
            {
                Handlers = XmlAnything<IFileHandler>.WrapToArray(value);
            }
        }

        protected IFileHandler iOwner = null;
        protected List<string> iHandledExtensions = new List<string>();
        protected List<IFileHandler> iHandlers = new List<IFileHandler>();
        protected string iTempPathRelative = "";


        public virtual bool AddHandler(IFileHandler handler)
        {
            if (iHandlers.Contains(handler)) return false;

            iHandlers.Add(handler);
            return true;
        }

        public virtual bool DeleteHandler(int index)
        {
            if (IsValidHandlerTypeIndex(index))
            {
                iHandlers.RemoveAt(index);
                return true;
            }

            return false;
        }

        public virtual bool DeleteHandler(IFileHandler handler)
        {
            return iHandlers.Remove(handler);
        }

        public bool DeleteHandler(string id)
        {
            return DeleteHandler(HandlerTypeIndex(id));
        }

        public int HandlerTypeIndex(IFileHandler handler)
        {
            return iHandlers.IndexOf(handler);
        }

        public int HandlerTypeIndex(string id)
        {
            for (int i = 0; i < iHandlers.Count; i++)
            {
                if (iHandlers[i].Name.ToLower().Equals(id.ToLower()))
                    return i;
            }

            return -1;
        }

        public bool IsValidHandlerTypeIndex(int index)
        {
            return (index >= 0) && (index < iHandlers.Count);
        }

        public IFileHandler GetHandlerType(int index)
        {
            if (IsValidHandlerTypeIndex(index)) return iHandlers[index];

            return null;
        }

        public IFileHandler GetHandlerType(string id)
        {
            return GetHandlerType(HandlerTypeIndex(id));
        }

        public int HandlerTypeIndexBySigningFileExt(string ext)
        {
            for (int i = 0; i < iHandlers.Count; i++)
            {
                if (iHandlers[i].ContainsFileToSignExt(ext))
                    return i;
            }

            return -1;
        }
        public IFileHandler GetHandlerTypeBySigningFileExt(string ext)
        {
            return GetHandlerType(HandlerTypeIndexBySigningFileExt(ext));
        }


        public virtual void ClearHandlers()
        {
            iHandlers.Clear();
        }

        public virtual void Assign(IAssignable source)
        {
            IFileHandler handler = (source as IFileHandler);

            //Owner = handler.Owner;
            Name = handler.Name;
            HandledExtensions = handler.HandledExtensions;
            TempPathRelative = handler.TempPathRelative;
        }

        public virtual object Clone()
        {
            IFileHandler result = new FileHandlerBase();
            result.Assign(this);
            return result;
        }

        public bool ContainsFileToSignExt(string ext)
        {
            ext = ext.ToLower();
            foreach (string cont_ext in iHandledExtensions)
            {
                if (cont_ext.ToLower().Equals(ext)) return true;
            }

            return false;
        }

        public virtual bool Handle(string fileName, string outputDir)
        {
            foreach (IFileHandler handler in iHandlers)
            {
                if (!handler.Handle(fileName, outputDir)) return false;
            }

            return true;
        }

        public void ClearHandledExtensions()
        {
            iHandledExtensions.Clear();
        }

        public virtual void ToDefault()
        {
            Globals.DefaultFileHandler(this);
        }
    }


}

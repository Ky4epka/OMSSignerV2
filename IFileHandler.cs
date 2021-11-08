using System;

namespace OMSSigner
{
    public interface IHasDefaultCapability
    {
        void ToDefault();
    }

    public interface IFileHandler: IAssignable, ICloneable, IHasDefaultCapability
    {
        string Name { get; set; }
        string TempPathRelative { get; set; }
        string TempPathAbsolute { get; }
        string [] HandledExtensions { get; set; }
        IFileHandler Owner { get; set; }
        IFileHandler[] Handlers { get; set; }

        /// <summary>
        /// Обработчик файла.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="outputDir"></param>
        /// <returns>True, если файл не обработан или его обработка не исключительна. False - если нужно завершить обработку.</returns>
        bool Handle(string fileName, string outputDir);

        void ClearHandledExtensions();

        bool ContainsFileToSignExt(string ext);
        bool AddHandler(IFileHandler handler);
        bool DeleteHandler(IFileHandler handler);
        void ClearHandlers();
    }
}

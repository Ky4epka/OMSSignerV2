using System;

namespace OMSSigner
{
    public interface ISignModule: IAssignable, ICloneable, IHasDefaultCapability
    {
        string Name { get; set; }
        string InPath { get; set; }
        string OutPath { get; set; }
        string HandledRepositoryPath { get; set; }

        IFileHandler[] Handlers { get; set; }
        bool MonitorActivity { get; set; }


        void Sign();

        void LoadFromFile(string fileName);
        void SaveToFile(string fileName);

        bool AddHandler(IFileHandler handler);
        bool DeleteHandler(IFileHandler handler);
        void ClearHandlers();
    }


}

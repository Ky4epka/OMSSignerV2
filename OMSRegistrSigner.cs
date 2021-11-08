using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OMSSigner
{

    public class OMSRegistrPacketHandler: PacketHandlerBase
    {
        public override string Name { get => "Обработчик пакетов"; }

        public OMSRegistrPacketHandler() : base()
        {

        }

        public OMSRegistrPacketHandler(IFileHandler owner) : base(owner)
        {

        }
    }

    public class OMSRegistrFileHandler: SignerHandler
    {
        public override string Name { get => "Обработчик файлов реестров"; }


        public OMSRegistrFileHandler() : base()
        {

        }

        public OMSRegistrFileHandler(IFileHandler owner) : base(owner)
        {

        }
    }

    [Serializable]
    public class OMSRegistrSigner: SignModule
    {
        public override string Name 
        { 
            get => "Подпись реестров ОМС"; 
        }

        protected OMSRegistrPacketHandler iPacketHandler = null;
        protected OMSRegistrFileHandler iSignerHandler = null;

        public OMSRegistrSigner() : base()
        {
            iPacketHandler = new OMSRegistrPacketHandler(this);
            iSignerHandler = new OMSRegistrFileHandler(iPacketHandler);
        }
    }
}

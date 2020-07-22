using EpipeProtocal.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpipeProtocal.YongMeiSanHeng
{
    class Unknown : Base
    {
        public Unknown(ParseProtocal protocol, DataRepo dataRepo, Action<string> log)
            : base(protocol, dataRepo, log)
        {

        }
        public override string FileName => "";

        public override bool IsSatisfyCondition(string file)
        {
            return false;
        }

        public override void ParseLines(string file)
        {
            Move2BackDirectory(file, BakDirType.Unkown);
        }

        protected override void ParseContent(List<string> lines, DateTime realTime)
        {

        }
    }
}

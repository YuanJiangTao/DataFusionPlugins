using MineSafetySystemProtocal.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSafetySystemProtocal.YongMeiSanHeng
{
    class Accdata : Base
    {
        public Accdata(ParseProtocal protocol, DataRepo dataRepo, Action<string> log, Dev dev)
            : base(protocol, dataRepo, log)
        {

        }
        public override string FileName => "ACCDATA";

        public override bool IsSatisfyCondition(string file)
        {
            return false;
        }

        protected override void ParseContent(List<string> lines, DateTime realTime)
        {

        }
    }
}

using MineSafetySystemProtocal.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSafetySystemProtocal.YongMeiSanHeng
{
    class Kgbhdata : Base
    {
        public Kgbhdata(ParseProtocal protocol, DataRepo dataRepo, Action<string> log, Dev dev)
            : base(protocol, dataRepo, log)
        {

        }
        public override string FileName => "KGBHDATA";

        public override bool IsSatisfyCondition(string file)
        {
            return false;
        }

        protected override void ParseContent(List<string> lines, DateTime realTime)
        {

        }
    }
}

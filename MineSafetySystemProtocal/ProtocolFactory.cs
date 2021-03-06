﻿using DataFusionProtocal.Interfaces;
using MineSafetySystemProtocal.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MineSafetySystemProtocal
{
    internal class ProtocolFactory
    {
        internal static IParseProtocal Create(string protocalName)
        {
            IParseProtocal protocol = default;
            switch (protocalName)
            {
                case ProtocalConst.SANHENG_SAFETY_PROTOCAL:
                    protocol = new  YongMeiSanHeng.ParseProtocal();
                    break;
                default:
                    break;
            }
            return protocol;
        }
    }

    internal interface IParseProtocal : IDisposable
    {
        void Load(DataRepo repo, Action<string> log, IProtocalHostConfig hostConfig);
        void Cancel();
    }
}

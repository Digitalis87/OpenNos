﻿////<auto-generated <- Codemaid exclusion for now (PacketIndex Order is important for maintenance)
using OpenNos.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenNos.GameObject
{
    [PacketHeader("eq")]
    public class EqPacket : PacketDefinition
    {
        [PacketIndex(0)]
        public long Id { get; set; }
    }

    [PacketHeader("eqsub")]
    public class EqSubPacket : PacketDefinition
    {
    }

    [PacketHeader("eqraresub")]
    public class EqRareSubPacket : PacketDefinition
    {
    }
}

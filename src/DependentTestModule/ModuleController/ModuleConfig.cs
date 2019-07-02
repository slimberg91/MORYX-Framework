﻿using System.Runtime.Serialization;
using Marvin.Configuration;
using Marvin.Serialization;

namespace Marvin.DependentTestModule
{
    [DataContract]
    public class ModuleConfig : ConfigBase
    {
        [DataMember]
        [PluginConfigs(typeof(ISimpleHelloWorldWcfConnector))]
        public SimpleHelloWorldWcfConnectorConfig SimpleHelloWorldWcfConnector { get; set; }
    }
}

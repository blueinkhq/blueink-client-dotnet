using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueink.Client.Net.v2.Serializer
{
    public interface ISerializer
    {
        string Format { get; }
        void Serialize(object obj, Stream target);
        string Serialize(object obj);
        T Deserialize<T>(string input);
        object Deserialize(string input, Type type);
        T Deserialize<T>(Stream input);
    }
}

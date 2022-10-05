using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spp_laba_1
{
    public class WriterToFile : Writer
    {
        public void Write(string text, string Format = null)
        {
            File.WriteAllText($"out.{Format}", text);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.Dto.WhatsAPP
{
    public class WhatsAppOrderRequestDto
    {
        public string messaging_product { get; set; }
        public string to { get; set; }
        public string type { get; set; }
        public Template template { get; set; }
    }

    public class Component
    {
        public string type { get; set; }
        public List<ComponentParameter> parameters { get; set; }
        public string sub_type { get; set; }
        public int? index { get; set; }
    }

    public class Language
    {
        public string code { get; set; }
    }

    public class ComponentParameter
    {
        public string type { get; set; }
        public string text { get; set; }
    }

    public class Template
    {
        public string name { get; set; }
        public Language language { get; set; }
        public List<Component> components { get; set; }
    }
}

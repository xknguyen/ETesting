using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WebCore.Models
{
    public class DataGroupModel
    {
        public string Href { get; set; }
        public string Text { get; set; }

        public List<DataGroupModel> Nodes { get; set; }
    }
}
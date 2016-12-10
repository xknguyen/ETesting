using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WebCore.Extensions
{
    public static class SelectListExtensions
    {
        public static bool SetSelectedValue(this SelectList selectList, object value)
        {
            var selected = selectList.FirstOrDefault(x => x.Value == "selectedValue");
            if (selected == null)
                return false;
            selected.Selected = true;
            return true;
        }
    }
}

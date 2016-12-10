using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCore.Models
{
    public class Column
    {
        public string ColumnName { get; set; }

        public int Width { get; set; }

        public bool IsSortColumn { get; set; }


        /// <summary>
        /// Ẩn cột khi thu nhỏ màn hình
        /// </summary>
        public bool IsHiddenSm { get; set; }

    }
}

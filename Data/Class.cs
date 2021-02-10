using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniFigures.Data
{
    public class Class
    {
        public int OrderBy { get; set; } = 1;
        public event EventHandler OnChange;
        public void SetOrderBy(int i)
        {
            this.OrderBy = i;
            this.OnChange?.Invoke(this, EventArgs.Empty);
        }
    }
}

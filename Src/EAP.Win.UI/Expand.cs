using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EAP.Win.UI
{
    public static class Expand
    {
        public static DataGridViewColumn FindColumn(this DataGridView grid, string propertyName)
        {
            foreach (DataGridViewColumn c in grid.Columns)
                if (c.DataPropertyName == propertyName)
                    return c;
            return null;
        }

        public static void ClearError(this DataGridView grid)
        {
            foreach (DataGridViewRow r in grid.Rows)
                foreach (DataGridViewCell c in r.Cells)
                    c.ErrorText = "";
        }

        internal static bool StateExcludes(this DataGridViewColumn column, DataGridViewElementStates elementState)
        {
            return ((column.State & elementState) == DataGridViewElementStates.None);
        }

        internal static bool StateIncludes(this DataGridViewColumn column, DataGridViewElementStates elementState)
        {
            return ((column.State & elementState) == elementState);
        }
    }
}

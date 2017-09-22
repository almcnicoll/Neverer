using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Neverer.UtilityClass;
using System.Drawing;
using System.ComponentModel;

namespace Neverer.DataGridViewClasses
{
    public class DataGridViewColouredCell : DataGridViewTextBoxCell
    {
        public Neverer.UtilityClass.PlacedClue.ClueStatus Status;

        protected override void Paint(System.Drawing.Graphics graphics,
            System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle cellBounds,
            int rowIndex, DataGridViewElementStates cellState,
            object value, object formattedValue, string errorText,
            DataGridViewCellStyle cellStyle,
            DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
        {
            // Populate status from owning row's data object
            PlacedClue pc = ((PlacedClue)this.DataGridView.Rows[rowIndex].DataBoundItem);
            if (pc != null)
            {
                this.Status = pc.status;
                cellStyle.BackColor = PlacedClue.StatusColor(this.Status);
            }

            base.Paint(graphics, clipBounds, cellBounds, rowIndex,
                cellState, value, formattedValue, errorText,
                cellStyle, advancedBorderStyle, paintParts);
        }

        public override Object Clone()
        {
            Object o = base.Clone();
            ((DataGridViewColouredCell)o).Status = this.Status;
            return o;
        }
        
    }


    public class DataGridViewColouredColumn : DataGridViewTextBoxColumn
    {
        public DataGridViewColouredColumn()
        {
            this.CellTemplate = new DataGridViewColouredCell();
        }
    }
}

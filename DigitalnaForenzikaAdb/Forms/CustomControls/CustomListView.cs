using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DigitalnaForenzikaAdb.CustomControls
{
    public class CustomListView : ListView
    {
        public CustomListView(int width, int height, List<string> columns)
        {
            AddHeaders(columns);
            View = View.Details;
            Width = width;
            Height = height;
        }

        public void AddHeaders(List<string> columns)
        {
            HeaderStyle = ColumnHeaderStyle.Clickable;

            foreach (var column in columns)
            {
                Columns.Add(column, 100);
            }
        }
    }
}

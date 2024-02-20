using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.IO;

namespace Servis_2024
{
    public class ExportToCSV
    {
        public void ExportDataGridToCSV(DataGrid dataGrid, string path)
        {
            dataGrid.SelectAllCells();
            dataGrid.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, dataGrid);
            dataGrid.UnselectAllCells();
            String result = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            File.WriteAllText(path, result, UnicodeEncoding.UTF8);
        }
    }
}

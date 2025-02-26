using System.Windows.Forms;

namespace MrDAL.Utility.GridControl;

public interface IStackedHeaderGenerator
{
    Header GenerateStackedHeader(DataGridView objGridView);
}
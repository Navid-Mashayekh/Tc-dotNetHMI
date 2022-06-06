using System.Windows.Forms;
using System.ComponentModel;

namespace HMI1
{
  class MomentaryButton : Button
  {
    [Description("The BOOL Variable in the PLC to write to"),
         Category("PLC")]
    public string VariableName { get; set; }
  }
}

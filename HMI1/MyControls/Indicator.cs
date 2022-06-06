using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace HMI1
{
  [ToolboxItem(true)]
  class Indicator : Label
  {
    public Indicator()
    {
      this.AutoSize = false;
      this.Width = 100;
      this.Height = 25;
      this.TextAlign = ContentAlignment.MiddleCenter;
      this.BorderStyle = BorderStyle.FixedSingle;
      this.OnColor = Color.Green;
      this.OffColor = Color.DarkGray;
    }

    [Description("The BOOL Variable in the PLC to read from"),
     Category("PLC")]
    public string VariableName { get; set; }

    [Description("Color when the Variable is TRUE"),
     Category("PLC")]
    public Color OnColor { get; set; }

    [Description("Color when the Variable is FALSE"),
     Category("PLC")]
    public Color OffColor { get; set; }
  }
}

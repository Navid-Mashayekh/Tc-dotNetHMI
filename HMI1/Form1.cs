using System;
using System.Windows.Forms;
using TwinCAT.Ads;


namespace HMI1
{
  public partial class Form1 : Form
  {
    private static readonly int port = 851;
    private static readonly AmsNetId netId = new AmsNetId("192.168.188.1.1.1");
    private readonly CommunicationManager communicationManager
                         = new CommunicationManager(netId, port);

    public Form1()
    {
      InitializeComponent();

      foreach (var control in this.Controls)
      {
        this.communicationManager
            .Register(control as Control);
      }
      this.tmrPoll.Tick += (s, e) =>
      {
        this.communicationManager.Poll();
      };
      this.tmrPoll.Start();
    }

    private void button1_Click(object sender, System.EventArgs e)
    {
      var readWriteErrorVariableNames
        = this.communicationManager.GetReadWriteErrors();
      var readWriteErrors = string.Join(
          Environment.NewLine,
          readWriteErrorVariableNames);
      if (string.IsNullOrWhiteSpace(readWriteErrors))
      {
        MessageBox.Show("None");
      }
      else
      {
        MessageBox.Show(readWriteErrors);
      }
    }
  }
}

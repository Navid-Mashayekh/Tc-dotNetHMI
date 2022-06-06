
namespace HMI1
{
  partial class Form1
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.tmrPoll = new System.Windows.Forms.Timer(this.components);
      this.button1 = new System.Windows.Forms.Button();
      this.momentaryButton1 = new HMI1.MomentaryButton();
      this.indicator1 = new HMI1.Indicator();
      this.SuspendLayout();
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(563, 339);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(75, 23);
      this.button1.TabIndex = 0;
      this.button1.Text = "Check Err";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // momentaryButton1
      // 
      this.momentaryButton1.Location = new System.Drawing.Point(13, 13);
      this.momentaryButton1.Name = "momentaryButton1";
      this.momentaryButton1.Size = new System.Drawing.Size(129, 23);
      this.momentaryButton1.TabIndex = 1;
      this.momentaryButton1.Text = "Switch";
      this.momentaryButton1.UseVisualStyleBackColor = true;
      this.momentaryButton1.VariableName = "GVL1.Switch";
      // 
      // indicator1
      // 
      this.indicator1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.indicator1.Location = new System.Drawing.Point(13, 65);
      this.indicator1.Name = "indicator1";
      this.indicator1.OffColor = System.Drawing.Color.DarkGray;
      this.indicator1.OnColor = System.Drawing.Color.Green;
      this.indicator1.Size = new System.Drawing.Size(129, 105);
      this.indicator1.TabIndex = 2;
      this.indicator1.Text = "Lamp";
      this.indicator1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.indicator1.VariableName = "GVL1.Lamp";
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(650, 374);
      this.Controls.Add(this.indicator1);
      this.Controls.Add(this.momentaryButton1);
      this.Controls.Add(this.button1);
      this.Name = "Form1";
      this.Text = "Form1";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Timer tmrPoll;
    private System.Windows.Forms.Button button1;
    private MomentaryButton momentaryButton1;
    private Indicator indicator1;
  }
}


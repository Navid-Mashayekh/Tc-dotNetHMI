using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using TwinCAT.Ads;

namespace HMI1
{
  class CommunicationManager : IDisposable
  {
    private readonly int port;
    private readonly AmsNetId amsNetId;
    private readonly TcAdsClient client = new TcAdsClient();
    private readonly List<Action> pollActions
                     = new List<Action>();
    private readonly Dictionary<string, DateTime> readWriteErrors
                     = new Dictionary<string, DateTime>();
    private bool connected;
    private DateTime? lastErrorTime = null;

    public CommunicationManager(AmsNetId amsNetId, int port)
    {
      this.port = port;
      this.amsNetId = amsNetId;
    }

    public void Poll()
    {
      foreach (var action in this.pollActions)
      {
        action();
      }
    }

    public bool IsConnected
    {
      get { return this.connected; }
    }

    public ReadOnlyCollection<string> GetReadWriteErrors()
    {
      var result = this.readWriteErrors.Keys
          .OrderBy(x => x)
          .ToList();
      return result.AsReadOnly();
    }

    public void Register(Control control)
    {
      if (control == null) return;
      if (control is MomentaryButton)
      {
        this.register(control as MomentaryButton);
      }
      else if (control is Indicator)
      {
        this.register(control as Indicator);
      }
    }

    private void register(MomentaryButton momentaryButton)
    {
      momentaryButton.MouseDown += (s, e) =>
      {
        this.doWithClient(c =>
        {
          c.WriteSymbol(momentaryButton.VariableName,
              true,
              reloadInfo: true);
        }, momentaryButton.VariableName);
      };
      momentaryButton.MouseUp += (s, e) =>
      {
        this.doWithClient(c =>
        {
          c.WriteSymbol(momentaryButton.VariableName,
              false,
              reloadInfo: true);
        }, momentaryButton.VariableName);
      };
    }

    private void register(Indicator indicator)
    {
      this.pollActions.Add(() =>
      {
        this.doWithClient(c =>
        {
          if (string.IsNullOrWhiteSpace(
              indicator.VariableName))
          {
            return;
          }
          bool value = (bool)c.ReadSymbol(
              indicator.VariableName, typeof(bool),
              reloadInfo: true);
          indicator.BackColor = value
              ? indicator.OnColor
              : indicator.OffColor;
        }, indicator.VariableName);
      });
    }

    private void doWithClient(
        Action<TcAdsClient> action,
        string variableName)
    {
      this.tryConnect();
      if (this.connected)
      {
        try
        {
          action(this.client);
          this.readWriteSuccess(variableName);
        }
        catch (AdsException)
        {
          readWriteError(variableName);
        }
      }
    }

    private void tryConnect()
    {
      if (!this.connected)
      {
        if (this.lastErrorTime.HasValue)
        {
          // wait a bit before re-establishing connection
          var elapsed = DateTime.Now
              .Subtract(this.lastErrorTime.Value);
          if (elapsed.TotalMilliseconds < 3000)
          {
            return;
          }
        }
        try
        {
          this.client.Connect(this.amsNetId, this.port);
          this.connected = this.client.IsConnected;
        }
        catch (AdsException)
        {
          connectError();
        }
      }
    }

    private void connectError()
    {
      this.connected = false;
      this.lastErrorTime = DateTime.Now;
    }

    private void readWriteSuccess(string variableName)
    {
      if (this.readWriteErrors.ContainsKey(variableName))
      {
        this.readWriteErrors.Remove(variableName);
      }
    }

    private void readWriteError(string variableName)
    {
      if (this.readWriteErrors.ContainsKey(variableName))
      {
        this.readWriteErrors[variableName] = DateTime.Now;
      }
      else
      {
        this.readWriteErrors.Add(variableName, DateTime.Now);
      }
    }

    public void Dispose()
    {
      this.client.Dispose();
    }
  }
}

using OpenLibSys;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReadWriteMSR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!IsAdmin())
            {
                RunAsRestart();
                Environment.Exit(0);
            }
            CenterToScreen();
            Init();
        }
        #region InitFunction
        private bool IsAdmin()
        {
            OperatingSystem osInfo = Environment.OSVersion;
            if (osInfo.Platform == PlatformID.Win32Windows)
            {
                return true;
            }
            else
            {
                WindowsIdentity usrId = WindowsIdentity.GetCurrent();
                WindowsPrincipal p = new WindowsPrincipal(usrId);
                return p.IsInRole(@"BUILTIN\Administrators");
            }
        }
        private bool RunAsRestart()
        {
            string[] args = Environment.GetCommandLineArgs();

            foreach (string s in args)
            {
                if (s.Equals("runas"))
                {
                    return false;
                }
            }
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.UseShellExecute = true;
            startInfo.WorkingDirectory = Environment.CurrentDirectory;
            startInfo.FileName = Application.ExecutablePath;
            startInfo.Verb = "runas";
            startInfo.Arguments = "runas";

            try
            {
                Process.Start(startInfo);
            }
            catch
            {
                return false;
            }
            return true;
        }
        unsafe private void Init()
        {
            //try
            {
                //-----------------------------------------------------------------------------
                // Initialize
                //-----------------------------------------------------------------------------
                Ols ols = new Ols();
                // Check support library sutatus
                switch (ols.GetStatus())
                {
                    case (uint)Ols.Status.NO_ERROR:
                        break;
                    case (uint)Ols.Status.DLL_NOT_FOUND:
                        MessageBox.Show("Status Error!! DLL_NOT_FOUND");
                        Environment.Exit(0);
                        break;
                    case (uint)Ols.Status.DLL_INCORRECT_VERSION:
                        MessageBox.Show("Status Error!! DLL_INCORRECT_VERSION");
                        break;
                    case (uint)Ols.Status.DLL_INITIALIZE_ERROR:
                        MessageBox.Show("Status Error!! DLL_INITIALIZE_ERROR");
                        break;
                }

                // Check WinRing0 status
                switch (ols.GetDllStatus())
                {
                    case (uint)Ols.OlsDllStatus.OLS_DLL_NO_ERROR:
                        break;
                    case (uint)Ols.OlsDllStatus.OLS_DLL_DRIVER_NOT_LOADED:
                        MessageBox.Show("DLL Status Error!! OLS_DRIVER_NOT_LOADED");
                        Environment.Exit(0);
                        break;
                    case (uint)Ols.OlsDllStatus.OLS_DLL_UNSUPPORTED_PLATFORM:
                        MessageBox.Show("DLL Status Error!! OLS_UNSUPPORTED_PLATFORM");
                        Environment.Exit(0);
                        break;
                    case (uint)Ols.OlsDllStatus.OLS_DLL_DRIVER_NOT_FOUND:
                        MessageBox.Show("DLL Status Error!! OLS_DLL_DRIVER_NOT_FOUND");
                        Environment.Exit(0);
                        break;
                    case (uint)Ols.OlsDllStatus.OLS_DLL_DRIVER_UNLOADED:
                        MessageBox.Show("DLL Status Error!! OLS_DLL_DRIVER_UNLOADED");
                        Environment.Exit(0);
                        break;
                    case (uint)Ols.OlsDllStatus.OLS_DLL_DRIVER_NOT_LOADED_ON_NETWORK:
                        MessageBox.Show("DLL Status Error!! DRIVER_NOT_LOADED_ON_NETWORK");
                        Environment.Exit(0);
                        break;
                    case (uint)Ols.OlsDllStatus.OLS_DLL_UNKNOWN_ERROR:
                        MessageBox.Show("DLL Status Error!! OLS_DLL_UNKNOWN_ERROR");
                        Environment.Exit(0);
                        break;
                }

                ols.DeinitializeOls();
            }
            /*catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                Environment.Exit(0);
            }*/
        }
        #endregion 
        private void bt_Read_Click(object sender, EventArgs e)
        {
            bool result = System.Text.RegularExpressions.Regex.IsMatch(tb_Register.Text.ToString(), @"\A\b(0[xX])?[0-9a-fA-F]+\b\Z");

            if (result)
            {
                UInt32 eax = 0x0;
                UInt32 edx = 0x0;
                Ols ols = new Ols();
                ols.InitializeOls();
                ols.Rdmsr(Convert.ToUInt32(tb_Register.Text, 16), ref eax, ref edx);
                UInt64 Value = ((UInt64)edx << 32) | eax;
                lb_Value.Text = Value.ToString("X").PadLeft(16,'0');
                ols.DeinitializeOls();
            }
            else
            {
                MessageBox.Show("Vlaue Fail", "Error",
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Error);
            }
        }


        private void lb_Value_Click(object sender, EventArgs e)
        {
            QWordForm.OutValue = Convert.ToUInt64(lb_Value.Text, 16);
            QWordForm dwf = new QWordForm();
            
            if (dwf.ShowDialog() == DialogResult.OK)
            {
                Ols ols = new Ols();
                ols.InitializeOls();
                ols.Wrmsr(Convert.ToUInt32(tb_Register.Text, 16), (UInt32)(QWordForm.OutValue), (UInt32)(QWordForm.OutValue >> 32));
                ols.DeinitializeOls();
            }
            //lb_Value.Text = String.Format("{0:X016}", QWordForm.OutValue);

            bt_Read_Click(null, null);
        }
    }
}

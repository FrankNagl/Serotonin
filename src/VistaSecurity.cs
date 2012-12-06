// Shader-Based-Image-Processing (SBIP)
// http://code.google.com/p/sbip/
//
// Copyright © Frank Nagl, 2009-2011
// admin@franknagl.de
////
// Parts of source code of this class from article at code project.
// http://www.codeproject.com/KB/vista-security/UAC_Shield_for_Elevation.aspx
//
namespace SBIP
{
    using System;
    using System.Runtime.InteropServices;
    using System.Diagnostics;
    using System.Windows.Forms;
    using System.Security.Principal;

    /// <summary>
    /// Elevates and restarts the process (sbip) as admin if required.
    /// </summary>
    public static class VistaSecurity
    {
        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="msg">The MSG.</param>
        /// <param name="wParam">The w param.</param>
        /// <param name="lParam">The l param.</param>
        /// <returns></returns>
        [DllImport("user32")]
        public static extern UInt32 SendMessage(
            IntPtr hWnd, UInt32 msg, UInt32 wParam, UInt32 lParam);

        internal const int BcmFirst = 0x1600;
        internal const int BcmSetshield = (BcmFirst + 0x000C);

        /// <summary>
        /// Checks windows version is vista or higher.
        /// </summary>
        /// <returns>True if windows version is vista or higher.</returns>
        internal static bool IsVistaOrHigher()
        {
            return Environment.OSVersion.Version.Major < 6;
        }

        /// <summary>
        /// Checks if the process is elevated (starts as admin).
        /// </summary>
        /// <returns>True if is elevated (starts as admin).</returns>
        internal static bool IsAdmin()
        {
            WindowsIdentity id = WindowsIdentity.GetCurrent();
            if (id != null)
            {
                WindowsPrincipal p = new WindowsPrincipal(id);
                return p.IsInRole(WindowsBuiltInRole.Administrator);
            }
            return false;
        }

        /// <summary>
        /// Add a shield icon to a button object.
        /// </summary>
        /// <param name="b">The windows form button.</param>
        internal static void AddShieldToButton(Button b)
        {
            b.FlatStyle = FlatStyle.System;
            SendMessage(b.Handle, BcmSetshield, 0, 0xFFFFFFFF);
        }

        /// <summary>
        /// Restart the current process with administrator credentials.
        /// </summary>
        internal static void RestartElevatedForUpdate()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.UseShellExecute = true;
            startInfo.WorkingDirectory = Environment.CurrentDirectory;
            startInfo.FileName = Application.ExecutablePath;
            startInfo.Verb = "runas";
            startInfo.Arguments = Program.UpdateArgument;
            try
            {
                Process.Start(startInfo);
            }
            catch(System.ComponentModel.Win32Exception)
            {
                return; //If cancelled, do nothing
            }

            Application.Exit();
        }
    }
}

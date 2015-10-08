using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hassium.Functions;
using Hassium.HassiumObjects;

namespace HassiumForms
{
    public class HassiumForm : HassiumControl
    {
        public HassiumForm(string title)
        {
            Value = new Form() {Text = title, ClientSize = new Size(284, 261), Name = string.Concat(title.Where(char.IsLetter))};
            //((Form)Value).FormClosed += (sender, args) => Environment.Exit(0);
            Attributes.Add("show", new InternalFunction(Show, 0));


        }

        private const int SW_SHOWNOACTIVATE = 4;
        private const int HWND_TOPMOST = -1;
        private const uint SWP_NOACTIVATE = 0x0010;

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        static extern bool SetWindowPos(
             int hWnd,             // Window handle
             int hWndInsertAfter,  // Placement-order handle
             int X,                // Horizontal position
             int Y,                // Vertical position
             int cx,               // Width
             int cy,               // Height
             uint uFlags);         // Window positioning flags

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [STAThread]
        public HassiumObject Show(HassiumObject[] args)
        {
            //Application.Run((Form)Value);
            var frm = (Form) Value;
            ShowWindow(frm.Handle, 1);
            SetWindowPos(frm.Handle.ToInt32(), HWND_TOPMOST,
            frm.Left, frm.Top, frm.Width, frm.Height,
            SWP_NOACTIVATE);
            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HostingApplication
{


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        [DllImport("user32.dll", EntryPoint = "SetWindowLongA", SetLastError = true)]
        public static extern int SetWindowLongA([System.Runtime.InteropServices.InAttribute()] System.IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern long SetWindowPos(IntPtr hwnd, long hWndInsertAfter, long x, long y, long cx, long cy, long wFlags);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hwnd, int x, int y, int cx, int cy, bool repaint);

        private System.Diagnostics.Process _childp;
        private string exeName = "TryBinding.exe";
        private const int WS_VISIBLE = 0x10000000;
        private const int WS_CHILD = 0x40000000;
        private const int GWL_STYLE = (-16);

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Thread.Sleep(500000);

                var procInfo = new System.Diagnostics.ProcessStartInfo(this.exeName);
                procInfo.WorkingDirectory = System.IO.Path.GetDirectoryName(this.exeName);
                // Start the process
                _childp = System.Diagnostics.Process.Start(procInfo);

                // Wait for process to be created and enter idle condition
                _childp.WaitForInputIdle();
                Thread.Sleep(100);
                
                // Get the main handle
                IntPtr _appWin = _childp.MainWindowHandle;
                //Thread.Sleep(100);
                // Put it into this form
                var helper = new WindowInteropHelper(Window.GetWindow(this)).Handle;
                Thread.Sleep(100);

                SetParent(_childp.MainWindowHandle, helper);
                
                //Thread.Sleep(100);

                // Remove border and whatnot
                SetWindowLongA(_childp.MainWindowHandle, GWL_STYLE, WS_VISIBLE);

                // Move the window to overlay it on this window
                MoveWindow(_childp.MainWindowHandle, 0, 0, (int)this.ActualWidth, (int)this.ActualHeight, true);
                //MoveWindow(_childp.MainWindowHandle, 0, 0, 100, 100, true);

            }
            catch (Exception ex)
            {
                //Debug.Print(ex.Message + "Error");
            }  
        }
    }
}

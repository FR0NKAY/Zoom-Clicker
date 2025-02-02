using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Zoom
{
    public partial class Zoom : Form
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, System.Text.StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        public static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll")]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll")]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        public delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        private LowLevelKeyboardProc _proc;
        private IntPtr _hookID = IntPtr.Zero;

        private const int WH_KEYBOARD_LL = 13; // Low-level keyboard hook
        private const int WM_KEYDOWN = 0x0100; // Key down message

        public Zoom()
        {
            InitializeComponent();
            _proc = HookCallback;
            _hookID = SetHook(_proc);
        }

        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (var curProcess = System.Diagnostics.Process.GetCurrentProcess())
            using (var curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                if (vkCode == (int)Keys.Tab) // Check if the "TAB" key is pressed
                {
                    guna2CheckBox3.Checked = !guna2CheckBox3.Checked; // Toggle the checkbox state
                }
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        private void Zoom_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnhookWindowsHookEx(_hookID); // Unhook the keyboard hook when the form is closing
        }

        private void clicker_Load(object sender, EventArgs e)
        {
           
        }

        private void guna2TrackBar1_Scroll(object sender, ScrollEventArgs e)
        {
            
        }

        private void guna2CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2CheckBox3.Checked)
            {
                timer1.Start();
            }
            else
            {
                timer1.Stop();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                timer1.Interval = 800 / Convert.ToInt32((guna2TrackBar1.Value).ToString("0"));
                if (guna2CheckBox3.Checked == true && IsMinecraftWindowActive())
                {
                    if (MouseButtons == MouseButtons.Left)
                    {
                        clickerclass.leftclick(1);
                    }
                }
            }
            catch (Exception) { }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            timer1.Stop();
            timer2.Stop();
        }

        private void guna2TrackBar1_Scroll_1(object sender, ScrollEventArgs e)
        {
            
            label1.Text = (guna2TrackBar1.Value).ToString("0.0") + "";
            
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            settings rr = new settings();
            rr.Show();
            
        }

        private void gunaTrackBar1_Scroll(object sender, ScrollEventArgs e)
        {
            label3.Text = (gunaTrackBar1.Value).ToString("0.0") + "";
        }

        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2CheckBox1.Checked)
            {
                timer2.Start();
            }
            else
            {
                timer2.Stop();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                timer2.Interval = 800 / Convert.ToInt32((gunaTrackBar1.Value).ToString("0"));
                if (guna2CheckBox1.Checked == true && IsMinecraftWindowActive())
                {
                    if (MouseButtons == MouseButtons.Right)
                    {
                        clickerclass.rightclick(1);
                    }
                }
            }
            catch (Exception) { }
        }

        private void guna2HtmlLabel3_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }

        private bool IsMinecraftWindowActive()
        {
            IntPtr handle = GetForegroundWindow();
            System.Text.StringBuilder windowText = new System.Text.StringBuilder(256);
            GetWindowText(handle, windowText, windowText.Capacity);
            return windowText.ToString().Contains("Minecraft");
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
    }
}

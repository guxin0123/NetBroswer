using Microsoft.Web.WebView2.Core;
using Microsoft.Win32;
using NetBroswer.Properties;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace NetBroswer
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }
        #region fields(菜单栏)
        private const int WM_SYSCOMMAND = 0X112;
        private const int MF_STRING = 0X0;
        private const int MF_SEPARATOR = 0X800;



        private const int MF_CHECKED = 0X8; //将复选标记属性设置为选定状态。
        private const int MF_UNCHECKED = 0x0;//将复选标记属性设置为清除状态。


        private enum SystemMenuItem : int
        {
            ShowInFront = 1,
            ShowInTaskbar = 2,
            HideNav = 3,
            About = 10,
        }
        #endregion




        #region OnHandleCreated 创建控件
        /// <summary>
        /// 创建控件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            nint hSysMenu = MenuUtils.GetSystemMenu(this.Handle, false);
            //加分割线
            MenuUtils.AppendMenu(hSysMenu, MF_SEPARATOR, 0, String.Empty);

            //加菜单项
            MenuUtils.AppendMenu(hSysMenu, TopMost ? MF_CHECKED : MF_STRING, (int)SystemMenuItem.ShowInFront, "总在最前");
            //加菜单项
            MenuUtils.AppendMenu(hSysMenu, ShowInTaskbar ? MF_CHECKED : MF_STRING, (int)SystemMenuItem.ShowInTaskbar, "任务栏显示");
            //加菜单项
            MenuUtils.AppendMenu(hSysMenu, tableLayoutPanel1.RowStyles[0].Height != 0 ? MF_CHECKED : MF_STRING, (int)SystemMenuItem.HideNav, "导航栏");


            ////加分割线
            //MenuUtils.AppendMenu(hSysMenu, MF_SEPARATOR, 0, String.Empty);

            ////加菜单项
            //MenuUtils.AppendMenu(hSysMenu, MF_STRING, (int)SystemMenuItem.About, "关于");
        }
        #endregion

        #region WndProc 处理 Windows 消息
        /// <summary>
        /// 处理 Windows 消息
        /// </summary>
        /// <param name="e"></param>
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_SYSCOMMAND)
            {

                nint hMenu;

                switch ((SystemMenuItem)m.WParam)
                {
                    case SystemMenuItem.ShowInFront:
                        hMenu = MenuUtils.GetSystemMenu(Handle, false);
                        if (TopMost)
                        {
                            MenuUtils.CheckMenuItem(hMenu, (int)SystemMenuItem.ShowInFront, MF_UNCHECKED);
                            TopMost = false;

                        }
                        else
                        {
                            MenuUtils.CheckMenuItem(hMenu, (int)SystemMenuItem.ShowInFront, MF_CHECKED);
                            TopMost = true;
                        }
                        RegistryHelper.SetInt(REG_NAME, "TopMost", TopMost ? 1 : 0);
                        break;
                    case SystemMenuItem.ShowInTaskbar:

                        ShowInTaskbar = !ShowInTaskbar;

                        break;
                    case SystemMenuItem.HideNav:
                        hMenu = MenuUtils.GetSystemMenu(Handle, false);
                        if (tableLayoutPanel1.RowStyles[0].Height == 0)
                        {
                            tableLayoutPanel1.RowStyles[0] = new RowStyle(SizeType.Absolute, 40F);
                            MenuUtils.CheckMenuItem(hMenu, (int)SystemMenuItem.HideNav, MF_CHECKED);
                        }
                        else
                        {
                            tableLayoutPanel1.RowStyles[0] = new RowStyle(SizeType.Absolute, 0);
                            MenuUtils.CheckMenuItem(hMenu, (int)SystemMenuItem.HideNav, MF_UNCHECKED);
                        }



                        break;
                    case SystemMenuItem.About:
                        MessageBox.Show("About");
                        break;
                }
            }
        }
        #endregion



        public string REG_NAME = @"HKEY_CURRENT_USER\Software\NetBroswer";

        private void Form1_Load(object sender, EventArgs e)
        {
            int FrmOpacity = RegistryHelper.GetInt(REG_NAME, "Opacity") ?? 100;
            trackBar1.Value = FrmOpacity;
            int FrmTopMost = RegistryHelper.GetInt(REG_NAME, "TopMost") ?? 0;
            TopMost = FrmTopMost == 1;
            MenuUtils.CheckMenuItem(MenuUtils.GetSystemMenu(this.Handle, false), 1, TopMost ? MF_CHECKED : MF_UNCHECKED);

            string lastVisit = RegistryHelper.GetString(REG_NAME, "LastVisit");
            lastVisit = string.IsNullOrEmpty(lastVisit) ? "baidu.com" : lastVisit;


            webView21.SourceChanged += WebView21_SourceChanged;
            webView21.NavigationStarting += WebView21_NavigationStarting;
            webView21.NavigationCompleted += WebView21_NavigationCompleted;

            viewUrl(lastVisit);
        }

        private void WebView21_NavigationCompleted(object? sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
        {
            p_go.Image = Resources.navigation;

            //throw new NotImplementedException();
        }

        private void WebView21_NavigationStarting(object? sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationStartingEventArgs e)
        {
            p_go.Image = Resources.loading;
            //throw new NotImplementedException();
        }

        private void WebView21_SourceChanged(object? sender, Microsoft.Web.WebView2.Core.CoreWebView2SourceChangedEventArgs e)
        {

            t_url.Text = webView21.Source.ToString();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            this.Opacity = trackBar1.Value / 100F;
            RegistryHelper.SetInt(REG_NAME, "Opacity", trackBar1.Value);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            viewUrl(t_url.Text);
        }
        private void viewUrl(string url)
        {
            if (!(url.StartsWith("http://") || url.StartsWith("https://")))
            {
                url = "https://" + url;
            }
            webView21.Source = new Uri(url);
            RegistryHelper.SetString(REG_NAME, "LastVisit", url);

        }

        private void t_url_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                p_go.Focus();
                button4_Click(null, null);

            }
        }

        private void p_go_MouseEnter(object sender, EventArgs e)
        {
            p_go.BackColor = SystemColors.ActiveCaption;
        }

        private void p_go_MouseLeave(object sender, EventArgs e)
        {
            p_go.BackColor = SystemColors.Control;
        }

        private void b_refish_Click(object sender, EventArgs e)
        {
            webView21.Reload();
        }

        private void b_back_Click(object sender, EventArgs e)
        {
            webView21.GoBack();
        }

        private void b_forward_Click(object sender, EventArgs e)
        {
            webView21.GoForward();

        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            webView21.Dispose();
        }

        private void webView21_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            webView21.CoreWebView2.NewWindowRequested += CoreWebView2_NewWindowRequested;
        }
        private void CoreWebView2_NewWindowRequested(object sender, CoreWebView2NewWindowRequestedEventArgs e)
        {
            String url = e.Uri.ToString();
            if (!url.Contains("oauth"))
            {
                webView21.Source = new Uri(url);
                e.Handled = true;
            }

        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Visible = !this.Visible;
            //this.Hide();
        }
    }
}

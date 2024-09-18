namespace NetBroswer
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            b_refish = new Button();
            b_forward = new Button();
            t_url = new TextBox();
            b_back = new Button();
            trackBar1 = new TrackBar();
            p_go = new PictureBox();
            notifyIcon1 = new NotifyIcon(components);
            ((System.ComponentModel.ISupportInitialize)webView21).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)p_go).BeginInit();
            SuspendLayout();
            // 
            // webView21
            // 
            webView21.AllowExternalDrop = true;
            webView21.CreationProperties = null;
            webView21.DefaultBackgroundColor = Color.White;
            webView21.Dock = DockStyle.Fill;
            webView21.Location = new Point(2, 37);
            webView21.Margin = new Padding(2, 3, 2, 3);
            webView21.Name = "webView21";
            webView21.Size = new Size(618, 342);
            webView21.Source = new Uri("https://www.baidu.com", UriKind.Absolute);
            webView21.TabIndex = 0;
            webView21.ZoomFactor = 1D;
            webView21.CoreWebView2InitializationCompleted += webView21_CoreWebView2InitializationCompleted;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(webView21, 0, 1);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(2, 3, 2, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(622, 382);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 6;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 31F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 31F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 31F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 47F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 93F));
            tableLayoutPanel2.Controls.Add(b_refish, 2, 0);
            tableLayoutPanel2.Controls.Add(b_forward, 1, 0);
            tableLayoutPanel2.Controls.Add(t_url, 3, 0);
            tableLayoutPanel2.Controls.Add(b_back, 0, 0);
            tableLayoutPanel2.Controls.Add(trackBar1, 5, 0);
            tableLayoutPanel2.Controls.Add(p_go, 4, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Margin = new Padding(0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(622, 34);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // b_refish
            // 
            b_refish.BackgroundImage = Properties.Resources.refresh;
            b_refish.BackgroundImageLayout = ImageLayout.Zoom;
            b_refish.Dock = DockStyle.Fill;
            b_refish.Location = new Point(64, 3);
            b_refish.Margin = new Padding(2, 3, 2, 3);
            b_refish.Name = "b_refish";
            b_refish.Size = new Size(27, 28);
            b_refish.TabIndex = 3;
            b_refish.UseVisualStyleBackColor = true;
            b_refish.Click += b_refish_Click;
            // 
            // b_forward
            // 
            b_forward.BackgroundImage = Properties.Resources.direction_right;
            b_forward.BackgroundImageLayout = ImageLayout.Zoom;
            b_forward.Dock = DockStyle.Fill;
            b_forward.Location = new Point(33, 3);
            b_forward.Margin = new Padding(2, 3, 2, 3);
            b_forward.Name = "b_forward";
            b_forward.Size = new Size(27, 28);
            b_forward.TabIndex = 2;
            b_forward.UseVisualStyleBackColor = true;
            b_forward.Click += b_forward_Click;
            // 
            // t_url
            // 
            t_url.Dock = DockStyle.Fill;
            t_url.Location = new Point(95, 7);
            t_url.Margin = new Padding(2, 7, 2, 3);
            t_url.Name = "t_url";
            t_url.Size = new Size(385, 23);
            t_url.TabIndex = 0;
            t_url.KeyPress += t_url_KeyPress;
            // 
            // b_back
            // 
            b_back.BackgroundImage = Properties.Resources.direction_left;
            b_back.BackgroundImageLayout = ImageLayout.Zoom;
            b_back.Dock = DockStyle.Fill;
            b_back.Location = new Point(2, 3);
            b_back.Margin = new Padding(2, 3, 2, 3);
            b_back.Name = "b_back";
            b_back.Size = new Size(27, 28);
            b_back.TabIndex = 1;
            b_back.UseVisualStyleBackColor = true;
            b_back.Click += b_back_Click;
            // 
            // trackBar1
            // 
            trackBar1.LargeChange = 20;
            trackBar1.Location = new Point(531, 3);
            trackBar1.Margin = new Padding(2, 3, 2, 3);
            trackBar1.Maximum = 100;
            trackBar1.Minimum = 5;
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(89, 28);
            trackBar1.SmallChange = 10;
            trackBar1.TabIndex = 5;
            trackBar1.TickFrequency = 20;
            trackBar1.Value = 100;
            trackBar1.ValueChanged += trackBar1_ValueChanged;
            // 
            // p_go
            // 
            p_go.BorderStyle = BorderStyle.FixedSingle;
            p_go.Dock = DockStyle.Fill;
            p_go.Image = Properties.Resources.navigation;
            p_go.Location = new Point(484, 3);
            p_go.Margin = new Padding(2, 3, 2, 3);
            p_go.Name = "p_go";
            p_go.Size = new Size(43, 28);
            p_go.SizeMode = PictureBoxSizeMode.StretchImage;
            p_go.TabIndex = 6;
            p_go.TabStop = false;
            p_go.Click += button4_Click;
            p_go.MouseEnter += p_go_MouseEnter;
            p_go.MouseLeave += p_go_MouseLeave;
            // 
            // notifyIcon1
            // 
            notifyIcon1.Icon = (Icon)resources.GetObject("notifyIcon1.Icon");
            notifyIcon1.Text = "NetBroswer";
            notifyIcon1.Visible = true;
            notifyIcon1.MouseDoubleClick += notifyIcon1_MouseDoubleClick;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(622, 382);
            Controls.Add(tableLayoutPanel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(2, 3, 2, 3);
            Name = "FormMain";
            Text = "NetBroswer";
            FormClosing += FormMain_FormClosing;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)webView21).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            ((System.ComponentModel.ISupportInitialize)p_go).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private TextBox t_url;
        private Button b_refish;
        private Button b_forward;
        private Button b_back;
        private TrackBar trackBar1;
        private PictureBox p_go;
        private NotifyIcon notifyIcon1;
    }
}

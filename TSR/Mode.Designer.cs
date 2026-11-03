namespace TSR
{
    partial class Mode
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
            components = new System.ComponentModel.Container();
            fade = new System.Windows.Forms.Timer(components);
            fadeout = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // fade
            // 
            fade.Interval = 50;
            fade.Tick += fade_Tick;
            // 
            // fadeout
            // 
            fadeout.Interval = 50;
            fadeout.Tick += fadeout_Tick;
            // 
            // Mode
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Mode";
            Text = "Mode";
            WindowState = FormWindowState.Maximized;
            FormClosed += Mode_FormClosed;
            Load += Mode_Load;
            Paint += Mode_Paint;
            KeyDown += Mode_KeyDown;
            MouseDown += Mode_MouseDown;
            Resize += Mode_Resize;
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer fade;
        private System.Windows.Forms.Timer fadeout;
    }
}
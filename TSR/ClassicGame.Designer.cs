namespace TSR
{
    partial class ClassicGame
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
            Road_timer = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // Road_timer
            // 
            Road_timer.Tick += Road_timer_Tick;
            // 
            // ClassicGame
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            FormBorderStyle = FormBorderStyle.None;
            KeyPreview = true;
            Name = "ClassicGame";
            Text = "ClassicGame";
            WindowState = FormWindowState.Maximized;
            FormClosed += ClassicGame_FormClosed;
            Load += ClassicGame_Load;
            Paint += ClassicGame_Paint;
            KeyDown += ClassicGame_KeyDown;
            KeyUp += ClassicGame_KeyUp;
            MouseDown += ClassicGame_MouseDown;
            Resize += ClassicGame_Resize;
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer Road_timer;
    }
}
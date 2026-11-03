namespace TSR
{
    partial class Game
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
            // Game
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Game";
            Text = "Game";
            WindowState = FormWindowState.Maximized;
            FormClosed += Game_FormClosed;
            Load += Game_Load;
            Paint += Game_Paint;
            KeyDown += Game_KeyDown;
            KeyUp += Game_KeyUp;
            MouseDown += Game_MouseDown;
            Resize += Game_Resize;
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer Road_timer;
    }
}
namespace ChessClient
{
    partial class Form1
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
            panel1 = new Panel();
            ChessPanel = new Panel();
            label2 = new Label();
            textBox1 = new TextBox();
            label1 = new Label();
            button1 = new Button();
            timer1 = new System.Windows.Forms.Timer(components);
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.AutoSize = true;
            panel1.BackColor = SystemColors.ActiveBorder;
            panel1.Controls.Add(ChessPanel);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(button1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(584, 461);
            panel1.TabIndex = 0;
            // 
            // ChessPanel
            // 
            ChessPanel.BackColor = Color.FromArgb(224, 224, 224);
            ChessPanel.Cursor = Cursors.Cross;
            ChessPanel.Dock = DockStyle.Fill;
            ChessPanel.Location = new Point(0, 0);
            ChessPanel.Name = "ChessPanel";
            ChessPanel.Size = new Size(584, 461);
            ChessPanel.TabIndex = 5;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(459, 437);
            label2.Name = "label2";
            label2.Size = new Size(113, 15);
            label2.TabIndex = 3;
            label2.Text = "Made by Vitor Novo";
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBox1.BorderStyle = BorderStyle.FixedSingle;
            textBox1.Location = new Point(96, 153);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(151, 23);
            textBox1.TabIndex = 2;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Verdana", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(96, 95);
            label1.Name = "label1";
            label1.Size = new Size(114, 35);
            label1.TabIndex = 0;
            label1.Text = "label1";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            button1.AutoSize = true;
            button1.Location = new Point(253, 153);
            button1.Name = "button1";
            button1.Size = new Size(117, 25);
            button1.TabIndex = 1;
            button1.Text = "Submeter";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 120;
            timer1.Tick += UpdateMethod;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 461);
            Controls.Add(panel1);
            MaximumSize = new Size(600, 500);
            MinimumSize = new Size(600, 500);
            Name = "Form1";
            Text = "Multiplayer Chess Game";
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private Button button1;
        private TextBox textBox1;
        private System.Windows.Forms.Timer timer1;
        private Label label2;
        private Panel ChessPanel;
    }
}

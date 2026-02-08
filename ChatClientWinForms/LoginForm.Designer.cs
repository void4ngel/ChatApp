namespace ChatClientWinForms
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            buttonLogin = new Button();
            groupBox1 = new GroupBox();
            label2 = new Label();
            label1 = new Label();
            textBoxPort = new TextBox();
            textBoxIP = new TextBox();
            groupBox2 = new GroupBox();
            label3 = new Label();
            label4 = new Label();
            comboBoxNickColor = new ComboBox();
            textBoxUsername = new TextBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // buttonLogin
            // 
            buttonLogin.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            buttonLogin.Location = new Point(80, 289);
            buttonLogin.Name = "buttonLogin";
            buttonLogin.Size = new Size(289, 59);
            buttonLogin.TabIndex = 0;
            buttonLogin.Text = "OK";
            buttonLogin.UseVisualStyleBackColor = true;
            buttonLogin.Click += buttonLogin_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(textBoxPort);
            groupBox1.Controls.Add(textBoxIP);
            groupBox1.Location = new Point(18, 34);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(405, 102);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Server";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(8, 61);
            label2.Name = "label2";
            label2.Size = new Size(29, 15);
            label2.TabIndex = 3;
            label2.Text = "Port";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(8, 27);
            label1.Name = "label1";
            label1.Size = new Size(55, 15);
            label1.TabIndex = 2;
            label1.Text = "IP Adress";
            // 
            // textBoxPort
            // 
            textBoxPort.Location = new Point(124, 61);
            textBoxPort.Name = "textBoxPort";
            textBoxPort.Size = new Size(267, 23);
            textBoxPort.TabIndex = 1;
            textBoxPort.Text = "8888";
            // 
            // textBoxIP
            // 
            textBoxIP.Location = new Point(124, 19);
            textBoxIP.Name = "textBoxIP";
            textBoxIP.Size = new Size(267, 23);
            textBoxIP.TabIndex = 0;
            textBoxIP.Text = "127.0.0.1";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(comboBoxNickColor);
            groupBox2.Controls.Add(textBoxUsername);
            groupBox2.Location = new Point(20, 159);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(405, 101);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "User Profile";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 30);
            label3.Name = "label3";
            label3.Size = new Size(60, 15);
            label3.TabIndex = 4;
            label3.Text = "Username";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 64);
            label4.Name = "label4";
            label4.Size = new Size(71, 15);
            label4.TabIndex = 5;
            label4.Text = "Name Color";
            // 
            // comboBoxNickColor
            // 
            comboBoxNickColor.FormattingEnabled = true;
            comboBoxNickColor.Location = new Point(123, 61);
            comboBoxNickColor.Name = "comboBoxNickColor";
            comboBoxNickColor.Size = new Size(266, 23);
            comboBoxNickColor.TabIndex = 2;
            // 
            // textBoxUsername
            // 
            textBoxUsername.Location = new Point(122, 22);
            textBoxUsername.Name = "textBoxUsername";
            textBoxUsername.Size = new Size(267, 23);
            textBoxUsername.TabIndex = 1;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(444, 369);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(buttonLogin);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ChatApp - Connect to...";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button buttonLogin;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label3;
        private Label label2;
        private Label label1;
        private TextBox textBoxPort;
        private TextBox textBoxIP;
        private Label label4;
        private ComboBox comboBoxNickColor;
        private TextBox textBoxUsername;
    }
}

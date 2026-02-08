using System.Reflection;

namespace ChatClientWinForms
{
    partial class ChatForm
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
            buttonExit = new Button();
            labelNickname = new Label();
            richTextBoxChat = new RichTextBox();
            labelUsers = new Label();
            listBoxUsers = new ListBox();
            textBoxMsgInput = new TextBox();
            buttonSendMsg = new Button();
            comboBox1 = new ComboBox();
            groupBox1 = new GroupBox();
            comboBoxPrivateUser = new ComboBox();
            radioButtonPrivateMsg = new RadioButton();
            radioButtonPublicMsg = new RadioButton();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // buttonExit
            // 
            buttonExit.Cursor = Cursors.Hand;
            buttonExit.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            buttonExit.Location = new Point(12, 7);
            buttonExit.Name = "buttonExit";
            buttonExit.Size = new Size(70, 28);
            buttonExit.TabIndex = 0;
            buttonExit.Text = "Exit";
            buttonExit.TextAlign = ContentAlignment.TopCenter;
            buttonExit.UseVisualStyleBackColor = true;
            buttonExit.Click += buttonExit_Click;
            // 
            // labelNickname
            // 
            labelNickname.AutoSize = true;
            labelNickname.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelNickname.Location = new Point(314, 2);
            labelNickname.Name = "labelNickname";
            labelNickname.Size = new Size(131, 32);
            labelNickname.TabIndex = 1;
            labelNickname.Text = "Logged as:";
            // 
            // richTextBoxChat
            // 
            richTextBoxChat.BackColor = SystemColors.ControlLightLight;
            richTextBoxChat.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            richTextBoxChat.Location = new Point(12, 40);
            richTextBoxChat.Name = "richTextBoxChat";
            richTextBoxChat.ReadOnly = true;
            richTextBoxChat.ScrollBars = RichTextBoxScrollBars.Vertical;
            richTextBoxChat.Size = new Size(718, 461);
            richTextBoxChat.TabIndex = 2;
            richTextBoxChat.Text = "";
            // 
            // labelUsers
            // 
            labelUsers.AutoSize = true;
            labelUsers.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point, 204);
            labelUsers.Location = new Point(805, 3);
            labelUsers.Name = "labelUsers";
            labelUsers.Size = new Size(111, 32);
            labelUsers.TabIndex = 3;
            labelUsers.Text = "Online: 0";
            // 
            // listBoxUsers
            // 
            listBoxUsers.FormattingEnabled = true;
            listBoxUsers.ItemHeight = 15;
            listBoxUsers.Location = new Point(736, 40);
            listBoxUsers.Name = "listBoxUsers";
            listBoxUsers.Size = new Size(234, 379);
            listBoxUsers.TabIndex = 4;
            // 
            // textBoxMsgInput
            // 
            textBoxMsgInput.BorderStyle = BorderStyle.FixedSingle;
            textBoxMsgInput.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            textBoxMsgInput.Location = new Point(12, 507);
            textBoxMsgInput.Multiline = true;
            textBoxMsgInput.Name = "textBoxMsgInput";
            textBoxMsgInput.ScrollBars = ScrollBars.Vertical;
            textBoxMsgInput.Size = new Size(718, 41);
            textBoxMsgInput.TabIndex = 5;
            // 
            // buttonSendMsg
            // 
            buttonSendMsg.Cursor = Cursors.Hand;
            buttonSendMsg.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            buttonSendMsg.Location = new Point(736, 507);
            buttonSendMsg.Name = "buttonSendMsg";
            buttonSendMsg.Size = new Size(234, 41);
            buttonSendMsg.TabIndex = 6;
            buttonSendMsg.Text = "Send";
            buttonSendMsg.UseVisualStyleBackColor = true;
            buttonSendMsg.Click += buttonSendMsg_Click;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(90, -296);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(135, 23);
            comboBox1.TabIndex = 7;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(comboBoxPrivateUser);
            groupBox1.Controls.Add(radioButtonPrivateMsg);
            groupBox1.Controls.Add(radioButtonPublicMsg);
            groupBox1.Controls.Add(comboBox1);
            groupBox1.Location = new Point(736, 419);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(234, 82);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            // 
            // comboBoxPrivateUser
            // 
            comboBoxPrivateUser.Enabled = false;
            comboBoxPrivateUser.FormattingEnabled = true;
            comboBoxPrivateUser.Location = new Point(90, 47);
            comboBoxPrivateUser.Name = "comboBoxPrivateUser";
            comboBoxPrivateUser.Size = new Size(121, 23);
            comboBoxPrivateUser.TabIndex = 10;
            // 
            // radioButtonPrivateMsg
            // 
            radioButtonPrivateMsg.AutoSize = true;
            radioButtonPrivateMsg.Location = new Point(6, 47);
            radioButtonPrivateMsg.Name = "radioButtonPrivateMsg";
            radioButtonPrivateMsg.Size = new Size(78, 19);
            radioButtonPrivateMsg.TabIndex = 9;
            radioButtonPrivateMsg.Text = "Private to:";
            radioButtonPrivateMsg.UseVisualStyleBackColor = true;
            // 
            // radioButtonPublicMsg
            // 
            radioButtonPublicMsg.AutoSize = true;
            radioButtonPublicMsg.Checked = true;
            radioButtonPublicMsg.Location = new Point(6, 22);
            radioButtonPublicMsg.Name = "radioButtonPublicMsg";
            radioButtonPublicMsg.Size = new Size(58, 19);
            radioButtonPublicMsg.TabIndex = 8;
            radioButtonPublicMsg.TabStop = true;
            radioButtonPublicMsg.Text = "Public";
            radioButtonPublicMsg.UseVisualStyleBackColor = true;
            // 
            // ChatForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(982, 572);
            Controls.Add(buttonSendMsg);
            Controls.Add(textBoxMsgInput);
            Controls.Add(listBoxUsers);
            Controls.Add(labelUsers);
            Controls.Add(richTextBoxChat);
            Controls.Add(labelNickname);
            Controls.Add(buttonExit);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "ChatForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ChatForm";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonExit;
        private Label labelNickname;
        private Label labelUsers;
        private ListBox listBoxUsers;
        private TextBox textBoxMsgInput;
        private Button buttonSendMsg;
        private ComboBox comboBox1;
        private GroupBox groupBox1;
        private RadioButton radioButtonPrivateMsg;
        public RadioButton radioButtonPublicMsg;
        private ComboBox comboBoxPrivateUser;
        private RichTextBox richTextBoxChat;
    }
}
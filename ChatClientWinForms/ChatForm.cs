using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace ChatClientWinForms
{
    public partial class ChatForm : Form
    {
        private ChatService _chatService;
        public ChatForm(ChatService chatService, string username)
        {
            InitializeComponent();
            _chatService = chatService;
            labelNickname.Text = $"Logged as {username}";
            richTextBoxChat.WordWrap = true;

            _chatService.MessageReceived += OnMessageReceived;
            _chatService.OnUsersUpdated += OnUsersUpdated;
            AddMessageToChat("Welcome!", Color.Blue);
        }
        private void OnMessageReceived(string message)
        {
            if (richTextBoxChat.InvokeRequired)
            {
                richTextBoxChat.Invoke(new Action(() => OnMessageReceived(message)));
                return;
            }
            AddMessageToChat(message, Color.Black);
        }
        private void OnUsersUpdated(List<string> usersList)
        {
            if (listBoxUsers.InvokeRequired)
            {
                listBoxUsers.Invoke(new Action(() => OnUsersUpdated(usersList)));
                return;
            }
            if (comboBoxPrivateUser.InvokeRequired)
            {
                comboBoxPrivateUser.Invoke(new Action(() => OnUsersUpdated(usersList)));
                return;
            }

            comboBoxPrivateUser.Enabled = usersList.Count > 1;

            listBoxUsers.Items.Clear();
            comboBoxPrivateUser.Items.Clear();

            foreach (string user in usersList)
            {
                listBoxUsers.Items.Add(user);
                comboBoxPrivateUser.Items.Add(user);
            }

            labelUsers.Text = $"Online: {usersList.Count}";
        }
        private void buttonSendMsg_Click(object sender, EventArgs e)
        {
            string text = textBoxMsgInput.Text;

            if (String.IsNullOrWhiteSpace(text))
            {
                textBoxMsgInput.Clear();
                return;
            }

            bool isPrivate = radioButtonPrivateMsg.Checked;
            string target = comboBoxPrivateUser.SelectedItem?.ToString();
            string myMessage;
            _chatService.SendMessage(text, isPrivate, target);
            if (isPrivate)
            {
                myMessage = $"[You] to {target}: {text}";
            }
            else
            {
                myMessage = $"[You] {text}";
            }
            AddMessageToChat(myMessage, Color.DarkSlateGray);
            textBoxMsgInput.Clear();
        }
        private void buttonExit_Click(object sender, EventArgs e)
        {
            _chatService.Disconnect();
            this.Close();
        }
        private void AddMessageToChat(string text, Color color)
        {
            richTextBoxChat.SelectionColor = color;
            richTextBoxChat.AppendText($"[{DateTime.Now:HH:mm}] {text}\n");
            richTextBoxChat.SelectionColor = richTextBoxChat.ForeColor;
        }
    }
}

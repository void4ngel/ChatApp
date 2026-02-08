namespace ChatClientWinForms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        private void LoginForm_Load(object sender, EventArgs e)
        {
            textBoxIP.Focus();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string ip = textBoxIP.Text.Trim();
            string username = textBoxUsername.Text.Trim();

            //валидация введенных данных
            if (!int.TryParse(textBoxPort.Text, out int port))
            {
                MessageBox.Show("Port must be between 1 to 65535", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Hand);
                textBoxPort.Focus();
                textBoxPort.SelectAll();
                return;
            }
            if (string.IsNullOrWhiteSpace(ip))
            {
                MessageBox.Show("Enter the server IP address", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Hand);
                textBoxIP.Focus();
                return;
            }
            if (username.Length < 3)
            {
                MessageBox.Show("Username must be at least 3 characters long", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Hand);
                textBoxUsername.Focus();
                return;
            }


            ChatService chatService = new ChatService();
            bool connected = chatService.Connect(ip, port, username);

            if (connected)
            {
                ChatForm chatForm = new ChatForm(chatService, username);

                chatForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Unable to connect. Check:\n1. Is the server running?\n2. Is the IP and port correct?\n3. Is the network available?",
                "Connection error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxIP.Focus();
                return;
            }
        }

    }
}

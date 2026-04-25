using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Grow2Go.Models;
using Grow2Go.Repositories;

namespace Grow2Go.Views
{
    public partial class LoginForm : Form
    {
        private UserRepository userRepository = new UserRepository();
        private string selectedSignUpRole = string.Empty;

        public LoginForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.Resize += LoginForm_Resize;
            ShowSignInPanel();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            btnSignInMain.ForeColor = Color.White;
            guna2PanelButtons.FillColor = Color.White;
            guna2PanelButtons.BorderRadius = 25;
            ShowSignInPanel();
        }

        private void LoginForm_Resize(object sender, EventArgs e)
        {
            float fontSizeBtn = Math.Max(12f, pnlSignIn.Height * 0.03f);
            btnSignInMain.Font = new Font("Segoe UI", fontSizeBtn, FontStyle.Regular);
            label8.Font = new Font("Segoe UI", Math.Max(18f, pnlSignIn.Height * 0.05f), FontStyle.Bold);
            label1.Font = new Font("Segoe UI", Math.Max(14f, pnlSignIn.Height * 0.03f), FontStyle.Regular);
            lblTagline.Font = new Font("Segoe UI", Math.Max(14f, this.ClientSize.Height * 0.03f));
            lblFooter.Font = new Font("Segoe UI", Math.Max(18f, this.ClientSize.Height * 0.04f));
        }

        private void btnSignInMain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            using (Font f = new Font("Segoe UI", 24, FontStyle.Regular))
            {
                SizeF signSize = g.MeasureString("Sign", f);
                SizeF inSize = g.MeasureString("In", f);
                float totalWidth = signSize.Width + inSize.Width;
                float startX = (btnSignInMain.Width - totalWidth) / 2;
                float startY = (btnSignInMain.Height - signSize.Height) / 2;
                g.DrawString("Sign", f, Brushes.White, new PointF(startX, startY));
                g.DrawString("In", f, Brushes.YellowGreen, new PointF(startX + signSize.Width, startY));
            }
        }

        // THIS IS THE FIXED LOGIN LOGIC
        private void btnSignInMain_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter your email and password.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var user = userRepository.GetUserByEmailAndPassword(email, password);

            if (user == null)
            {
                MessageBox.Show("Invalid email or password.", "Login Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show(
                $"Welcome, {user.Name}! {user.Role} login is working.",
                "Login Successful",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            txtPassword.Clear();
        }

        private void pnlSignIn_Paint(object sender, PaintEventArgs e) { }

        private void ShowSignInPanel()
        {
            SuspendLayout();
            pnlSignUp1.Visible = false;
            pnlSignIn.Visible = true;
            pnlSignIn.BringToFront();
            pnlSignUpRoleSelection.Visible = true;
            pnlSignUpDetails.Visible = false;

            btnSignInToggle.FillColor = Color.OliveDrab;
            btnSignInToggle.ForeColor = Color.White;
            btnSignUpToggle.FillColor = Color.White;
            btnSignUpToggle.ForeColor = Color.DarkGray;

            guna2Button3.FillColor = Color.OliveDrab;
            guna2Button3.ForeColor = Color.White;
            guna2Button4.FillColor = Color.White;
            guna2Button4.ForeColor = Color.DarkGray;
            btnSignInToggleDetails.FillColor = Color.OliveDrab;
            btnSignInToggleDetails.ForeColor = Color.White;
            btnSignUpToggleDetails.FillColor = Color.White;
            btnSignUpToggleDetails.ForeColor = Color.DarkGray;
            ResumeLayout(true);
        }

        private void ShowSignUpPanel()
        {
            SuspendLayout();
            pnlSignIn.Visible = false;
            pnlSignUp1.Visible = true;
            pnlSignUp1.BringToFront();
            ShowSignUpRoleSelection();

            btnSignInToggle.FillColor = Color.White;
            btnSignInToggle.ForeColor = Color.DarkGray;
            btnSignUpToggle.FillColor = Color.OliveDrab;
            btnSignUpToggle.ForeColor = Color.White;

            guna2Button3.FillColor = Color.White;
            guna2Button3.ForeColor = Color.DarkGray;
            guna2Button4.FillColor = Color.OliveDrab;
            guna2Button4.ForeColor = Color.White;
            btnSignInToggleDetails.FillColor = Color.White;
            btnSignInToggleDetails.ForeColor = Color.DarkGray;
            btnSignUpToggleDetails.FillColor = Color.OliveDrab;
            btnSignUpToggleDetails.ForeColor = Color.White;
            ResumeLayout(true);
        }

        private void ShowSignUpRoleSelection()
        {
            pnlSignUpDetails.Visible = false;
            pnlSignUpRoleSelection.Visible = true;
            pnlSignUpRoleSelection.BringToFront();
        }

        private void ShowSignUpDetails(string role)
        {
            selectedSignUpRole = role;
            lblSelectedRole.Text = $"{role} Account";
            txtSignUpFullName.Clear();
            txtSignUpPhone.Clear();
            txtSignUpEmail.Clear();
            txtSignUpPassword.Clear();
            txtSignUpConfirmPassword.Clear();

            pnlSignUpRoleSelection.Visible = false;
            pnlSignUpDetails.Visible = true;
            pnlSignUpDetails.BringToFront();
        }

        private void btnSignInToggle_Click(object sender, EventArgs e)
        {
            ShowSignInPanel();
        }

        private void btnSignUpToggle_Click(object sender, EventArgs e)
        {
            ShowSignUpPanel();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            ShowSignInPanel();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            ShowSignUpPanel();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            ShowSignUpDetails("Customer");
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            ShowSignUpDetails("Farmer");
        }

        private void btnSignUpBack_Click(object sender, EventArgs e)
        {
            ShowSignUpRoleSelection();
        }

        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            string fullName = txtSignUpFullName.Text.Trim();
            string phoneNumber = txtSignUpPhone.Text.Trim();
            string email = txtSignUpEmail.Text.Trim();
            string password = txtSignUpPassword.Text.Trim();
            string confirmPassword = txtSignUpConfirmPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(selectedSignUpRole))
            {
                MessageBox.Show("Please choose Customer or Farmer first.", "Sign Up",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ShowSignUpRoleSelection();
                return;
            }

            if (string.IsNullOrWhiteSpace(fullName) ||
                string.IsNullOrWhiteSpace(phoneNumber) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("Please complete all sign up fields.", "Sign Up",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!email.Contains("@") || !email.Contains("."))
            {
                MessageBox.Show("Please enter a valid email address.", "Sign Up",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters long.", "Sign Up",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.", "Sign Up",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (userRepository.EmailExists(email))
            {
                MessageBox.Show("That email is already registered. Please sign in instead.", "Sign Up",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var user = new User
            {
                Name = fullName,
                Email = email,
                Password = password,
                Role = selectedSignUpRole.ToLower()
            };

            if (!userRepository.CreateUser(user))
            {
                MessageBox.Show("Unable to create account right now. Please try again.", "Sign Up",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show(
                $"{selectedSignUpRole} account created successfully. You can now sign in.",
                "Sign Up Successful",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            txtEmail.Text = email;
            txtPassword.Clear();
            ShowSignInPanel();
        }

        private void pnlSignUp1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2PanelButtons_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

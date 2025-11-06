namespace Appointment_Manager
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            username_textbox = new TextBox();
            password_textbox = new TextBox();
            Username = new Label();
            Password = new Label();
            AMS_Title = new Label();
            exit_btn = new Button();
            login_btn = new Button();
            dbLabel = new Label();
            dbSuccessProvider = new ErrorProvider(components);
            dbErrorProvider = new ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)dbSuccessProvider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dbErrorProvider).BeginInit();
            SuspendLayout();
            // 
            // username_textbox
            // 
            username_textbox.Location = new Point(125, 135);
            username_textbox.Name = "username_textbox";
            username_textbox.Size = new Size(173, 23);
            username_textbox.TabIndex = 0;
            // 
            // password_textbox
            // 
            password_textbox.Location = new Point(125, 212);
            password_textbox.Name = "password_textbox";
            password_textbox.Size = new Size(173, 23);
            password_textbox.TabIndex = 1;
            // 
            // Username
            // 
            Username.AutoSize = true;
            Username.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Username.Location = new Point(170, 112);
            Username.Name = "Username";
            Username.Size = new Size(78, 20);
            Username.TabIndex = 2;
            Username.Text = "Username";
            Username.Click += label1_Click;
            // 
            // Password
            // 
            Password.AutoSize = true;
            Password.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Password.Location = new Point(170, 189);
            Password.Name = "Password";
            Password.Size = new Size(73, 20);
            Password.TabIndex = 3;
            Password.Text = "Password";
            // 
            // AMS_Title
            // 
            AMS_Title.AutoSize = true;
            AMS_Title.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            AMS_Title.Location = new Point(38, 50);
            AMS_Title.Name = "AMS_Title";
            AMS_Title.Size = new Size(348, 30);
            AMS_Title.TabIndex = 4;
            AMS_Title.Text = "Appointment Management System";
            AMS_Title.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // exit_btn
            // 
            exit_btn.Location = new Point(322, 350);
            exit_btn.Name = "exit_btn";
            exit_btn.Size = new Size(86, 36);
            exit_btn.TabIndex = 5;
            exit_btn.Text = "Exit";
            exit_btn.UseVisualStyleBackColor = true;
            exit_btn.Click += exit_btn_Click;
            // 
            // login_btn
            // 
            login_btn.Location = new Point(170, 271);
            login_btn.Name = "login_btn";
            login_btn.Size = new Size(86, 36);
            login_btn.TabIndex = 6;
            login_btn.Text = "Login";
            login_btn.UseVisualStyleBackColor = true;
            login_btn.Click += login_btn_Click;
            // 
            // dbLabel
            // 
            dbLabel.AutoSize = true;
            dbLabel.Location = new Point(38, 9);
            dbLabel.Name = "dbLabel";
            dbLabel.Size = new Size(65, 15);
            dbLabel.TabIndex = 7;
            dbLabel.Text = "[DB Status]";
            // 
            // dbSuccessProvider
            // 
            dbSuccessProvider.ContainerControl = this;
            dbSuccessProvider.Icon = (Icon)resources.GetObject("dbSuccessProvider.Icon");
            // 
            // dbErrorProvider
            // 
            dbErrorProvider.ContainerControl = this;
            dbErrorProvider.Icon = (Icon)resources.GetObject("dbErrorProvider.Icon");
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(420, 398);
            Controls.Add(dbLabel);
            Controls.Add(login_btn);
            Controls.Add(exit_btn);
            Controls.Add(AMS_Title);
            Controls.Add(Password);
            Controls.Add(Username);
            Controls.Add(password_textbox);
            Controls.Add(username_textbox);
            Name = "Login";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login";
            ((System.ComponentModel.ISupportInitialize)dbSuccessProvider).EndInit();
            ((System.ComponentModel.ISupportInitialize)dbErrorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox username_textbox;
        private TextBox password_textbox;
        private Label Username;
        private Label Password;
        private Label AMS_Title;
        private Button exit_btn;
        private Button login_btn;
        private Label dbLabel;
        private ErrorProvider dbSuccessProvider;
        private ErrorProvider dbErrorProvider;
    }
}

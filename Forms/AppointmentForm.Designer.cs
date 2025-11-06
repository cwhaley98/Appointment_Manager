namespace Appointment_Manager.Forms
{
    partial class AppointmentForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AppointmentForm));
            cancel_btn = new Button();
            save_btn = new Button();
            AppointmentFormTitle = new Label();
            customerNameLabel = new Label();
            ConsultantNameLabel = new Label();
            DescriptionLabel = new Label();
            LocationLabel = new Label();
            VisitTypeLabel = new Label();
            AppointmentDayLabel = new Label();
            AppointmentTimeLabel = new Label();
            AppointmentDatePicker = new DateTimePicker();
            AppointmentTimeComboBox = new ComboBox();
            LocationComboBox = new ComboBox();
            VisitTypeComboBox = new ComboBox();
            DescriptionTextBox = new TextBox();
            ConsultantComboBox = new ComboBox();
            CustomerNameComboBox = new ComboBox();
            errorProvider = new ErrorProvider(components);
            titleLabel = new Label();
            titleTextBox = new TextBox();
            phoneLabel = new Label();
            contactLabel = new Label();
            UrlLabel = new Label();
            phoneTextBox = new TextBox();
            contactTextBox = new TextBox();
            urlTextBox = new TextBox();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // cancel_btn
            // 
            cancel_btn.Location = new Point(124, 570);
            cancel_btn.Name = "cancel_btn";
            cancel_btn.Size = new Size(112, 51);
            cancel_btn.TabIndex = 0;
            cancel_btn.Text = "Cancel";
            cancel_btn.UseVisualStyleBackColor = true;
            cancel_btn.Click += cancel_btn_Click;
            // 
            // save_btn
            // 
            save_btn.Location = new Point(305, 570);
            save_btn.Name = "save_btn";
            save_btn.Size = new Size(112, 51);
            save_btn.TabIndex = 1;
            save_btn.Text = "Save";
            save_btn.UseVisualStyleBackColor = true;
            save_btn.Click += save_btn_Click;
            // 
            // AppointmentFormTitle
            // 
            AppointmentFormTitle.AutoSize = true;
            AppointmentFormTitle.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            AppointmentFormTitle.Location = new Point(166, 22);
            AppointmentFormTitle.Name = "AppointmentFormTitle";
            AppointmentFormTitle.Size = new Size(196, 30);
            AppointmentFormTitle.TabIndex = 2;
            AppointmentFormTitle.Text = "[AppointmentTitle]";
            // 
            // customerNameLabel
            // 
            customerNameLabel.AutoSize = true;
            customerNameLabel.Location = new Point(65, 64);
            customerNameLabel.Name = "customerNameLabel";
            customerNameLabel.Size = new Size(94, 15);
            customerNameLabel.TabIndex = 3;
            customerNameLabel.Text = "Customer Name";
            // 
            // ConsultantNameLabel
            // 
            ConsultantNameLabel.AutoSize = true;
            ConsultantNameLabel.Location = new Point(65, 123);
            ConsultantNameLabel.Name = "ConsultantNameLabel";
            ConsultantNameLabel.Size = new Size(65, 15);
            ConsultantNameLabel.TabIndex = 4;
            ConsultantNameLabel.Text = "Consultant";
            // 
            // DescriptionLabel
            // 
            DescriptionLabel.AutoSize = true;
            DescriptionLabel.Location = new Point(65, 184);
            DescriptionLabel.Name = "DescriptionLabel";
            DescriptionLabel.Size = new Size(67, 15);
            DescriptionLabel.TabIndex = 5;
            DescriptionLabel.Text = "Description";
            // 
            // LocationLabel
            // 
            LocationLabel.AutoSize = true;
            LocationLabel.Location = new Point(124, 355);
            LocationLabel.Name = "LocationLabel";
            LocationLabel.Size = new Size(53, 15);
            LocationLabel.TabIndex = 6;
            LocationLabel.Text = "Location";
            // 
            // VisitTypeLabel
            // 
            VisitTypeLabel.AutoSize = true;
            VisitTypeLabel.Location = new Point(305, 355);
            VisitTypeLabel.Name = "VisitTypeLabel";
            VisitTypeLabel.Size = new Size(57, 15);
            VisitTypeLabel.TabIndex = 7;
            VisitTypeLabel.Text = "Visit Type";
            // 
            // AppointmentDayLabel
            // 
            AppointmentDayLabel.AutoSize = true;
            AppointmentDayLabel.Location = new Point(124, 421);
            AppointmentDayLabel.Name = "AppointmentDayLabel";
            AppointmentDayLabel.Size = new Size(101, 15);
            AppointmentDayLabel.TabIndex = 8;
            AppointmentDayLabel.Text = "Appointment Day";
            // 
            // AppointmentTimeLabel
            // 
            AppointmentTimeLabel.AutoSize = true;
            AppointmentTimeLabel.Location = new Point(124, 488);
            AppointmentTimeLabel.Name = "AppointmentTimeLabel";
            AppointmentTimeLabel.Size = new Size(108, 15);
            AppointmentTimeLabel.TabIndex = 9;
            AppointmentTimeLabel.Text = "Appointment Time";
            // 
            // AppointmentDatePicker
            // 
            AppointmentDatePicker.Location = new Point(124, 439);
            AppointmentDatePicker.MinDate = new DateTime(2010, 1, 1, 0, 0, 0, 0);
            AppointmentDatePicker.Name = "AppointmentDatePicker";
            AppointmentDatePicker.Size = new Size(316, 23);
            AppointmentDatePicker.TabIndex = 10;
            AppointmentDatePicker.ValueChanged += AppointmentDatePicker_ValueChanged;
            // 
            // AppointmentTimeComboBox
            // 
            AppointmentTimeComboBox.FormattingEnabled = true;
            AppointmentTimeComboBox.Location = new Point(124, 506);
            AppointmentTimeComboBox.Name = "AppointmentTimeComboBox";
            AppointmentTimeComboBox.Size = new Size(153, 23);
            AppointmentTimeComboBox.TabIndex = 11;
            AppointmentTimeComboBox.SelectedIndexChanged += AppointmentTimeComboBox_SelectedIndexChanged;
            // 
            // LocationComboBox
            // 
            LocationComboBox.FormattingEnabled = true;
            LocationComboBox.Location = new Point(124, 382);
            LocationComboBox.Name = "LocationComboBox";
            LocationComboBox.Size = new Size(136, 23);
            LocationComboBox.TabIndex = 12;
            LocationComboBox.SelectedIndexChanged += LocationComboBox_SelectedIndexChanged;
            // 
            // VisitTypeComboBox
            // 
            VisitTypeComboBox.FormattingEnabled = true;
            VisitTypeComboBox.Location = new Point(305, 382);
            VisitTypeComboBox.Name = "VisitTypeComboBox";
            VisitTypeComboBox.Size = new Size(135, 23);
            VisitTypeComboBox.TabIndex = 13;
            VisitTypeComboBox.SelectedIndexChanged += VisitTypeComboBox_SelectedIndexChanged;
            // 
            // DescriptionTextBox
            // 
            DescriptionTextBox.Location = new Point(65, 202);
            DescriptionTextBox.Name = "DescriptionTextBox";
            DescriptionTextBox.Size = new Size(388, 23);
            DescriptionTextBox.TabIndex = 14;
            DescriptionTextBox.TextChanged += DescriptionTextBox_TextChanged;
            // 
            // ConsultantComboBox
            // 
            ConsultantComboBox.FormattingEnabled = true;
            ConsultantComboBox.Location = new Point(65, 141);
            ConsultantComboBox.Name = "ConsultantComboBox";
            ConsultantComboBox.Size = new Size(388, 23);
            ConsultantComboBox.TabIndex = 15;
            ConsultantComboBox.SelectedIndexChanged += ConsultantComboBox_SelectedIndexChanged;
            // 
            // CustomerNameComboBox
            // 
            CustomerNameComboBox.FormattingEnabled = true;
            CustomerNameComboBox.Location = new Point(65, 82);
            CustomerNameComboBox.Name = "CustomerNameComboBox";
            CustomerNameComboBox.Size = new Size(212, 23);
            CustomerNameComboBox.TabIndex = 16;
            CustomerNameComboBox.SelectedIndexChanged += CustomerNameComboBox_SelectedIndexChanged;
            // 
            // errorProvider
            // 
            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProvider.ContainerControl = this;
            errorProvider.Icon = (Icon)resources.GetObject("errorProvider.Icon");
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(305, 64);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(30, 15);
            titleLabel.TabIndex = 17;
            titleLabel.Text = "Title";
            // 
            // titleTextBox
            // 
            titleTextBox.Location = new Point(305, 82);
            titleTextBox.Name = "titleTextBox";
            titleTextBox.Size = new Size(148, 23);
            titleTextBox.TabIndex = 18;
            // 
            // phoneLabel
            // 
            phoneLabel.AutoSize = true;
            phoneLabel.Location = new Point(65, 243);
            phoneLabel.Name = "phoneLabel";
            phoneLabel.Size = new Size(88, 15);
            phoneLabel.TabIndex = 19;
            phoneLabel.Text = "Phone Number";
            // 
            // contactLabel
            // 
            contactLabel.AutoSize = true;
            contactLabel.Location = new Point(278, 243);
            contactLabel.Name = "contactLabel";
            contactLabel.Size = new Size(49, 15);
            contactLabel.TabIndex = 20;
            contactLabel.Text = "Contact";
            // 
            // UrlLabel
            // 
            UrlLabel.AutoSize = true;
            UrlLabel.Location = new Point(65, 298);
            UrlLabel.Name = "UrlLabel";
            UrlLabel.Size = new Size(28, 15);
            UrlLabel.TabIndex = 21;
            UrlLabel.Text = "URL";
            // 
            // phoneTextBox
            // 
            phoneTextBox.Location = new Point(65, 261);
            phoneTextBox.Name = "phoneTextBox";
            phoneTextBox.Size = new Size(171, 23);
            phoneTextBox.TabIndex = 22;
            // 
            // contactTextBox
            // 
            contactTextBox.Location = new Point(278, 261);
            contactTextBox.Name = "contactTextBox";
            contactTextBox.Size = new Size(175, 23);
            contactTextBox.TabIndex = 23;
            // 
            // urlTextBox
            // 
            urlTextBox.Location = new Point(65, 316);
            urlTextBox.Name = "urlTextBox";
            urlTextBox.Size = new Size(388, 23);
            urlTextBox.TabIndex = 24;
            // 
            // AppointmentForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(558, 647);
            Controls.Add(urlTextBox);
            Controls.Add(contactTextBox);
            Controls.Add(phoneTextBox);
            Controls.Add(UrlLabel);
            Controls.Add(contactLabel);
            Controls.Add(phoneLabel);
            Controls.Add(titleTextBox);
            Controls.Add(titleLabel);
            Controls.Add(CustomerNameComboBox);
            Controls.Add(ConsultantComboBox);
            Controls.Add(DescriptionTextBox);
            Controls.Add(VisitTypeComboBox);
            Controls.Add(LocationComboBox);
            Controls.Add(AppointmentTimeComboBox);
            Controls.Add(AppointmentDatePicker);
            Controls.Add(AppointmentTimeLabel);
            Controls.Add(AppointmentDayLabel);
            Controls.Add(VisitTypeLabel);
            Controls.Add(LocationLabel);
            Controls.Add(DescriptionLabel);
            Controls.Add(ConsultantNameLabel);
            Controls.Add(customerNameLabel);
            Controls.Add(AppointmentFormTitle);
            Controls.Add(save_btn);
            Controls.Add(cancel_btn);
            Name = "AppointmentForm";
            Text = "Appointment";
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button cancel_btn;
        private Button save_btn;
        private Label AppointmentFormTitle;
        private Label customerNameLabel;
        private Label ConsultantNameLabel;
        private Label DescriptionLabel;
        private Label LocationLabel;
        private Label VisitTypeLabel;
        private Label AppointmentDayLabel;
        private Label AppointmentTimeLabel;
        private DateTimePicker AppointmentDatePicker;
        private ComboBox AppointmentTimeComboBox;
        private ComboBox LocationComboBox;
        private ComboBox VisitTypeComboBox;
        private TextBox DescriptionTextBox;
        private ComboBox ConsultantComboBox;
        private ComboBox CustomerNameComboBox;
        private ErrorProvider errorProvider;
        private TextBox titleTextBox;
        private Label titleLabel;
        private Label UrlLabel;
        private Label contactLabel;
        private Label phoneLabel;
        private TextBox urlTextBox;
        private TextBox contactTextBox;
        private TextBox phoneTextBox;
    }
}
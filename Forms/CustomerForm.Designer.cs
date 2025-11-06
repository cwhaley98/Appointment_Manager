namespace Appointment_Manager.Forms
{
    partial class CustomerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomerForm));
            CustomerFormTitle = new Label();
            name_label = new Label();
            addressLine1_label = new Label();
            city_label = new Label();
            postal_label = new Label();
            phoneNumber_label = new Label();
            name_textbox = new TextBox();
            address_textbox = new TextBox();
            city_textbox = new TextBox();
            postalcode_textbox = new TextBox();
            phone_textbox = new TextBox();
            cancel_btn = new Button();
            save_btn = new Button();
            country_label = new Label();
            country_textbox = new TextBox();
            errorProvider = new ErrorProvider(components);
            addressLine2_label = new Label();
            address2_textbox = new TextBox();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // CustomerFormTitle
            // 
            CustomerFormTitle.AutoSize = true;
            CustomerFormTitle.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            CustomerFormTitle.Location = new Point(181, 72);
            CustomerFormTitle.Name = "CustomerFormTitle";
            CustomerFormTitle.Size = new Size(158, 30);
            CustomerFormTitle.TabIndex = 0;
            CustomerFormTitle.Text = "[customerTitle]";
            CustomerFormTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // name_label
            // 
            name_label.AutoSize = true;
            name_label.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            name_label.Location = new Point(109, 134);
            name_label.Name = "name_label";
            name_label.Size = new Size(50, 20);
            name_label.TabIndex = 1;
            name_label.Text = "Name";
            // 
            // addressLine1_label
            // 
            addressLine1_label.AutoSize = true;
            addressLine1_label.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            addressLine1_label.Location = new Point(109, 200);
            addressLine1_label.Name = "addressLine1_label";
            addressLine1_label.Size = new Size(105, 20);
            addressLine1_label.TabIndex = 2;
            addressLine1_label.Text = "Address Line 1";
            // 
            // city_label
            // 
            city_label.AutoSize = true;
            city_label.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            city_label.Location = new Point(109, 321);
            city_label.Name = "city_label";
            city_label.Size = new Size(35, 20);
            city_label.TabIndex = 3;
            city_label.Text = "City";
            // 
            // postal_label
            // 
            postal_label.AutoSize = true;
            postal_label.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            postal_label.Location = new Point(313, 321);
            postal_label.Name = "postal_label";
            postal_label.Size = new Size(49, 20);
            postal_label.TabIndex = 4;
            postal_label.Text = "Postal";
            // 
            // phoneNumber_label
            // 
            phoneNumber_label.AutoSize = true;
            phoneNumber_label.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            phoneNumber_label.Location = new Point(109, 430);
            phoneNumber_label.Name = "phoneNumber_label";
            phoneNumber_label.Size = new Size(114, 20);
            phoneNumber_label.TabIndex = 5;
            phoneNumber_label.Text = "Phone Number";
            // 
            // name_textbox
            // 
            name_textbox.Location = new Point(109, 157);
            name_textbox.Name = "name_textbox";
            name_textbox.Size = new Size(339, 23);
            name_textbox.TabIndex = 6;
            name_textbox.TextChanged += name_textbox_TextChanged;
            // 
            // address_textbox
            // 
            address_textbox.Location = new Point(109, 223);
            address_textbox.Name = "address_textbox";
            address_textbox.Size = new Size(339, 23);
            address_textbox.TabIndex = 7;
            address_textbox.TextChanged += address_textbox_TextChanged;
            // 
            // city_textbox
            // 
            city_textbox.Location = new Point(109, 344);
            city_textbox.Name = "city_textbox";
            city_textbox.Size = new Size(144, 23);
            city_textbox.TabIndex = 8;
            city_textbox.TextChanged += city_textbox_TextChanged;
            // 
            // postalcode_textbox
            // 
            postalcode_textbox.Location = new Point(313, 344);
            postalcode_textbox.Name = "postalcode_textbox";
            postalcode_textbox.Size = new Size(135, 23);
            postalcode_textbox.TabIndex = 9;
            postalcode_textbox.TextChanged += postalcode_textbox_TextChanged;
            // 
            // phone_textbox
            // 
            phone_textbox.Location = new Point(109, 453);
            phone_textbox.Name = "phone_textbox";
            phone_textbox.Size = new Size(230, 23);
            phone_textbox.TabIndex = 10;
            phone_textbox.TextChanged += phone_textbox_TextChanged;
            // 
            // cancel_btn
            // 
            cancel_btn.Location = new Point(295, 518);
            cancel_btn.Name = "cancel_btn";
            cancel_btn.Size = new Size(105, 44);
            cancel_btn.TabIndex = 11;
            cancel_btn.Text = "Cancel";
            cancel_btn.UseVisualStyleBackColor = true;
            cancel_btn.Click += cancel_btn_Click;
            // 
            // save_btn
            // 
            save_btn.Location = new Point(416, 518);
            save_btn.Name = "save_btn";
            save_btn.Size = new Size(105, 44);
            save_btn.TabIndex = 12;
            save_btn.Text = "Save";
            save_btn.UseVisualStyleBackColor = true;
            save_btn.Click += save_btn_Click;
            // 
            // country_label
            // 
            country_label.AutoSize = true;
            country_label.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            country_label.Location = new Point(109, 379);
            country_label.Name = "country_label";
            country_label.Size = new Size(65, 20);
            country_label.TabIndex = 13;
            country_label.Text = "Country";
            // 
            // country_textbox
            // 
            country_textbox.Location = new Point(109, 402);
            country_textbox.Name = "country_textbox";
            country_textbox.Size = new Size(230, 23);
            country_textbox.TabIndex = 14;
            country_textbox.TextChanged += country_textbox_TextChanged;
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            errorProvider.Icon = (Icon)resources.GetObject("errorProvider.Icon");
            // 
            // addressLine2_label
            // 
            addressLine2_label.AutoSize = true;
            addressLine2_label.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            addressLine2_label.Location = new Point(109, 259);
            addressLine2_label.Name = "addressLine2_label";
            addressLine2_label.Size = new Size(107, 20);
            addressLine2_label.TabIndex = 15;
            addressLine2_label.Text = "Address Line 2";
            // 
            // address2_textbox
            // 
            address2_textbox.Location = new Point(109, 282);
            address2_textbox.Name = "address2_textbox";
            address2_textbox.Size = new Size(339, 23);
            address2_textbox.TabIndex = 16;
            address2_textbox.TextChanged += address2_textbox_TextChanged;
            // 
            // CustomerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(533, 582);
            Controls.Add(address2_textbox);
            Controls.Add(addressLine2_label);
            Controls.Add(country_textbox);
            Controls.Add(country_label);
            Controls.Add(save_btn);
            Controls.Add(cancel_btn);
            Controls.Add(phone_textbox);
            Controls.Add(postalcode_textbox);
            Controls.Add(city_textbox);
            Controls.Add(address_textbox);
            Controls.Add(name_textbox);
            Controls.Add(phoneNumber_label);
            Controls.Add(postal_label);
            Controls.Add(city_label);
            Controls.Add(addressLine1_label);
            Controls.Add(name_label);
            Controls.Add(CustomerFormTitle);
            Name = "CustomerForm";
            Text = "Customer Form";
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label CustomerFormTitle;
        private Label name_label;
        private Label addressLine1_label;
        private Label city_label;
        private Label postal_label;
        private Label phoneNumber_label;
        private TextBox name_textbox;
        private TextBox address_textbox;
        private TextBox city_textbox;
        private TextBox postalcode_textbox;
        private TextBox phone_textbox;
        private Button cancel_btn;
        private Button save_btn;
        private Label country_label;
        private TextBox country_textbox;
        private ErrorProvider errorProvider;
        private TextBox address2_textbox;
        private Label addressLine2_label;
    }
}
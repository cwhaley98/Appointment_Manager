namespace Appointment_Manager.Forms
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            menuStrip1 = new MenuStrip();
            CustomersTab = new ToolStripMenuItem();
            AppointmentsTab = new ToolStripMenuItem();
            ReportsMenuItem = new ToolStripMenuItem();
            appointmentTypesByMonthToolStripMenuItem = new ToolStripMenuItem();
            consultantSchedulesToolStripMenuItem = new ToolStripMenuItem();
            appointmentPerLocationToolStripMenuItem = new ToolStripMenuItem();
            LogOutMenuItem = new ToolStripMenuItem();
            add_btn = new Button();
            update_btn = new Button();
            delete_btn = new Button();
            mainDGV = new DataGridView();
            viewReport_btn = new Button();
            feedbackLabel = new Label();
            comboBoxMonths = new ComboBox();
            monthsLabel = new Label();
            comboBoxConsultants = new ComboBox();
            consultantsLabel = new Label();
            calendarView = new MonthCalendar();
            errorProvider = new ErrorProvider(components);
            successProvider = new ErrorProvider(components);
            showAll_btn = new Button();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mainDGV).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)successProvider).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { CustomersTab, AppointmentsTab, ReportsMenuItem, LogOutMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1397, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // CustomersTab
            // 
            CustomersTab.BackColor = SystemColors.ControlLight;
            CustomersTab.Name = "CustomersTab";
            CustomersTab.Size = new Size(76, 20);
            CustomersTab.Text = "Customers";
            CustomersTab.Click += CustomersTab_Click;
            // 
            // AppointmentsTab
            // 
            AppointmentsTab.BackColor = SystemColors.ControlLight;
            AppointmentsTab.Name = "AppointmentsTab";
            AppointmentsTab.Size = new Size(95, 20);
            AppointmentsTab.Text = "Appointments";
            AppointmentsTab.Click += AppointmentsTab_Click;
            // 
            // ReportsMenuItem
            // 
            ReportsMenuItem.DropDownItems.AddRange(new ToolStripItem[] { appointmentTypesByMonthToolStripMenuItem, consultantSchedulesToolStripMenuItem, appointmentPerLocationToolStripMenuItem });
            ReportsMenuItem.Name = "ReportsMenuItem";
            ReportsMenuItem.Size = new Size(59, 20);
            ReportsMenuItem.Text = "Reports";
            // 
            // appointmentTypesByMonthToolStripMenuItem
            // 
            appointmentTypesByMonthToolStripMenuItem.Name = "appointmentTypesByMonthToolStripMenuItem";
            appointmentTypesByMonthToolStripMenuItem.Size = new Size(233, 22);
            appointmentTypesByMonthToolStripMenuItem.Text = "Appointment Types by Month";
            // 
            // consultantSchedulesToolStripMenuItem
            // 
            consultantSchedulesToolStripMenuItem.Name = "consultantSchedulesToolStripMenuItem";
            consultantSchedulesToolStripMenuItem.Size = new Size(233, 22);
            consultantSchedulesToolStripMenuItem.Text = "Consultant Schedules";
            // 
            // appointmentPerLocationToolStripMenuItem
            // 
            appointmentPerLocationToolStripMenuItem.Name = "appointmentPerLocationToolStripMenuItem";
            appointmentPerLocationToolStripMenuItem.Size = new Size(233, 22);
            appointmentPerLocationToolStripMenuItem.Text = "Appointment per Location";
            // 
            // LogOutMenuItem
            // 
            LogOutMenuItem.Alignment = ToolStripItemAlignment.Right;
            LogOutMenuItem.Name = "LogOutMenuItem";
            LogOutMenuItem.RightToLeft = RightToLeft.No;
            LogOutMenuItem.Size = new Size(62, 20);
            LogOutMenuItem.Text = "Log Out";
            LogOutMenuItem.Click += LogOutMenuItem_Click;
            // 
            // add_btn
            // 
            add_btn.Location = new Point(12, 517);
            add_btn.Name = "add_btn";
            add_btn.Size = new Size(99, 48);
            add_btn.TabIndex = 2;
            add_btn.Text = "Add";
            add_btn.UseVisualStyleBackColor = true;
            add_btn.Click += add_btn_Click;
            // 
            // update_btn
            // 
            update_btn.Location = new Point(117, 517);
            update_btn.Name = "update_btn";
            update_btn.Size = new Size(99, 48);
            update_btn.TabIndex = 3;
            update_btn.Text = "Update";
            update_btn.UseVisualStyleBackColor = true;
            update_btn.Click += update_btn_Click;
            // 
            // delete_btn
            // 
            delete_btn.Location = new Point(222, 517);
            delete_btn.Name = "delete_btn";
            delete_btn.Size = new Size(99, 48);
            delete_btn.TabIndex = 4;
            delete_btn.Text = "Delete";
            delete_btn.UseVisualStyleBackColor = true;
            delete_btn.Click += delete_btn_Click;
            // 
            // mainDGV
            // 
            mainDGV.AllowUserToAddRows = false;
            mainDGV.AllowUserToDeleteRows = false;
            mainDGV.AllowUserToResizeColumns = false;
            mainDGV.AllowUserToResizeRows = false;
            mainDGV.BackgroundColor = SystemColors.ControlLightLight;
            mainDGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            mainDGV.Location = new Point(12, 27);
            mainDGV.MultiSelect = false;
            mainDGV.Name = "mainDGV";
            mainDGV.Size = new Size(1128, 480);
            mainDGV.TabIndex = 1;
            mainDGV.CellClick += mainDGV_CellClick;
            mainDGV.DataBindingComplete += mainDGV_DataBindingComplete;
            // 
            // viewReport_btn
            // 
            viewReport_btn.Location = new Point(1041, 513);
            viewReport_btn.Name = "viewReport_btn";
            viewReport_btn.Size = new Size(99, 48);
            viewReport_btn.TabIndex = 5;
            viewReport_btn.Text = "View Report";
            viewReport_btn.UseVisualStyleBackColor = true;
            viewReport_btn.Click += viewReport_btn_Click;
            // 
            // feedbackLabel
            // 
            feedbackLabel.AutoSize = true;
            feedbackLabel.Location = new Point(359, 534);
            feedbackLabel.Name = "feedbackLabel";
            feedbackLabel.Size = new Size(65, 15);
            feedbackLabel.TabIndex = 6;
            feedbackLabel.Text = "[Feedback]";
            // 
            // comboBoxMonths
            // 
            comboBoxMonths.FormattingEnabled = true;
            comboBoxMonths.Items.AddRange(new object[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" });
            comboBoxMonths.Location = new Point(863, 531);
            comboBoxMonths.Name = "comboBoxMonths";
            comboBoxMonths.Size = new Size(161, 23);
            comboBoxMonths.TabIndex = 9;
            comboBoxMonths.SelectedIndexChanged += comboBoxMonths_SelectedIndexChanged;
            // 
            // monthsLabel
            // 
            monthsLabel.AutoSize = true;
            monthsLabel.Location = new Point(920, 513);
            monthsLabel.Name = "monthsLabel";
            monthsLabel.Size = new Size(48, 15);
            monthsLabel.TabIndex = 10;
            monthsLabel.Text = "Months";
            // 
            // comboBoxConsultants
            // 
            comboBoxConsultants.FormattingEnabled = true;
            comboBoxConsultants.Items.AddRange(new object[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" });
            comboBoxConsultants.Location = new Point(863, 531);
            comboBoxConsultants.Name = "comboBoxConsultants";
            comboBoxConsultants.Size = new Size(161, 23);
            comboBoxConsultants.TabIndex = 11;
            comboBoxConsultants.SelectedIndexChanged += comboBoxConsultants_SelectedIndexChanged;
            // 
            // consultantsLabel
            // 
            consultantsLabel.AutoSize = true;
            consultantsLabel.Location = new Point(910, 513);
            consultantsLabel.Name = "consultantsLabel";
            consultantsLabel.Size = new Size(70, 15);
            consultantsLabel.TabIndex = 12;
            consultantsLabel.Text = "Consultants";
            // 
            // calendarView
            // 
            calendarView.Location = new Point(1152, 33);
            calendarView.Name = "calendarView";
            calendarView.TabIndex = 13;
            calendarView.DateSelected += calendarView_DateSelected;
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            errorProvider.Icon = (Icon)resources.GetObject("errorProvider.Icon");
            // 
            // successProvider
            // 
            successProvider.ContainerControl = this;
            successProvider.Icon = (Icon)resources.GetObject("successProvider.Icon");
            // 
            // showAll_btn
            // 
            showAll_btn.Location = new Point(1152, 207);
            showAll_btn.Name = "showAll_btn";
            showAll_btn.Size = new Size(108, 54);
            showAll_btn.TabIndex = 14;
            showAll_btn.Text = "Show All Appointments";
            showAll_btn.UseVisualStyleBackColor = true;
            showAll_btn.Click += showAll_btn_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1397, 581);
            Controls.Add(showAll_btn);
            Controls.Add(calendarView);
            Controls.Add(consultantsLabel);
            Controls.Add(comboBoxConsultants);
            Controls.Add(monthsLabel);
            Controls.Add(comboBoxMonths);
            Controls.Add(feedbackLabel);
            Controls.Add(viewReport_btn);
            Controls.Add(mainDGV);
            Controls.Add(delete_btn);
            Controls.Add(update_btn);
            Controls.Add(add_btn);
            Controls.Add(menuStrip1);
            Name = "MainForm";
            Text = "Appointment Scheduler";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)mainDGV).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ((System.ComponentModel.ISupportInitialize)successProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem CustomersTab;
        private ToolStripMenuItem AppointmentsTab;
        private ToolStripMenuItem ReportsMenuItem;
        private ToolStripMenuItem appointmentTypesByMonthToolStripMenuItem;
        private ToolStripMenuItem consultantSchedulesToolStripMenuItem;
        private ToolStripMenuItem appointmentPerLocationToolStripMenuItem;
        private ToolStripMenuItem LogOutMenuItem;
        private Button add_btn;
        private Button update_btn;
        private Button delete_btn;
        private DataGridView mainDGV;
        private Button viewReport_btn;
        private Label feedbackLabel;
        private ComboBox comboBoxMonths;
        private Label monthsLabel;
        private ComboBox comboBoxConsultants;
        private Label consultantsLabel;
        private MonthCalendar calendarView;
        private ErrorProvider errorProvider;
        private ErrorProvider successProvider;
        private Button showAll_btn;
    }
}
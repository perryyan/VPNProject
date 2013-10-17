namespace VPN
{
    partial class Form1
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
            this.serverRB = new System.Windows.Forms.RadioButton();
            this.clientRB = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.serverGroupBox = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.textServerSharedKey = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textServerPort = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.clientGroupBox = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.textClientSharedKey = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textClientPort = new System.Windows.Forms.TextBox();
            this.textClientIP = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.listMessageLink = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.listMessageReceived = new System.Windows.Forms.ListBox();
            this.textSendMessage = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.PrimeBaseBox = new System.Windows.Forms.GroupBox();
            this.gLabel = new System.Windows.Forms.Label();
            this.primeLabel = new System.Windows.Forms.Label();
            this.basetextbox = new System.Windows.Forms.TextBox();
            this.primetextbox = new System.Windows.Forms.TextBox();
            this.ContinueButton = new System.Windows.Forms.Button();
            this.ConsoleOutputBox = new System.Windows.Forms.ListBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.serverGroupBox.SuspendLayout();
            this.clientGroupBox.SuspendLayout();
            this.PrimeBaseBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // serverRB
            // 
            this.serverRB.AutoSize = true;
            this.serverRB.Checked = true;
            this.serverRB.Location = new System.Drawing.Point(6, 19);
            this.serverRB.Name = "serverRB";
            this.serverRB.Size = new System.Drawing.Size(56, 17);
            this.serverRB.TabIndex = 0;
            this.serverRB.TabStop = true;
            this.serverRB.Text = "Server";
            this.serverRB.UseVisualStyleBackColor = true;
            this.serverRB.CheckedChanged += new System.EventHandler(this.serverRB_CheckedChanged);
            // 
            // clientRB
            // 
            this.clientRB.AutoSize = true;
            this.clientRB.Location = new System.Drawing.Point(6, 42);
            this.clientRB.Name = "clientRB";
            this.clientRB.Size = new System.Drawing.Size(51, 17);
            this.clientRB.TabIndex = 1;
            this.clientRB.Text = "Client";
            this.clientRB.UseVisualStyleBackColor = true;
            this.clientRB.CheckedChanged += new System.EventHandler(this.clientRB_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.serverRB);
            this.groupBox1.Controls.Add(this.clientRB);
            this.groupBox1.Location = new System.Drawing.Point(642, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(65, 71);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Mode";
            // 
            // serverGroupBox
            // 
            this.serverGroupBox.Controls.Add(this.button3);
            this.serverGroupBox.Controls.Add(this.textServerSharedKey);
            this.serverGroupBox.Controls.Add(this.label2);
            this.serverGroupBox.Controls.Add(this.textServerPort);
            this.serverGroupBox.Controls.Add(this.label1);
            this.serverGroupBox.Location = new System.Drawing.Point(13, 13);
            this.serverGroupBox.Name = "serverGroupBox";
            this.serverGroupBox.Size = new System.Drawing.Size(300, 71);
            this.serverGroupBox.TabIndex = 3;
            this.serverGroupBox.TabStop = false;
            this.serverGroupBox.Text = "Server";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(219, 40);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 8;
            this.button3.Text = "Save";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textServerSharedKey
            // 
            this.textServerSharedKey.Location = new System.Drawing.Point(118, 42);
            this.textServerSharedKey.Name = "textServerSharedKey";
            this.textServerSharedKey.Size = new System.Drawing.Size(101, 20);
            this.textServerSharedKey.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Shared Secret Value";
            // 
            // textServerPort
            // 
            this.textServerPort.Location = new System.Drawing.Point(118, 16);
            this.textServerPort.Name = "textServerPort";
            this.textServerPort.Size = new System.Drawing.Size(176, 20);
            this.textServerPort.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Port";
            // 
            // clientGroupBox
            // 
            this.clientGroupBox.Controls.Add(this.button4);
            this.clientGroupBox.Controls.Add(this.textClientSharedKey);
            this.clientGroupBox.Controls.Add(this.label3);
            this.clientGroupBox.Controls.Add(this.label5);
            this.clientGroupBox.Controls.Add(this.textClientPort);
            this.clientGroupBox.Controls.Add(this.textClientIP);
            this.clientGroupBox.Controls.Add(this.label4);
            this.clientGroupBox.Location = new System.Drawing.Point(319, 13);
            this.clientGroupBox.Name = "clientGroupBox";
            this.clientGroupBox.Size = new System.Drawing.Size(300, 100);
            this.clientGroupBox.TabIndex = 4;
            this.clientGroupBox.TabStop = false;
            this.clientGroupBox.Text = "Client";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(235, 70);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(65, 23);
            this.button4.TabIndex = 12;
            this.button4.Text = "Save";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // textClientSharedKey
            // 
            this.textClientSharedKey.Location = new System.Drawing.Point(117, 70);
            this.textClientSharedKey.Name = "textClientSharedKey";
            this.textClientSharedKey.Size = new System.Drawing.Size(112, 20);
            this.textClientSharedKey.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "IP Address";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Shared Secret Value";
            // 
            // textClientPort
            // 
            this.textClientPort.Location = new System.Drawing.Point(117, 43);
            this.textClientPort.Name = "textClientPort";
            this.textClientPort.Size = new System.Drawing.Size(177, 20);
            this.textClientPort.TabIndex = 10;
            // 
            // textClientIP
            // 
            this.textClientIP.Location = new System.Drawing.Point(117, 14);
            this.textClientIP.Name = "textClientIP";
            this.textClientIP.Size = new System.Drawing.Size(177, 20);
            this.textClientIP.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Port";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(632, 90);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Connect";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listMessageLink
            // 
            this.listMessageLink.FormattingEnabled = true;
            this.listMessageLink.Location = new System.Drawing.Point(13, 191);
            this.listMessageLink.Name = "listMessageLink";
            this.listMessageLink.Size = new System.Drawing.Size(481, 121);
            this.listMessageLink.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 175);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(163, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Data Sent and Received on Link";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 327);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Data as Received";
            // 
            // listMessageReceived
            // 
            this.listMessageReceived.FormattingEnabled = true;
            this.listMessageReceived.Location = new System.Drawing.Point(13, 343);
            this.listMessageReceived.Name = "listMessageReceived";
            this.listMessageReceived.Size = new System.Drawing.Size(481, 147);
            this.listMessageReceived.TabIndex = 9;
            // 
            // textSendMessage
            // 
            this.textSendMessage.Location = new System.Drawing.Point(13, 499);
            this.textSendMessage.Name = "textSendMessage";
            this.textSendMessage.Size = new System.Drawing.Size(606, 20);
            this.textSendMessage.TabIndex = 10;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(632, 496);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 11;
            this.button2.Text = "Send";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // PrimeBaseBox
            // 
            this.PrimeBaseBox.Controls.Add(this.gLabel);
            this.PrimeBaseBox.Controls.Add(this.primeLabel);
            this.PrimeBaseBox.Controls.Add(this.basetextbox);
            this.PrimeBaseBox.Controls.Add(this.primetextbox);
            this.PrimeBaseBox.Location = new System.Drawing.Point(13, 102);
            this.PrimeBaseBox.Name = "PrimeBaseBox";
            this.PrimeBaseBox.Size = new System.Drawing.Size(160, 56);
            this.PrimeBaseBox.TabIndex = 12;
            this.PrimeBaseBox.TabStop = false;
            this.PrimeBaseBox.Visible = false;
            // 
            // gLabel
            // 
            this.gLabel.AutoSize = true;
            this.gLabel.Location = new System.Drawing.Point(6, 33);
            this.gLabel.Name = "gLabel";
            this.gLabel.Size = new System.Drawing.Size(13, 13);
            this.gLabel.TabIndex = 15;
            this.gLabel.Text = "g";
            this.gLabel.Click += new System.EventHandler(this.label8_Click_1);
            // 
            // primeLabel
            // 
            this.primeLabel.AutoSize = true;
            this.primeLabel.Location = new System.Drawing.Point(6, 11);
            this.primeLabel.Name = "primeLabel";
            this.primeLabel.Size = new System.Drawing.Size(13, 13);
            this.primeLabel.TabIndex = 14;
            this.primeLabel.Text = "p";
            this.primeLabel.Click += new System.EventHandler(this.label8_Click);
            // 
            // basetextbox
            // 
            this.basetextbox.Location = new System.Drawing.Point(28, 32);
            this.basetextbox.Name = "basetextbox";
            this.basetextbox.Size = new System.Drawing.Size(100, 20);
            this.basetextbox.TabIndex = 1;
            // 
            // primetextbox
            // 
            this.primetextbox.Location = new System.Drawing.Point(28, 9);
            this.primetextbox.Name = "primetextbox";
            this.primetextbox.Size = new System.Drawing.Size(100, 20);
            this.primetextbox.TabIndex = 0;
            // 
            // ContinueButton
            // 
            this.ContinueButton.Location = new System.Drawing.Point(554, 122);
            this.ContinueButton.Name = "ContinueButton";
            this.ContinueButton.Size = new System.Drawing.Size(153, 23);
            this.ContinueButton.TabIndex = 13;
            this.ContinueButton.Text = "Authenticate - Continue";
            this.ContinueButton.UseVisualStyleBackColor = true;
            this.ContinueButton.Click += new System.EventHandler(this.ContinueButton_Click);
            // 
            // ConsoleOutputBox
            // 
            this.ConsoleOutputBox.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ConsoleOutputBox.FormattingEnabled = true;
            this.ConsoleOutputBox.Location = new System.Drawing.Point(500, 191);
            this.ConsoleOutputBox.Name = "ConsoleOutputBox";
            this.ConsoleOutputBox.Size = new System.Drawing.Size(326, 290);
            this.ConsoleOutputBox.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(499, 175);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Console Output";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 534);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.ConsoleOutputBox);
            this.Controls.Add(this.ContinueButton);
            this.Controls.Add(this.PrimeBaseBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textSendMessage);
            this.Controls.Add(this.listMessageReceived);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.listMessageLink);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.clientGroupBox);
            this.Controls.Add(this.serverGroupBox);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "VPN";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.serverGroupBox.ResumeLayout(false);
            this.serverGroupBox.PerformLayout();
            this.clientGroupBox.ResumeLayout(false);
            this.clientGroupBox.PerformLayout();
            this.PrimeBaseBox.ResumeLayout(false);
            this.PrimeBaseBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton serverRB;
        private System.Windows.Forms.RadioButton clientRB;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox serverGroupBox;
        private System.Windows.Forms.GroupBox clientGroupBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textServerSharedKey;
        private System.Windows.Forms.TextBox textServerPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textClientIP;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textClientPort;
        private System.Windows.Forms.TextBox textClientSharedKey;
        private System.Windows.Forms.ListBox listMessageLink;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListBox listMessageReceived;
        private System.Windows.Forms.TextBox textSendMessage;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.GroupBox PrimeBaseBox;
        private System.Windows.Forms.Label primeLabel;
        private System.Windows.Forms.TextBox basetextbox;
        private System.Windows.Forms.TextBox primetextbox;
        private System.Windows.Forms.Label gLabel;
        private System.Windows.Forms.Button ContinueButton;
        private System.Windows.Forms.ListBox ConsoleOutputBox;
        private System.Windows.Forms.Label label8;
    }
}


namespace RouterClient
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
            this.WpisywanaNazwaDocelowego = new System.Windows.Forms.TextBox();
            this.NapiszCoChceszWyslac = new System.Windows.Forms.TextBox();
            this.PotwierdzNazwe = new System.Windows.Forms.Button();
            this.WyslijTresc = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.CoDostalem = new System.Windows.Forms.TextBox();
            this.WyslijOkresowoPrzycisk = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.CoWysylam = new System.Windows.Forms.TextBox();
            this.PrzestanWysylacPrzycisk = new System.Windows.Forms.Button();
            this.WyborPliku = new System.Windows.Forms.Button();
            this.PolaczZKablami = new System.Windows.Forms.Button();
            this.PotwierdzIP = new System.Windows.Forms.Button();
            this.IPDocelowegoWezla = new System.Windows.Forms.TextBox();
            this.ZazadajPolaczenia = new System.Windows.Forms.Button();
            this.ZakonczPolaczenie = new System.Windows.Forms.Button();
            this.NazwaPolaczenia = new System.Windows.Forms.TextBox();
            this.WymaganaPojemnosc = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // WpisywanaNazwaDocelowego
            // 
            this.WpisywanaNazwaDocelowego.Location = new System.Drawing.Point(12, 62);
            this.WpisywanaNazwaDocelowego.Name = "WpisywanaNazwaDocelowego";
            this.WpisywanaNazwaDocelowego.Size = new System.Drawing.Size(140, 20);
            this.WpisywanaNazwaDocelowego.TabIndex = 0;
            this.WpisywanaNazwaDocelowego.Text = "Nazwa wezla docelowego";
            this.WpisywanaNazwaDocelowego.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // NapiszCoChceszWyslac
            // 
            this.NapiszCoChceszWyslac.Location = new System.Drawing.Point(282, 14);
            this.NapiszCoChceszWyslac.Multiline = true;
            this.NapiszCoChceszWyslac.Name = "NapiszCoChceszWyslac";
            this.NapiszCoChceszWyslac.Size = new System.Drawing.Size(206, 50);
            this.NapiszCoChceszWyslac.TabIndex = 2;
            this.NapiszCoChceszWyslac.Text = "Napisz co chcesz wyslac.";
            this.NapiszCoChceszWyslac.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // PotwierdzNazwe
            // 
            this.PotwierdzNazwe.Location = new System.Drawing.Point(12, 88);
            this.PotwierdzNazwe.Name = "PotwierdzNazwe";
            this.PotwierdzNazwe.Size = new System.Drawing.Size(140, 23);
            this.PotwierdzNazwe.TabIndex = 3;
            this.PotwierdzNazwe.Text = "Potwierdz nazwe";
            this.PotwierdzNazwe.UseVisualStyleBackColor = true;
            this.PotwierdzNazwe.Click += new System.EventHandler(this.button1_Click);
            // 
            // WyslijTresc
            // 
            this.WyslijTresc.BackColor = System.Drawing.SystemColors.Highlight;
            this.WyslijTresc.ForeColor = System.Drawing.SystemColors.Control;
            this.WyslijTresc.Location = new System.Drawing.Point(157, 143);
            this.WyslijTresc.Name = "WyslijTresc";
            this.WyslijTresc.Size = new System.Drawing.Size(144, 42);
            this.WyslijTresc.TabIndex = 5;
            this.WyslijTresc.Text = "Wyslij tresc";
            this.WyslijTresc.UseVisualStyleBackColor = false;
            this.WyslijTresc.Click += new System.EventHandler(this.button3_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // CoDostalem
            // 
            this.CoDostalem.Location = new System.Drawing.Point(308, 172);
            this.CoDostalem.Multiline = true;
            this.CoDostalem.Name = "CoDostalem";
            this.CoDostalem.Size = new System.Drawing.Size(395, 60);
            this.CoDostalem.TabIndex = 6;
            this.CoDostalem.Text = "Otrzymane dane";
            this.CoDostalem.TextChanged += new System.EventHandler(this.textBox4_TextChanged_1);
            // 
            // WyslijOkresowoPrzycisk
            // 
            this.WyslijOkresowoPrzycisk.Location = new System.Drawing.Point(332, 121);
            this.WyslijOkresowoPrzycisk.Name = "WyslijOkresowoPrzycisk";
            this.WyslijOkresowoPrzycisk.Size = new System.Drawing.Size(75, 38);
            this.WyslijOkresowoPrzycisk.TabIndex = 7;
            this.WyslijOkresowoPrzycisk.Text = "Wyslij okresowo";
            this.WyslijOkresowoPrzycisk.UseVisualStyleBackColor = true;
            this.WyslijOkresowoPrzycisk.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(364, 88);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(124, 20);
            this.textBox2.TabIndex = 8;
            this.textBox2.Text = "Czestotliwosc wysylania";
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged_1);
            // 
            // CoWysylam
            // 
            this.CoWysylam.Location = new System.Drawing.Point(494, 32);
            this.CoWysylam.Multiline = true;
            this.CoWysylam.Name = "CoWysylam";
            this.CoWysylam.Size = new System.Drawing.Size(209, 111);
            this.CoWysylam.TabIndex = 9;
            this.CoWysylam.Text = "Co wysylam:";
            this.CoWysylam.TextChanged += new System.EventHandler(this.textBox5_TextChanged);
            // 
            // PrzestanWysylacPrzycisk
            // 
            this.PrzestanWysylacPrzycisk.Location = new System.Drawing.Point(413, 121);
            this.PrzestanWysylacPrzycisk.Name = "PrzestanWysylacPrzycisk";
            this.PrzestanWysylacPrzycisk.Size = new System.Drawing.Size(75, 37);
            this.PrzestanWysylacPrzycisk.TabIndex = 10;
            this.PrzestanWysylacPrzycisk.Text = "Przestan wysylac";
            this.PrzestanWysylacPrzycisk.UseVisualStyleBackColor = true;
            this.PrzestanWysylacPrzycisk.Click += new System.EventHandler(this.button4_Click_2);
            // 
            // WyborPliku
            // 
            this.WyborPliku.Location = new System.Drawing.Point(12, 12);
            this.WyborPliku.Name = "WyborPliku";
            this.WyborPliku.Size = new System.Drawing.Size(106, 41);
            this.WyborPliku.TabIndex = 11;
            this.WyborPliku.Text = "Wybierz plik konfiguracyjny";
            this.WyborPliku.UseVisualStyleBackColor = true;
            this.WyborPliku.Click += new System.EventHandler(this.button5_Click);
            // 
            // PolaczZKablami
            // 
            this.PolaczZKablami.Location = new System.Drawing.Point(124, 12);
            this.PolaczZKablami.Name = "PolaczZKablami";
            this.PolaczZKablami.Size = new System.Drawing.Size(140, 23);
            this.PolaczZKablami.TabIndex = 12;
            this.PolaczZKablami.Text = "Polacz z kablami";
            this.PolaczZKablami.UseVisualStyleBackColor = true;
            this.PolaczZKablami.Click += new System.EventHandler(this.button6_Click);
            // 
            // PotwierdzIP
            // 
            this.PotwierdzIP.Location = new System.Drawing.Point(157, 88);
            this.PotwierdzIP.Name = "PotwierdzIP";
            this.PotwierdzIP.Size = new System.Drawing.Size(118, 23);
            this.PotwierdzIP.TabIndex = 13;
            this.PotwierdzIP.Text = "Potwierdz IP";
            this.PotwierdzIP.UseVisualStyleBackColor = true;
            this.PotwierdzIP.Click += new System.EventHandler(this.button7_Click);
            // 
            // IPDocelowegoWezla
            // 
            this.IPDocelowegoWezla.Location = new System.Drawing.Point(158, 62);
            this.IPDocelowegoWezla.Name = "IPDocelowegoWezla";
            this.IPDocelowegoWezla.Size = new System.Drawing.Size(118, 20);
            this.IPDocelowegoWezla.TabIndex = 14;
            this.IPDocelowegoWezla.Text = "IP docelowego wezla";
            this.IPDocelowegoWezla.TextChanged += new System.EventHandler(this.textBox6_TextChanged);
            // 
            // ZazadajPolaczenia
            // 
            this.ZazadajPolaczenia.Location = new System.Drawing.Point(12, 143);
            this.ZazadajPolaczenia.Name = "ZazadajPolaczenia";
            this.ZazadajPolaczenia.Size = new System.Drawing.Size(106, 42);
            this.ZazadajPolaczenia.TabIndex = 15;
            this.ZazadajPolaczenia.Text = "Zazadaj polaczenia";
            this.ZazadajPolaczenia.UseVisualStyleBackColor = true;
            this.ZazadajPolaczenia.Click += new System.EventHandler(this.button8_Click);
            // 
            // ZakonczPolaczenie
            // 
            this.ZakonczPolaczenie.Location = new System.Drawing.Point(12, 191);
            this.ZakonczPolaczenie.Name = "ZakonczPolaczenie";
            this.ZakonczPolaczenie.Size = new System.Drawing.Size(106, 41);
            this.ZakonczPolaczenie.TabIndex = 16;
            this.ZakonczPolaczenie.Text = "Zakoncz polaczenie";
            this.ZakonczPolaczenie.UseVisualStyleBackColor = true;
            this.ZakonczPolaczenie.Click += new System.EventHandler(this.button9_Click);
            // 
            // NazwaPolaczenia
            // 
            this.NazwaPolaczenia.Location = new System.Drawing.Point(161, 117);
            this.NazwaPolaczenia.Name = "NazwaPolaczenia";
            this.NazwaPolaczenia.Size = new System.Drawing.Size(144, 20);
            this.NazwaPolaczenia.TabIndex = 17;
            this.NazwaPolaczenia.Text = "Nazwa polaczenia";
            this.NazwaPolaczenia.TextChanged += new System.EventHandler(this.nazwaPolaczenia_TextChanged);
            // 
            // WymaganaPojemnosc
            // 
            this.WymaganaPojemnosc.Location = new System.Drawing.Point(12, 117);
            this.WymaganaPojemnosc.Name = "WymaganaPojemnosc";
            this.WymaganaPojemnosc.Size = new System.Drawing.Size(143, 20);
            this.WymaganaPojemnosc.TabIndex = 18;
            this.WymaganaPojemnosc.Text = "2";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 244);
            this.Controls.Add(this.WymaganaPojemnosc);
            this.Controls.Add(this.NazwaPolaczenia);
            this.Controls.Add(this.ZakonczPolaczenie);
            this.Controls.Add(this.ZazadajPolaczenia);
            this.Controls.Add(this.IPDocelowegoWezla);
            this.Controls.Add(this.PotwierdzIP);
            this.Controls.Add(this.PolaczZKablami);
            this.Controls.Add(this.WyborPliku);
            this.Controls.Add(this.PrzestanWysylacPrzycisk);
            this.Controls.Add(this.CoWysylam);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.WyslijOkresowoPrzycisk);
            this.Controls.Add(this.CoDostalem);
            this.Controls.Add(this.WyslijTresc);
            this.Controls.Add(this.PotwierdzNazwe);
            this.Controls.Add(this.NapiszCoChceszWyslac);
            this.Controls.Add(this.WpisywanaNazwaDocelowego);
            this.Name = "Form1";
            this.Text = "Router Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox WpisywanaNazwaDocelowego;
        private System.Windows.Forms.TextBox NapiszCoChceszWyslac;
        private System.Windows.Forms.Button PotwierdzNazwe;
        private System.Windows.Forms.Button WyslijTresc;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox CoDostalem;
        private System.Windows.Forms.Button WyslijOkresowoPrzycisk;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox CoWysylam;
        private System.Windows.Forms.Button PrzestanWysylacPrzycisk;
        private System.Windows.Forms.Button WyborPliku;
        private System.Windows.Forms.Button PolaczZKablami;
        private System.Windows.Forms.Button PotwierdzIP;
        private System.Windows.Forms.TextBox IPDocelowegoWezla;
        private System.Windows.Forms.Button ZazadajPolaczenia;
        private System.Windows.Forms.Button ZakonczPolaczenie;
        private System.Windows.Forms.TextBox NazwaPolaczenia;
        private System.Windows.Forms.TextBox WymaganaPojemnosc;
    }
}


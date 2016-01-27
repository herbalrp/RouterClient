using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace RouterClient
{
    public partial class Form1 : Form
    {
        public string IPAdresata;

        public string filePath;
        public bool przestanWysylac = false;
        public string id;
        public TcpClient myClient = new TcpClient();
        public string IDadresata;
        public string tekstOkresowy;
        public int czestotliwosc;
        public string[] clientAddresses = new string[1024];
        private ServerClient client;
        public string textToSend;

        public Dictionary<string, bool> zestawianePolaczenia = new Dictionary<string, bool>();

        public Dictionary<string, DateTime> stanSasiadow = new Dictionary<string,DateTime>();
        public List<string> listaStraconych = new List<string>();
        public List<string> listaZywych = new List<string>();

        public bool czyZestawione = false;

        public string[] adresKabli = new string[2];

        public string portManagera;
        public string[] otrzymaneDane;

        private StreamWriter writer;

        private StreamReader reader;

        public Form1()
        {
            InitializeComponent();
            
            try {
                
            }
            catch (Exception)
            {
                Console.WriteLine("Nie udalo sie polaczyc z kablami");
            }
            
        }

        public static string getIPAdress()
        {
            IPHostEntry host;
            string localIP = "?";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                }
            }
            return localIP;
        }

        public void odczytaj(string returndata)
        {
            Console.WriteLine(returndata);

            if (!returndata.Equals(""))
               {
                   CoDostalem.Invoke(new Action(delegate()
                     {
                        CoDostalem.AppendText(returndata +"\n\r");
                     }));
               }
            otrzymaneDane = returndata.Split('#');
            Console.WriteLine(otrzymaneDane.ToString());
            
            switch (otrzymaneDane[0])
            {
                    //To zostalo z pierwszego etapu, nie wiem czy nadal odpowiednio zadziala
                case "TABLICA":
                                for (int i = 2; i < otrzymaneDane.Length; i++)
                                {
                                    if (!otrzymaneDane[i].Contains("KONIEC"))
                                    {
                                        clientAddresses[i - 2] = otrzymaneDane[i];
                                    }
                                }
                                client.zapelnijTablice(clientAddresses);
                                break;
                case "CallAccept":
                                CoDostalem.Invoke(new Action(delegate()
                                {
                                    string nazwaZadajacego = otrzymaneDane[1];
                                    string przeplywnoscPrzychodzaca = otrzymaneDane[3];
                                    string idPolaczeniaPrzychodzacego = otrzymaneDane[4];
                                    var confirmResult = MessageBox.Show("Czy chcesz przyjac polaczenie?",
                                     "Potwierdz",
                                     MessageBoxButtons.YesNo);
                                    if (confirmResult == DialogResult.Yes)
                                    {
                                        CoDostalem.AppendText(client.callAccept(myClient, nazwaZadajacego, id,przeplywnoscPrzychodzaca, idPolaczeniaPrzychodzacego, "CONFIRMATION") + "\n\r");
                                        czyZestawione = true;
                                        string nowaNazwaPolaczenia = otrzymaneDane[4];
                                        CoDostalem.Invoke(new Action(delegate()
                                        {
                                            //idnad idadr przep idpol
                                            NazwaPolaczenia.Clear();
                                            NazwaPolaczenia.AppendText(nowaNazwaPolaczenia);
                                        }));
                                        zestawianePolaczenia.Add(nowaNazwaPolaczenia, true);
                                        for (int i = 0; i < clientAddresses.Length; i++)
                                        {
                                            if (clientAddresses[i] != null)
                                            {
                                                if (clientAddresses[i].Contains(idPolaczeniaPrzychodzacego + "idPolaczenia"))
                                                {
                                                    if (clientAddresses[i + 3] != null)
                                                    {
                                                        //dopisanie danych do uzyskanych przez LinkConnectionRequest
                                                        clientAddresses[i + 1] = nazwaZadajacego;
                                                        clientAddresses[i + 2] = "127.0.0.1";
                                                        clientAddresses[i + 3] = clientAddresses[i+3] +"/15";
                                                        if (clientAddresses[i + 4] == null)
                                                        {
                                                            clientAddresses[i + 4] = "nextHOP";
                                                        }
                                                        break;
                                                    }
                                                }
                                                i = i + 4;
                                            }
                                            else
                                            {
                                                //nie znaleziono wpisu do takiego polaczenia, wiec sami go tworzymy
                                                clientAddresses[i] = idPolaczeniaPrzychodzacego + "idPolaczenia";
                                                clientAddresses[i + 1] = nazwaZadajacego;
                                                clientAddresses[i + 2] = "127.0.0.1";
                                                clientAddresses[i + 3] = clientAddresses[i + 3] + "/15";
                                                clientAddresses[i + 4] = "nextHOP";
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        CoDostalem.AppendText(client.callAccept(myClient, otrzymaneDane[1], id, otrzymaneDane[3], otrzymaneDane[4], "REJECTION") + "\n\r");
                                        czyZestawione = false;
                                    }
                                }));
                                
                                
                                break;
                case "CallRelease":
                                CoDostalem.Invoke(new Action(delegate()
                                {
                                    //idnada idadre idpol
                                    CoDostalem.AppendText(returndata + "\n\r");
                                    czyZestawione = false;
                                }));
                                String nazwaPolaczeniaOdMenago = otrzymaneDane[3];
                                zestawianePolaczenia[nazwaPolaczeniaOdMenago] = false;
                                break;
                case "CallRequest":
                                if (otrzymaneDane[5].Equals("REJECTION"))
                                {
                                    CoDostalem.Invoke(new Action(delegate()
                                    {

                                        CoDostalem.AppendText(returndata + "\n\r");
                                        czyZestawione = false;
                                    }));
                                }
                                else
                                {
                                    // mojeID IDadresata przepustowosc idPolaczenia CONF
                                    if (otrzymaneDane[5].Equals("CONFIRMATION"))
                                    {
                                        CoDostalem.Invoke(new Action(delegate()
                                        {
                                            CoDostalem.AppendText(returndata + "\n\r");
                                            czyZestawione = true;
                                        }));

                                        string idOdMenago = otrzymaneDane[4];
                                        CoDostalem.Invoke(new Action(delegate()
                                        {
                                           //idnad idadr przep idpol
                                             NazwaPolaczenia.Clear();
                                            NazwaPolaczenia.AppendText(idOdMenago);
                                        }));
                                        string doKogoMenago = otrzymaneDane[2];
                                        for (int i = 0; i < clientAddresses.Length; i++)
                                        {
                                            if (clientAddresses[i] != null)
                                            {
                                                if (clientAddresses[i].Contains( idOdMenago + "idPolaczenia"))
                                                {
                                                    if (clientAddresses[i + 3] != null)
                                                    {
                                                        clientAddresses[i + 1] = doKogoMenago;
                                                        clientAddresses[i + 2] = "127.0.0.1";
                                                        clientAddresses[i + 3] = clientAddresses[i+3] +"/15";
                                                        if (clientAddresses[i + 4] == null)
                                                        {
                                                            clientAddresses[i + 4] = "nextHop" + doKogoMenago;
                                                        }
                                                        break;
                                                    }
                                                }
                                                i = i + 4;
                                            }
                                            
                                            else
                                            {
                                                //nazwa adresata,IP, etykieta, TTL, next hop
                                                clientAddresses[i] = idOdMenago + "idPolaczenia";
                                                clientAddresses[i + 1] = doKogoMenago;
                                                clientAddresses[i + 2] = "127.0.0.1";
                                                clientAddresses[i + 3] = clientAddresses[i+3] +"/15";
                                                clientAddresses[i + 4] = "nextHop" + doKogoMenago;
                                                break;
                                            }
                                        }
                                        zestawianePolaczenia.Add(idOdMenago, true);
                                            client.zapelnijTablice(clientAddresses);
                                            for (int i = 0; i < clientAddresses.Length; i++)
                                            {
                                                if (clientAddresses[i] != null)
                                                {

                                                    Console.WriteLine(i + ":" + clientAddresses[i]);
                                                }
                                            }
                                    }
                                }
                                break;
                case "LinkConnectionRequest":
                                CoDostalem.Invoke(new Action(delegate()
                                {
                                    CoDostalem.AppendText(returndata + "\n\r");
                                }));
                                string idPolaczenia = otrzymaneDane[1];
                                string odKogo = otrzymaneDane[2];
                                string doKogo = otrzymaneDane[3];
                                //if (odKogo.Equals(doKogo))
                                //{
                                    for (int i = 0; i < clientAddresses.Length; i++)
                                    {
                                        if (clientAddresses[i] != null)
                                        {
                                            if (clientAddresses[i] == idPolaczenia + "idPolaczenia")
                                            {
                                                if (clientAddresses[i + 3] != null)
                                                {
                                                    //ewentualnie zmiana parametrow na idPolaczenia, doKogo, odKogo, tu probowalem zrobic dzialenie w obie strony
                                                    clientAddresses[i + 3] = client.negotiation(idPolaczenia, odKogo, doKogo) + clientAddresses[i+3];
                                                }
                                                else
                                                {
                                                    clientAddresses[i + 3] = client.negotiation(idPolaczenia, odKogo, doKogo);
                                                }
                                                
                                                clientAddresses[i + 4] = doKogo;
                                                if (id.Contains(clientAddresses[i+4]))
                                                {
                                                    clientAddresses[i + 4] = odKogo;
                                                }
                                                break;
                                            }
                                            i = i + 4;
                                        }
                                        else
                                        {
                                            clientAddresses[i] = idPolaczenia + "idPolaczenia";
                                            if (clientAddresses[i + 3] != null)
                                            {
                                                clientAddresses[i + 3] = client.negotiation(idPolaczenia, odKogo, doKogo) + clientAddresses[i + 3];
                                            }
                                            else
                                            {
                                                clientAddresses[i + 3] = client.negotiation(idPolaczenia, odKogo, doKogo);
                                            }
                                            clientAddresses[i + 4] = doKogo;
                                            break;
                                        }
                                    }
                                client.zapelnijTablice(clientAddresses);
                                Console.WriteLine("zawartosc client addresses");
                                for (int i = 0; i < clientAddresses.Length; i++)
                                {
                                    if (clientAddresses[i] != null)
                                    {

                                        Console.WriteLine(i + ":" +clientAddresses[i]);
                                    }
                                }
                                    break;
                case "KeepAlive":
                                 lock (stanSasiadow)
                                 {
                                     //uaktualnianie stanu w zaleznosci od czasu badz dopisanie pojawiajacego sie sasiada
                                     if (!stanSasiadow.ContainsKey(otrzymaneDane[1]))
                                     {
                                         stanSasiadow.Add(otrzymaneDane[1], DateTime.Now);
                                     }
                                     else
                                     {
                                         stanSasiadow[otrzymaneDane[1]] = DateTime.Now;
                                     }
                                 }
                                  break;
                default:
                                  CoDostalem.Invoke(new Action(delegate()
                                  {
                                      CoDostalem.AppendText(returndata + "\n\r");
                                  }));
                    break;
            }
            //if (otrzymaneDane[1].Equals("TABLICA"))
            //            {
            //                if (otrzymaneDane[0].Equals(portManagera))
            //                {
            //                    for (int i = 2; i < otrzymaneDane.Length; i++)
            //                    {
            //                        if (!otrzymaneDane[i].Contains("KONIEC"))
            //                        {
            //                            clientAddresses[i - 2] = otrzymaneDane[i];
            //                        }
            //                    }
            //                    client.zapelnijTablice(clientAddresses);
            //                }
            //            }
            //            else
            //            {   if(otrzymaneDane[1].Equals("CallAccept")) {
            //                    CoDostalem.Invoke(new Action(delegate()
            //                        {
            //                            CoDostalem.AppendText(client.callAccept(myClient,otrzymaneDane[2],id) + "\n\r");
            //                            czyZestawione = true;
            //                        }));
            //                } else {
            //                    if (otrzymaneDane[1].Equals("CallRelease") || otrzymaneDane[1].Equals("CallRequest") && otrzymaneDane[4].Equals("REJECTION"))
            //                    {
            //                        CoDostalem.Invoke(new Action(delegate()
            //                        {
            //                            CoDostalem.AppendText(returndata + "\n\r");
            //                            czyZestawione = false;
            //                        }));
            //                    }
            //                    else
            //                    {
            //                        if(otrzymaneDane[1].Equals("KeepAlive")) {
            //                            CoDostalem.Invoke(new Action(delegate()
            //                            {
            //                                CoDostalem.AppendText(returndata + "\n\r");
            //                                czyZestawione = false;
            //                            }));
            //                            stanSasiadow[otrzymaneDane[2]] = DateTime.Now;

            //                        } else{
            //                            CoWysylam.Invoke(new Action(delegate()
            //                            {
            //                                CoWysylam.AppendText(returndata + "\n\r");
            //                            }));
            //                        }
            //                    }
            //                }
            //            }
                        //reader.Close();
        }

        public void nasluchuj()
        {
            string dane = "";
            while (true)
            {
                try
                {
                    string returndata = reader.ReadLine();
                    dane = returndata;
                    odczytaj(returndata);
                }

                catch (System.IO.IOException)
                {
                    Console.WriteLine("IO exception");
                }
                catch (System.InvalidOperationException)
                {
                    odczytaj(dane);
                }
                catch (Exception)
                {
                    //Console.WriteLine("Zlapano wyjatek, prawdopodobne rozlaczenie kabli");
                    //myClient.Close();
                    //try
                    //{
                    //    AsyncCallback callBack = new AsyncCallback(doSomething);
                    //    myClient.BeginConnect(IPAddress.Parse(adresKabli[0]), int.Parse(adresKabli[1]), callBack, myClient);
                    //    //myClient.BeginConnect(IPAddress.Parse(adresKabli[0]), int.Parse(adresKabli[1]));
                    //}

                    //catch (SocketException)
                    //{
                    //    Console.WriteLine("Socket exception");
                    //}
                    //Thread.Sleep(1000);
                    //nasluchuj();
                }
            }
        }


        private void jestem()
        {
            while (true)
            {
                
                try{
                string stringToSend = "JESTEM#" + client.mojeID.Split('@')[0] + "#" + client.getIPAdress();
                writer.WriteLine(stringToSend);
                writer.Flush();
                client.keepAlive(filePath);
                //CoWysylam.AppendText("\n" + stringToSend + "\n" + client.keepAlive(filePath));
                //CoWysylam.Invoke(new Action(delegate()
                //{
                //    CoWysylam.AppendText(stringToSend + "\n" + client.keepAlive(filePath) +"\n\r");
                //}));
                Thread.Sleep(3000);
                }
                catch (System.InvalidOperationException)
                {
                  
                } catch(System.IO.IOException) {
                    Console.WriteLine("IO exception");
                }
                catch (Exception)
                {
                    Console.WriteLine("Polaczenie przerwanie, czekam na dzialanie");
                    Thread.Sleep(1000);
                    jestem();
                }

            }
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                IDadresata = WpisywanaNazwaDocelowego.Text;
                WpisywanaNazwaDocelowego.Text = "IDadresata: " + IDadresata; 
            }
            catch (System.FormatException)
            {
                WpisywanaNazwaDocelowego.Text = "To nie jest poprawne ID";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            textToSend = NapiszCoChceszWyslac.Text;
            try
            {
            client.writeTextFromUser(textToSend);
            if (zestawianePolaczenia[NazwaPolaczenia.Text])
            {
                CoWysylam.Text = client.buildPackage(IDadresata, IPAdresata, id);
                client.sendPackage(myClient, IDadresata, IPAdresata, id);
            }
            else
            {
                CoWysylam.Text = "!!! Nie zestawione polaczenie !!!";
            }
            } catch(Exception) {
                CoWysylam.Text = "Nie podlaczony element, uruchom ponownie program.";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
        }

        private void textBox4_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            przestanWysylac = false;
            tekstOkresowy = NapiszCoChceszWyslac.Text;
            try
            {

            czestotliwosc = int.Parse(textBox2.Text);
            Thread th = new Thread(new ThreadStart(wyslijOkresowo));
            th.Start();
            }
            catch
            {
                CoWysylam.Text = "Czy czestotliwosc na pewno jest liczba i kable sa podlaczone?";
            }
            
        }

        private void wyslijOkresowo()
        {
                while (true)
                {
                    if (przestanWysylac)
                    {
                        break;
                    }
                    client.writeTextFromUser(textToSend);
                    CoWysylam.Invoke(new Action(delegate()
                    {
                        CoWysylam.AppendText(client.buildPackage(IDadresata,IPAdresata, id) + "\n");
                    }));
                    client.sendPackage(myClient, IDadresata, IPAdresata, id);

                    Thread.Sleep(czestotliwosc * 1000);
            }
            
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        public void wyslijJestem()
        {
            try
            {

            string stringToSend = "JESTEM#" + id.Split('@')[0] + "#" + getIPAdress();
            writer.WriteLine(stringToSend);
            writer.Flush();
            CoWysylam.AppendText("\n" +stringToSend + "\n" + client.keepAlive(filePath));
            
            }
            catch (System.InvalidOperationException)
            {
            }
            catch(Exception) {
                Console.WriteLine("Nie mozna pisac w niepolaczonym strumieniu");
                Thread.Sleep(1000);
                wyslijJestem();
            }
        }

        private void button4_Click_2(object sender, EventArgs e)
        {
            przestanWysylac = true;
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {

            Environment.Exit(0);
            } catch(Exception) {

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filePath = openFileDialog1.FileName;
                adresKabli = ServerClient.odczytajAdresKabliXML(filePath);
                string[] names = filePath.Split('\\', '.');
                this.Text = names[names.Length - 2];
            }
        }

        private void czyZyje()
        {
            string pierwszeWyslanie = "SYGNALIZUJ#" + id.Split('@')[0] + "#" + client.idRC + "#Topology#" + id;
            foreach (KeyValuePair<string, DateTime> entry in stanSasiadow)
            {
                pierwszeWyslanie = pierwszeWyslanie + "#" + entry.Key;
            }
            writer.WriteLine(pierwszeWyslanie);
            writer.Flush();
            while (true)
            {
                Thread.Sleep(9000);
                List<string> listaStraconychTemp = new List<string>();
                List<string> listaZywychTemp = new List<string>();
                string stringToSend = "";

                lock (this)
                {
                    //int i = 0;
                    //while (i < 2)
                    //{
                        try
                        {

                        
                        foreach (var entry in stanSasiadow.Keys)
                        {
                            TimeSpan roznicaCzasow = DateTime.Now.Subtract(stanSasiadow[entry]);
                            if (roznicaCzasow > TimeSpan.FromSeconds(5))
                            {
                                if (listaStraconych.Contains(entry))
                                {
                                    continue;
                                }
                                listaStraconychTemp.Add(entry);
                            }
                            else
                            {
                                if (listaStraconych.Contains(entry))
                                {
                                    listaStraconych.Remove(entry);
                                }
                                listaZywychTemp.Add(entry);
                            }
                        }
                    } catch(Exception) {
                        Console.WriteLine("exc w iterowaniu po kluczach");
                    }
                        if (listaStraconychTemp.Count != 0 || listaZywychTemp.Count != listaZywych.Count)
                        {
                            stringToSend = "SYGNALIZUJ#" + id.Split('@')[0] + "#" + client.idRC + "#Topology#" + id;
                            for (int j = 0; j < listaZywychTemp.Count; j++)
                            {
                                stringToSend = stringToSend + "#" + listaZywychTemp[j];
                            }
                            //if (stringToSend.Split('#').Length > 5)
                            //{
                            //    i = 2;
                            //    break;
                            //}
                            //else
                            //{
                            //    i++;
                            //    continue;
                            //}
                        //}   
                    }
                }
                if (stringToSend != " " && stringToSend != null && stringToSend != "")
                {
                    writer.WriteLine(stringToSend);
                    writer.Flush();
                Console.WriteLine("LRM do RC:\n" + stringToSend);
                }
                
                listaStraconych = listaStraconychTemp;
                listaZywych = listaZywychTemp;
                for (int i = 0; i < listaStraconych.Count; i++)
                {
                    if (stanSasiadow.ContainsKey(listaStraconych[i]))
                    {
                        stanSasiadow.Remove(listaStraconych[i]);
                    }
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            portManagera = ServerClient.odczytajPortManageraXML(filePath);
            

            try
            {
                if (myClient.Connected)
                {
                    myClient.Close();
                }
                myClient.Connect(adresKabli[0], int.Parse(adresKabli[1]));
                NetworkStream stream = myClient.GetStream();
                writer = new StreamWriter(stream);
                reader = new StreamReader(stream);
                id = ServerClient.odczytajID(filePath);
                client = new ServerClient(myClient, id, filePath);
                stanSasiadow = client.zapoczatkujStan(filePath);
            }
            catch(Exception)
            {
                Console.WriteLine("Nie udalo sie polaczyc.");
            }
            Thread th = new Thread(new ThreadStart(nasluchuj));
            th.Start();
            Thread th2 = new Thread(new ThreadStart(jestem));
            th2.Start();
            Thread th3 = new Thread(new ThreadStart(czyZyje));
            th3.Start();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            IPAdresata = IPDocelowegoWezla.Text;
            IPDocelowegoWezla.Text = "IPAdresata: " + IPAdresata;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            CoWysylam.Text = client.callRequest(myClient, IDadresata, id, WymaganaPojemnosc.Text);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            CoWysylam.Text = client.callRelease(myClient, IDadresata, id, NazwaPolaczenia.Text);
            zestawianePolaczenia[NazwaPolaczenia.Text] = false;
        }

        private void nazwaPolaczenia_TextChanged(object sender, EventArgs e)
        {

        }


    }
}

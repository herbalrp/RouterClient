using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace RouterClient
{

    class ServerClient
    {
        public string filepath = "C://Users//a//Desktop//ZST_projekt//RouterClient//RouterClient//XMLFile1.xml";
        string[] receivedData = new string[20];
        string userText = "";
        TcpClient myClient;
        
        public Dictionary<string, string> slownikPortow = new Dictionary<string,string>();
        public Dictionary<string, string[]> slownikPolaczen = new Dictionary<string, string[]>();
        public List<string> wykorzystaneEtykiety = new List<string>();


        public string mojeID = "123";
        public string portManagera = "";
        public string idManagera = "";
        public string idRC = "";
        public string[] adresKabli = new string[2];
        StreamWriter writer;

        /// <summary>
        /// nazwa adresata,IP, etykieta, TTL, next hop
        /// </summary>
        public string[] clientAddresses = new string[1024];

        public ServerClient(TcpClient myClient, string mojeID, string filepath)
        {
            portManagera = odczytajPortManageraXML(filepath);
            idManagera = odczytajIdManageraXML(filepath);
            idRC = odczytajIdRCXML(filepath);
            this.mojeID = mojeID;
            this.myClient = myClient;
            NetworkStream stream = myClient.GetStream();
            writer = new StreamWriter(stream);
            this.filepath = filepath;
            odczytajPorty(filepath);
        }

        //public void odczytajXML()
        //{
        //    XmlDocument doc = new XmlDocument();
        //    doc.Load("C://Users//a//Desktop//ZST_projekt//RouterClient//RouterClient//XMLFile1.xml");
        //    XmlNode node = doc.DocumentElement.SelectSingleNode("/Router");
        //    mojeID = node.Attributes["IDRoutera"].InnerText;
        //    portManagera = node.Attributes["PortManagera"].InnerText;
        //    adresKabli[0] = node.Attributes["IPKabli"].InnerText;
        //    adresKabli[1] = node.Attributes["PortKabli"].InnerText;
        //}

        public static string odczytajID(string filepath)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(@filepath);
                XmlNode node = doc.DocumentElement.SelectSingleNode("/Router");
                return node.Attributes["IDRoutera"].InnerText;
            }
            catch(Exception)
            {

            }
            return "";
        }

        public static string odczytajPortManageraXML(string filepath)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(@filepath);
                XmlNode node = doc.DocumentElement.SelectSingleNode("/Router");
                return node.Attributes["PortManagera"].InnerText;
            }
            catch(Exception)
            {
                
            }
            return "";
        }

        public static string odczytajIdManageraXML(string filepath)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(@filepath);
                XmlNode node = doc.DocumentElement.SelectSingleNode("/Router");
                return node.Attributes["idManagera"].InnerText;
            }
            catch (Exception)
            {

            }
            return "";
        }

        public static string odczytajIdRCXML(string filepath)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(@filepath);
                XmlNode node = doc.DocumentElement.SelectSingleNode("/Router");
                return node.Attributes["idRC"].InnerText;
            }
            catch (Exception)
            {

            }
            return "";
        }

        public static string[] odczytajAdresKabliXML(string filepath)
        {
            string[] adresKabli = new string[2];
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(@filepath);
                XmlNode node = doc.DocumentElement.SelectSingleNode("/Router");
                adresKabli[0] = node.Attributes["IPKabli"].InnerText;
                adresKabli[1] = node.Attributes["PortKabli"].InnerText;
                return adresKabli;
            }
            catch(Exception)
            {

            }
            return null;
        }


        public String getIPAdress()
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


        public void writeTextFromUser(string text)
        {
            userText = text;
        }

        public Dictionary<string, DateTime> zapoczatkujStan(string filepath)
        {
            XmlDocument doc = new XmlDocument();
            Dictionary<string, DateTime> slownik = new Dictionary<string,DateTime>();
            doc.Load(@filepath);
            for (int element = 0; element < doc.GetElementsByTagName("Router").Item(0).ChildNodes.Count; element++)
            {
                //pobiera ID
                string id = doc.GetElementsByTagName("Sasiad").Item(element).ChildNodes.Item(0).InnerText;
                DateTime stan = DateTime.Now;

                slownik.Add(id, stan);

            }
            return slownik;
        }

        public void odczytajPorty(string filepath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@filepath);
            for (int element = 0; element < doc.GetElementsByTagName("Router").Item(0).ChildNodes.Count; element++)
            {
                //pobiera ID
                string id = doc.GetElementsByTagName("Sasiad").Item(element).ChildNodes.Item(0).InnerText;
                string portWyjsciowy = doc.GetElementsByTagName("Sasiad").Item(element).ChildNodes.Item(1).InnerText;

                slownikPortow.Add(id.Split('@')[0], portWyjsciowy);
             
            }

        }

        public void zapelnijTablice(string[] tablica)
        {
            clientAddresses = tablica;
        }

        public string buildPackage(string IDadresata, string IPadresata, string id)
        {
            string stringToSend = "";
            for (int i = 0; i < clientAddresses.Length; i++)
            {
                if (clientAddresses[i].Equals(IDadresata) && clientAddresses[i] != null)
                {
                    stringToSend = "WYSLIJ#" + mojeID.Split('@')[0] + "#" +/*odczytany port */ slownikPortow[clientAddresses[i + 3]] + "#"/*podana etykieta*/
                        + clientAddresses[i + 2] +"#" /*IP*/+ getIPAdress() +"#" + userText + "->" +IPadresata  + "-" + id;
                    i++;
                    break;
                }
            }
            return stringToSend;
        }

        public string callRequest(TcpClient myClient, string IDadresata, string id, string pojemnosc)
        {
            string stringToSend = "";
            stringToSend = "SYGNALIZUJ#" + mojeID.Split('@')[0] + "#" + idManagera + "#CallRequest#" + mojeID + "#" + IDadresata + "#" + pojemnosc;
            writer.WriteLine(stringToSend);
            writer.Flush();
            Console.WriteLine("CallRequest: " + mojeID + " do " + IDadresata);
            return stringToSend;
        }

        public string callAccept(TcpClient myClient, string IDadresata, string id, string przepustowosc, string idPolaczenia, string odpowiedz)
        {
            string stringToSend = "";
            stringToSend = "SYGNALIZUJ#" + mojeID.Split('@')[0] + "#" + idManagera + "#CallAccept#" + IDadresata + "#" + mojeID + "#" +przepustowosc +"#" +idPolaczenia + "#" + odpowiedz;
            writer.WriteLine(stringToSend);
            writer.Flush();
            Console.WriteLine("CallAccept: " + mojeID + " do " + IDadresata + " CONFIRMATION");
            return stringToSend;
        }

        public string callRelease(TcpClient myClient, string IDadresata, string id, string nazwaPolaczenia)
        {
            string stringToSend = "";
            stringToSend = "SYGNALIZUJ#" + mojeID.Split('@')[0] + "#" + idManagera + "#CallRelease#" + mojeID + "#" + IDadresata + "#" + nazwaPolaczenia;
            writer.WriteLine(stringToSend);
            writer.Flush();
            Console.WriteLine("CallRelease: " + mojeID + " do " + IDadresata);
            return stringToSend;
        }

        public string negotiation(string idPolaczenia, string odKogo, string doKogo)
        {
            
            string stringToSend = "";
            Random rnd = new Random();
            int etykietaOdeMnie = rnd.Next(1, 25);
            while (true)
            {
                if (!wykorzystaneEtykiety.Contains(etykietaOdeMnie.ToString()))
                {
                    wykorzystaneEtykiety.Add(etykietaOdeMnie.ToString());
                    break;
                }
                else
                {
                    etykietaOdeMnie = rnd.Next(1, 25);
                    continue;
                }
            }

            stringToSend = "SYGNALIZUJ#" + mojeID.Split('@')[0] + "#" + doKogo.Split('@')[0] + "#Negotiation#" + idPolaczenia + "#"
                + mojeID + "#" + etykietaOdeMnie.ToString();
            writer.WriteLine(stringToSend);
            writer.Flush();
            Console.WriteLine("Negocjacje miedzy LRM");
            return etykietaOdeMnie.ToString();
        }

        public string keepAlive(string filepath)
        {
            string wysylanyString = "";
            XmlDocument doc = new XmlDocument();
            doc.Load(@filepath);
            for (int element = 0; element < doc.GetElementsByTagName("Router").Item(0).ChildNodes.Count; element++)
            {
                //pobiera ID
                string idAdresata = doc.GetElementsByTagName("Sasiad").Item(element).ChildNodes.Item(0).InnerText;

                string stringToSend = "SYGNALIZUJ#" + mojeID.Split('@')[0] + "#" + idAdresata.Split('@')[0] + "#KeepAlive#" + mojeID + "#" + DateTime.Now;
                
                Console.WriteLine("Keep Alive" + mojeID + "do" + idAdresata);
                writer.WriteLine(stringToSend);
                writer.Flush();
                wysylanyString = wysylanyString +"\n" + stringToSend;
            }
            return wysylanyString;
        }

        public void sendPackage(TcpClient myClient, string IDadresata, string IPadresata, string id)
        {
            string stringToSend = buildPackage(IDadresata, IPadresata, id);
            if (!stringToSend.Equals(""))
            {
                writer.WriteLine(stringToSend);
                writer.Flush();

            }
            else
            {
                Console.WriteLine("Error in bulding package. Package is not correct.");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Security.Cryptography;

namespace VPN
{
    public partial class Form1 : Form
    {
        // Private shared key (KAB)
        public static string sharedPrivateKey = null;
        public static byte[] sharedPrivateKey_bytes = null;

        // Nonce
        public static int nonce_client = 0;
        public static int nonce_server = 0;

        // Secret integer value
        public static int secretInt = 1;

        // Public base integer and prime number
        public static int g = 6;
        public static int p = 23;

        // Session key
        public static int SessionKeyPart_Server = 0;
        public static int SessionKeyPart_Client = 0;
        public static int SessionKey = 0;
        public static byte[] SessionKeyb;

        // authentication step
        public static int auth_server_step = 0;
        public static int auth_client_step = 0;
        public static bool authflag = false;

        // IV
        public static string IV = "0123456789012345";
        public static byte[] IVb = ASCIIEncoding.ASCII.GetBytes(IV);

        bool serverMode;
        Socket clientSocket, serverSocket;
        private byte[] byteData = new byte[1024];
        private Socket sending;

        private void GenerateNonce()
        {
            // Seed RNG
            Random rnd = new Random();
            string data;
            if (serverMode)
            {
                nonce_server = rnd.Next(10000, 99999);
                data = "nonce_server = " + nonce_server;
            }
            else
            {
                nonce_client = rnd.Next(10000, 99999);
                data = "nonce_client = " + nonce_client;
            }
            WriteConsoleOutput(data);
        }

        private void GenerateSecretValue()
        {
            // Seed RNG
            Random rnd = new Random();
            secretInt = rnd.Next(1, 10);

            // Write console output
            string data = "secret integer = " + secretInt;
            WriteConsoleOutput(data);
        }

        private void GeneratePrimes()
        {
            Random r = new Random();
            bool foundPrime = false;
            int num;
            // Generate g (base)
            g = r.Next(5, 30);

            // Generate p (prime)
            while (!foundPrime)
            {
                num = r.Next(100, 20000);
                if (isPrime(num))
                {
                    foundPrime = true;
                    p = num;
                }
            }

            // Write to output box
            WriteConsoleOutput("prime p = " + p);
            WriteConsoleOutput("base g = " + g);

            ShowPrimes();

        }

        public void ShowPrimes()
        {
            // Show in the boxes
            basetextbox.Text = g.ToString();
            primetextbox.Text = p.ToString();
            basetextbox.ReadOnly = true;
            primetextbox.ReadOnly = true;
        }

        private void GenerateSessionPart()
        {
            if (serverMode)
            {
                SessionKeyPart_Server = IntPow(g, secretInt) % p;
                // Write to console
                WriteConsoleOutput("Session key part = " + SessionKeyPart_Server);
            }
            else
            {
                SessionKeyPart_Client = IntPow(g, secretInt) % p;
                // Write to console
                WriteConsoleOutput("Session key part = " + SessionKeyPart_Client);
            }
        }

        private void GenerateSessionKey()
        {
                if (serverMode)
                {
                    SessionKey = IntPow(SessionKeyPart_Client, secretInt) % p;
                }
                else
                {
                    SessionKey = IntPow(SessionKeyPart_Server, secretInt) % p;

                }
                //Write to console
                WriteConsoleOutput("Session key = " + SessionKey);
        }

        // Writes to the output box
        public void WriteConsoleOutput(string data)
        {
            if (serverMode)
            {
                data = "Server: " + data;
            }
            else
            {
                data = "Client: " + data;
            }
            ConsoleOutputBox.Items.Add(data);
        }

        public static bool isPrime(int number)
        {
            int boundary = (int)Math.Floor(Math.Sqrt(number));

            if (number == 1) return false;
            if (number == 2) return true;

            for (int i = 2; i <= boundary; ++i)
            {
                if (number % i == 0) return false;
            }

            return true;
        }




        // Generic send data function
        public void SendDataBytes(byte[] data)
        {
            WriteConsoleOutput("Send: " + ASCIIEncoding.ASCII.GetString(data));
            if (serverMode)
            {
                try
                {
                    //Send it to the client
                    byte[] bytes = data;
                    sending.BeginSend(bytes, 0, bytes.Length, SocketFlags.None, new AsyncCallback(OnSend), sending);
                    bytes = null;
                }
                catch (Exception)
                {
                    MessageBox.Show("Unable to send message to the server.", "Client ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                try
                {
                    //Send it to the server
                    byte[] bytes = data;
                    clientSocket.BeginSend(bytes, 0, bytes.Length, SocketFlags.None, new AsyncCallback(OnSend), null);
                    bytes = null;
                }
                catch (Exception)
                {
                    MessageBox.Show("Unable to send message to the server.", "Client ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        // Generic send data function
        public void SendDataString(string data)
        {
            WriteConsoleOutput("Send: " + data);
            if (serverMode)
            {
                try
                {
                    byte[] bytes = ASCIIEncoding.ASCII.GetBytes(data);
                    listMessageLink.Items.Add(data);
                    //Send it to the client
                    sending.BeginSend(bytes, 0, bytes.Length, SocketFlags.None, new AsyncCallback(OnSend), sending);
                    bytes = null;
                }
                catch (Exception)
                {
                    MessageBox.Show("Unable to send message to the server.", "Client ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                try
                {
                    byte[] bytes = ASCIIEncoding.ASCII.GetBytes(data);
                    listMessageLink.Items.Add(data);
                    //Send it to the server
                    clientSocket.BeginSend(bytes, 0, bytes.Length, SocketFlags.None, new AsyncCallback(OnSend), null);
                    bytes = null;
                }
                catch (Exception)
                {
                    MessageBox.Show("Unable to send message to the server.", "Client ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        public Form1()
        {
            InitializeComponent();

            // Server mode is select on initialize
            serverMode = true;
            clientGroupBox.Visible = false;
            serverGroupBox.Visible = true;
            ContinueButton.Visible = true;
            // set send button to be read only
            button2.Enabled = false;
        }

        // Check mode based on which radio button is selected
        private void serverRB_CheckedChanged(object sender, EventArgs e)
        {
            if (serverRB.Checked)
            {
                serverMode = true;
                clientGroupBox.Visible = false;
                serverGroupBox.Visible = true;
            }
            else
            {
                serverMode = false;
                serverGroupBox.Visible = false;
                clientGroupBox.Visible = true;
            }
        }


        // Check mode based on which radio button is selected
        private void clientRB_CheckedChanged(object sender, EventArgs e)
        {
            if (serverRB.Checked)
            {
                serverMode = true;
                clientGroupBox.Visible = false;
                serverGroupBox.Visible = true;
            }
            else
            {
                serverMode = false;
                serverGroupBox.Visible = false;
                clientGroupBox.Visible = true;
            }
        }

        // Connect button
        private void button1_Click(object sender, EventArgs e)
        {
            if (serverMode)
            {
                try
                {

                    serverSocket = new Socket(AddressFamily.InterNetwork,
                                              SocketType.Stream,
                                              ProtocolType.Tcp);


                    IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, Int32.Parse(textServerPort.Text));

                    //Bind and listen on the given address
                    serverSocket.Bind(ipEndPoint);
                    serverSocket.Listen(4);

                    //Accept the incoming clients
                    serverSocket.BeginAccept(new AsyncCallback(OnAccept), null);

                    // Output onto console box
                    WriteConsoleOutput("Accepting connections");

                    // Set button to be readonly
                    button1.Enabled = false;

                    // set authenticate button visible
                    ContinueButton.Visible = true;
                    ContinueButton.Enabled = true;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Server",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                try
                {
                    clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                    IPAddress ip = IPAddress.Parse(textClientIP.Text);
                    IPEndPoint ipEndPoint = new IPEndPoint(ip, Int32.Parse(textClientPort.Text));

                    clientSocket.BeginConnect(ipEndPoint, new AsyncCallback(OnConnect), null);
                    // Output onto console box
                    WriteConsoleOutput("Connected to " + ip.ToString());

                    // set authenticate button visible
                    ContinueButton.Visible = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "client", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                byteData = new byte[1024];
                //Start listening to the data asynchronously
                clientSocket.BeginReceive(byteData,
                                           0,
                                           byteData.Length,
                                           SocketFlags.None,
                                           new AsyncCallback(OnReceive),
                                           null);

                // Set button to be readonly
                button1.Enabled = false;
            }


        }

        private void OnReceive(IAsyncResult ar)
        {
            if (serverMode)
            {
                try
                {
                    Socket clientSocket = (Socket)ar.AsyncState;
                    clientSocket.EndReceive(ar);
                    ASCIIEncoding enc = new ASCIIEncoding();
                    string data = enc.GetString(byteData);

                    listMessageLink.Items.Add(data);

                    //  byte[] bytes = Encoding.ASCII.GetBytes(data);
                    //clientSocket.BeginSend(bytes, 0, bytes.Length, SocketFlags.None,
                    //                  new AsyncCallback(OnSend), clientSocket);
                    clientSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(OnReceive), clientSocket);
                    // Output onto console box
                    //WriteConsoleOutput("Recv: "+data);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Server", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                try
                {
                    clientSocket.EndReceive(ar);

                    ASCIIEncoding enc = new ASCIIEncoding();
                    string data = enc.GetString(byteData);
                    listMessageLink.Items.Add(data);

                    //Accordingly process the message received

                    clientSocket.BeginReceive(byteData,
                                              0,
                                              byteData.Length,
                                              SocketFlags.None,
                                              new AsyncCallback(OnReceive),
                                              null);
                    // Output onto console box
                    WriteConsoleOutput("Recv: " + data);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "clientTCP: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void OnConnect(IAsyncResult ar)
        {
            try
            {
                clientSocket.EndConnect(ar);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "client", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Send button
        private void button2_Click(object sender, EventArgs e)
        {
            if (serverMode)
            {
                listMessageLink.Items.Add(textSendMessage.Text);
                // Unencrypted message
                string unencr_message = textSendMessage.Text;

                // Output onto console box
                WriteConsoleOutput("Send: " + unencr_message);

                try
                {

                    byte[] bytes = Cryptography.Encrypt(unencr_message, IVb,SessionKeyb);

                    //Send it to the client
                    sending.BeginSend(bytes, 0, bytes.Length, SocketFlags.None, new AsyncCallback(OnSend), sending);
                    bytes = null;

                    //sending.BeginSend (encrypted, 0, encrypted.Length, SocketFlags.None, new AsyncCallback(OnSend), sending);
                    //encrypted = null;

                    textSendMessage.Text = "";
                }
                catch (Exception)
                {
                    MessageBox.Show("Unable to send message to the server.", "Client ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                try
                {

                    // Unencrypted message
                    string unencr_message = textSendMessage.Text;

                    // Encrypt messsage
                    byte[] bytes = Cryptography.Encrypt(unencr_message, IVb, SessionKeyb);

                    //Send it to the server
                    clientSocket.BeginSend(bytes, 0, bytes.Length, SocketFlags.None, new AsyncCallback(OnSend), null);
                    //clientSocket.BeginSend(encrypted, 0, encrypted.Length, SocketFlags.None, new AsyncCallback(OnSend), null);
                    listMessageLink.Items.Add(textSendMessage.Text);
                    bytes = null;

                    // Output onto console box
                    WriteConsoleOutput("Send: " + ASCIIEncoding.ASCII.GetString(bytes));

                    textSendMessage.Text = "";
                }
                catch (Exception)
                {
                    MessageBox.Show("Unable to send message to the server.", "Client ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void OnSend(IAsyncResult ar)
        {
            if (serverMode)
            {
                try
                {
                    Socket client = (Socket)ar.AsyncState;
                    client.EndSend(ar);


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Server", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                try
                {
                    clientSocket.EndSend(ar);
                }
                catch (ObjectDisposedException)
                { }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Client ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void OnAccept(IAsyncResult ar)
        {
            try
            {
                Socket clientSocket = serverSocket.EndAccept(ar);
                sending = clientSocket;

                //Start listening for more clients
                serverSocket.BeginAccept(new AsyncCallback(OnAccept), null);

                //Once the client connects then start receiving the commands from her
                clientSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None,
                    new AsyncCallback(OnReceive), clientSocket);

                // Print 
                WriteConsoleOutput("Client connected");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Server",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Button 3 and 4 are the "save" buttons on the form
        private void button3_Click(object sender, EventArgs e)
        {
            sharedPrivateKey = textServerSharedKey.Text;
            textServerSharedKey.ReadOnly = true; // grey out box

            int size = 16 - sharedPrivateKey.Length;

            for (int i = 0; i < size; i++)
            {
                // pad with zeros
                sharedPrivateKey += "0";
            }
            sharedPrivateKey_bytes = ASCIIEncoding.ASCII.GetBytes(sharedPrivateKey);
            WriteConsoleOutput(sharedPrivateKey);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            sharedPrivateKey = textClientSharedKey.Text;
            textClientSharedKey.ReadOnly = true; // grey out box

            int size = 16 - sharedPrivateKey.Length;

            for (int i = 0; i < size; i++)
            {
                // pad with zeros
                sharedPrivateKey += "0";
            }

                sharedPrivateKey_bytes = ASCIIEncoding.ASCII.GetBytes(sharedPrivateKey);
                WriteConsoleOutput(sharedPrivateKey);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click_1(object sender, EventArgs e)
        {

        }

        private void ContinueButton_Click(object sender, EventArgs e)
        {
            if (serverMode)
            {
                auth_server_step++;
                WriteConsoleOutput("" + auth_server_step);
                ASCIIEncoding enc = new ASCIIEncoding();
                string data = enc.GetString(byteData);

                if (auth_server_step == 1)
                {
                    WriteConsoleOutput("buffered data = " + data);
                    // fetch nonce_c
                    nonce_client = Convert.ToInt32(data);

                    // generate nonce_s
                    GenerateNonce();

                    // Generate secret int (b)
                    GenerateSecretValue();

                    // Generate server session key half (B = g^b mod p)
                    GenerateSessionPart();

                    // send nonce_s
                    SendDataString("" + nonce_server);

                }
                if (auth_server_step == 2)
                {

                    WriteConsoleOutput("buffered data = " + data);
                    // Encrypt and send E("nonce_c,B", Kab)
                    string plain = nonce_client + "," + SessionKeyPart_Server;
                    WriteConsoleOutput("DEBUG: plain = " + plain);

                    byte[] cipher = Cryptography.Encrypt(plain, IVb, sharedPrivateKey_bytes);
                    SendDataBytes(cipher);
                    WriteConsoleOutput("waiting for client to send encrypted auth");

                }

                if (auth_server_step == 3)
                {
                    WriteConsoleOutput("buffered data = " + data);
                    // Decrypt client's encrypted auth message
                    string decrypted = Cryptography.Decrypt(byteData, IVb, sharedPrivateKey_bytes);
                    WriteConsoleOutput("DEBUG: decrypted = " + decrypted);
                    // Explode into substrings
                    string[] substrings = decrypted.Split(',');

                    // Compare nonces
                    if (!nonce_client.Equals(substrings[0]))
                    {
                        // print output
                        WriteConsoleOutput("nonce_c is not equal!");

                        // close socket

                    }

                    // use A to get session key
                    SessionKeyPart_Client = Convert.ToInt32(substrings[1]);
                    // Generate session key
                    GenerateSessionKey();
                    WriteConsoleOutput("generated session key");

                    // Hide auth and continue button
                    ContinueButton.Enabled = false;
                }


            }
            else
            {
                auth_client_step++;
                WriteConsoleOutput("" + auth_client_step);
                ASCIIEncoding enc = new ASCIIEncoding();
                string data = enc.GetString(byteData);


                if (auth_client_step == 1)
                {
                    WriteConsoleOutput("buffered data = " + data);
                    // Generate nonce_c
                    GenerateNonce();
                    // Send nonce_c
                    SendDataString("" + nonce_client);

                    WriteConsoleOutput("waiting for server to send nonce");

                }
                if (auth_client_step == 2)
                {
                    // Get nonce_s

                    WriteConsoleOutput("buffered data = " + data);
                    nonce_server = Convert.ToInt32(data);
                    WriteConsoleOutput("nonce_s =" + nonce_server);

                    // Generate A = g^a mod p
                    GenerateSecretValue();
                    GenerateSessionPart();

                    WriteConsoleOutput("waiting for server to send encrypted auth");
                }
                if (auth_client_step == 3)
                {
                    // Receive encrypted data
                    WriteConsoleOutput("buffered data = " + data);
                    string decrypted = Cryptography.Decrypt(byteData, IVb, sharedPrivateKey_bytes);
                    string[] substrings = decrypted.Split(',');

                    if (!nonce_server.Equals(substrings[0]))
                    {
                        // terminate
                        WriteConsoleOutput("nonce_s is not equal!");
                    }


                    // Get B from substrings
                    SessionKeyPart_Server = Convert.ToInt32(substrings[1]);

                    // Encrypt("nonce_s, A", Kab)
                    string plainText = nonce_server + "," + SessionKeyPart_Client;
                    byte[] encrypted = Cryptography.Encrypt(plainText, IVb, sharedPrivateKey_bytes);

                    // Send encrypted
                    SendDataBytes(encrypted);

                    // Calclate session key
                    GenerateSessionKey();
                    WriteConsoleOutput("generated session key");

                    // Hide auth and continue button
                    ContinueButton.Enabled = false;
                }

            }
        }

        int IntPow(int a, int b)
        {
            int answer = a;
            int i;
            for (i = 0; i < b - 1; i++)
                answer *= a;
            return answer;
        }

        public static class Cryptography
        {
            /*
            #region Settings

            private static int _iterations = 2;
            private static int _keySize = 256;

            private static string _hash = "SHA1";
            private static string _salt = "aselrias38490a32"; // Random
            private static string _vector = "8947az34awl34kjq"; // Random

            #endregion

            public static string Encrypt(string value, string password)
            {
                return Encrypt<AesManaged>(value, password);
            }
            public static string Encrypt<T>(string value, string password)
                    where T : SymmetricAlgorithm, new()
            {
                byte[] vectorBytes = ASCIIEncoding.ASCII.GetBytes(_vector);
                byte[] saltBytes = ASCIIEncoding.ASCII.GetBytes(_salt);
                byte[] valueBytes = UTF8Encoding.UTF8.GetBytes(value);

                byte[] encrypted;
                using (T cipher = new T())
                {
                    PasswordDeriveBytes _passwordBytes =
                        new PasswordDeriveBytes(password, saltBytes, _hash, _iterations);
                    byte[] keyBytes = _passwordBytes.GetBytes(_keySize / 8);

                    cipher.Mode = CipherMode.CBC;

                    using (ICryptoTransform encryptor = cipher.CreateEncryptor(keyBytes, vectorBytes))
                    {
                        using (MemoryStream to = new MemoryStream())
                        {
                            using (CryptoStream writer = new CryptoStream(to, encryptor, CryptoStreamMode.Write))
                            {
                                writer.Write(valueBytes, 0, valueBytes.Length);
                                writer.FlushFinalBlock();
                                encrypted = to.ToArray();
                            }
                        }
                    }
                    cipher.Clear();
                }
                return Convert.ToBase64String(encrypted);
            }

            public static string Decrypt(string value, string password)
            {
                return Decrypt<AesManaged>(value, password);
            }
            public static string Decrypt<T>(string value, string password) where T : SymmetricAlgorithm, new()
            {
                byte[] vectorBytes = ASCIIEncoding.ASCII.GetBytes(_vector); 
                byte[] saltBytes = ASCIIEncoding.ASCII.GetBytes(_salt);
                byte[] valueBytes = Convert.FromBase64String(value);

                byte[] decrypted;
                int decryptedByteCount = 0;

                using (T cipher = new T())
                {
                    PasswordDeriveBytes _passwordBytes = new PasswordDeriveBytes(password, saltBytes, _hash, _iterations);
                    byte[] keyBytes = _passwordBytes.GetBytes(_keySize / 8);

                    cipher.Mode = CipherMode.CBC;

                    try
                    {
                        using (ICryptoTransform decryptor = cipher.CreateDecryptor(keyBytes, vectorBytes))
                        {
                            using (MemoryStream from = new MemoryStream(valueBytes))
                            {
                                using (CryptoStream reader = new CryptoStream(from, decryptor, CryptoStreamMode.Read))
                                {
                                    decrypted = new byte[valueBytes.Length];
                                    decryptedByteCount = reader.Read(decrypted, 0, decrypted.Length);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        return String.Empty;
                    }

                    cipher.Clear();
                }
                return Encoding.UTF8.GetString(decrypted, 0, decryptedByteCount);
            }

        }
    }*/

            public static byte[] Encrypt(string plainText, byte[] Key, byte[] IV)
            {
                // Check arguments.
                if (plainText == null || plainText.Length <= 0)
                    throw new ArgumentNullException("plainText");
                if (Key == null || Key.Length <= 0)
                    throw new ArgumentNullException("Key");
                if (IV == null || IV.Length <= 0)
                    throw new ArgumentNullException("Key");
                byte[] encrypted;
                // Create an RijndaelManaged object
                // with the specified key and IV.
                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Mode = CipherMode.CBC;
                    aesAlg.ValidKeySize(128);
                    aesAlg.Key = Key;
                    aesAlg.IV = IV;

                    // Create a decrytor to perform the stream transform.
                    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                    // Create the streams used for encryption.
                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {

                                //Write all data to the stream.
                                swEncrypt.Write(plainText);
                            }
                            encrypted = msEncrypt.ToArray();
                        }
                    }
                }


                // Return the encrypted bytes from the memory stream.
                return encrypted;

            }

            public static string Decrypt(byte[] cipherText, byte[] Key, byte[] IV)
            {
                // Check arguments.
                if (cipherText == null || cipherText.Length <= 0)
                    throw new ArgumentNullException("cipherText");
                if (Key == null || Key.Length <= 0)
                    throw new ArgumentNullException("Key");
                if (IV == null || IV.Length <= 0)
                    throw new ArgumentNullException("Key");

                // Declare the string used to hold
                // the decrypted text.
                string plaintext = null;

                // Create an RijndaelManaged object
                // with the specified key and IV.
                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = Key;
                    aesAlg.IV = IV;

                    // Create a decrytor to perform the stream transform.
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    // Create the streams used for decryption.
                    using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {

                                // Read the decrypted bytes from the decrypting stream
                                // and place them in a string.
                                plaintext = srDecrypt.ReadToEnd();
                            }
                        }
                    }

                }

                return plaintext;
            }
        }
    }
}


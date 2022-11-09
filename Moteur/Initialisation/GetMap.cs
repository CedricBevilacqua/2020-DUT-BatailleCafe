using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using Moteur.Jeu;

namespace Moteur.Initialisation
{
    public class GetMap
    {
        private static Socket connector;
        public static Socket Connector
        {
            get { return connector; }
        }

        /// <summary>
        /// Se connecte au serveur puis récupère la carte.
        /// </summary>
        /// <returns>Trame de la carte récupérée</returns>
        public static byte[] StartGetMap()
        {
            connector = ConnectStart("localhost", 1213); //Connexion au serveur
            byte[] mapTrame = ConnectDownload(connector); //Téléchargement de la carte
            //ConnectClose(connector); //Arrêt de la connexion
            return mapTrame;
        }

        public byte[] GetStartGetMap()
        {
            return StartGetMap();
        }

        /// <summary>
        /// Etablit la connexion avec le serveur.
        /// </summary>
        /// <param name="ip">Adresse IP du serveur</param>
        /// <param name="port">Port de connexion du serveur</param>
        /// <returns>Socket utile à la communication avec le serveur</returns>
        private static Socket ConnectStart(String ip, int port)
        {
            //ATTENTION : Il faut gérer en amont SocketException si aucune connexion
            Socket connector = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); //Création de la socket
            connector.Connect(ip, port); //Contact avec le serveur
            return connector;
        }

        public Socket GetConnectStart(String ip, int port)
        {
            return ConnectStart(ip, port);
        }

        /// <summary>
        /// Télécharge les informations relatives à la carte depuis le serveur.
        /// </summary>
        /// <param name="connector">Socket de communication avec le serveur</param>
        /// <returns>Trame de la carte</returns>
        private static byte[] ConnectDownload(Socket connector)
        {
            byte[] buffer = new byte[309]; //Taille maximale calculée (2 * chiffre * 100 + separateur * 99 + barre * 10)
            connector.Receive(buffer); //Réception des données en bytes
            return buffer;
        }

        public byte[] GetConnectDownload(Socket connector)
        {
            return ConnectDownload(connector);
        }

        /// <summary>
        /// Déconnecte du serveur.
        /// </summary>
        /// <param name="connector">Socket de communication avec le serveur</param>
        private static void ConnectClose(Socket connector)
        {
            connector.Shutdown(SocketShutdown.Both); //Désactivation de la socket
            connector.Close(); //Libération de la socket
        }
    }
}

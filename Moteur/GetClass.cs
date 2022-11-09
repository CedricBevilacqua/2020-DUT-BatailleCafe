using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Moteur
{
    public class GetMap
    {
        public static byte[] getMap()
        {
            Socket connector = connectStart("51.91.120.237", 1212); //Connexion au serveur
            byte[] mapTrame = connectDownload(connector); //Téléchargement de la carte
            connectClose(connector); //Arrêt de la connexion
            return mapTrame;
        }

        private static Socket connectStart(String IP, int port)
        {
            //ATTENTION : Il faut gérer en amont SocketException si aucune connexion
            Socket connector = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); //Création de la socket
            connector.Connect(IP, port); //Contact avec le serveur
            return connector;
        }

        private static byte[] connectDownload(Socket connector)
        {
            byte[] buffer = new byte[309]; //Taille maximale calculée (2 * chiffre * 100 + separateur * 99 + barre * 10)
            connector.Receive(buffer); //Réception des données en bytes
            return buffer;
        }
        
        private static void connectClose(Socket connector)
        {
            connector.Shutdown(SocketShutdown.Both); //Désactivation de la socket
            connector.Close(); //Libération de la socket
        }
    }
}

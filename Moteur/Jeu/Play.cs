using Moteur.Initialisation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moteur.Jeu
{
    public class Play
    {
        public static void GoPlay()
        {
            int lastPosX = 0;
            int lastPosY = 0;
            int compteur = 0;

            foreach(Parcelle parcelleEtudiee in Map.GetParcelles())
            {
                if (parcelleEtudiee.GetAssignedUnites().Count == 2)
                {
                    if (compteur == 2)
                    {
                        Map.PutGraine(parcelleEtudiee.GetAssignedUnites().ElementAt(0).GetposX(), parcelleEtudiee.GetAssignedUnites().ElementAt(0).GetposY(), 1);
                        byte[] messageBase = ConvertToByteArray("A:" + parcelleEtudiee.GetAssignedUnites().ElementAt(0).GetposY() + parcelleEtudiee.GetAssignedUnites().ElementAt(0).GetposX());
                        SendToServer(messageBase);
                        byte[] validationBase = ReceiveFromServer(4);
                        byte[] enemyPlayReceivedBase = ReceiveFromServer(4);
                        String enemyPlayBase = ConvertToString(enemyPlayReceivedBase);
                        lastPosX = int.Parse(enemyPlayBase[3].ToString());
                        lastPosY = int.Parse(enemyPlayBase[2].ToString());
                        byte[] continueTheGameBase = ReceiveFromServer(4);
                        Map.PutGraine(int.Parse(enemyPlayBase[3].ToString()), int.Parse(enemyPlayBase[2].ToString()), 2);
                        break;
                    }
                    compteur++;
                }
            }

            //Map.PutGraine(5, 3, 1);
            //byte[] messageBase = ConvertToByteArray("A:35");
            //SendToServer(messageBase);
            //byte[] validationBase = ReceiveFromServer(4);
            //byte[] enemyPlayReceivedBase = ReceiveFromServer(4);
            //String enemyPlayBase = ConvertToString(enemyPlayReceivedBase);
            //lastPosX = int.Parse(enemyPlayBase[3].ToString());
            //lastPosY = int.Parse(enemyPlayBase[2].ToString());
            //byte[] continueTheGameBase = ReceiveFromServer(4);
            //Map.PutGraine(int.Parse(enemyPlayBase[3].ToString()), int.Parse(enemyPlayBase[2].ToString()), 2);

            String continueServerAnswer = "";
            String enemyPlay = "";
            do
            {
                String messageToSend = IA.Play(lastPosX, lastPosY);
                Map.PutGraine(int.Parse(messageToSend[3].ToString()), int.Parse(messageToSend[2].ToString()), 1);
                byte[] message = ConvertToByteArray(messageToSend);
                SendToServer(message);
                byte[] validation = ReceiveFromServer(4);
                byte[] enemyPlayReceived = ReceiveFromServer(4);
                enemyPlay = ConvertToString(enemyPlayReceived);
                if(enemyPlay != "FINI")
                {
                    lastPosX = int.Parse(enemyPlay[3].ToString());
                    lastPosY = int.Parse(enemyPlay[2].ToString());
                    Map.PutGraine(int.Parse(enemyPlay[3].ToString()), int.Parse(enemyPlay[2].ToString()), 2);
                    byte[] continueTheGame = ReceiveFromServer(4);
                    continueServerAnswer = ConvertToString(continueTheGame);
                }
            } while (continueServerAnswer != "FINI" && enemyPlay != "FINI");
        }

        public static byte[] ConvertToByteArray(String message)
        {
            byte[] convertedMessage = new byte[message.Length];
            int compteur = 0;
            foreach(Char caracter in message)
            {
                convertedMessage[compteur] = Convert.ToByte(caracter);
                compteur++;
            }
            return convertedMessage;
        }

        public byte[] GetConvertToByteArray(String message)
        {
            return ConvertToByteArray(message);
        }

        public static void SendToServer(byte[] message)
        {
            GetMap.Connector.Send(message);
        }

        public static byte[] ReceiveFromServer(int size)
        {
            byte[] message = new byte[size];
            GetMap.Connector.Receive(message);
            return message;
        }

        public static String ConvertToString(byte[] message)
        {
            String convertedMessage = Encoding.ASCII.GetString(message);
            return convertedMessage;
        }

        public String GetConvertToString(byte[] message)
        {
            return ConvertToString(message);
        }
    }
}

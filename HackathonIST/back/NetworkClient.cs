using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Sockets;
using System.Text;
using HackathonIST;
using HackathonIST.back;

namespace NetworkLibrary
{
	public class NetworkClient
	{
		private IPAddress ipAddress;
		private IPEndPoint endPoint;

		private Socket socket;

		private int HEADER_LENGTH = 32;
		private int REQUEST_LENGTH = 32;

		private string ADDRESS = "95.142.47.122";
		private int PORT = 8686;

		public void ConnectToServer()
		{
			socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

			try
			{
				socket.Connect(endPoint);
				//Console.WriteLine("Socket connected to {0}", socket.RemoteEndPoint.ToString());

				string handshake = "SECRET_HANDSHAKE";
				SendToServer(handshake, "handshake");
				string serverHandshake = GetServerAnswer();
				//Console.WriteLine(handshake + " = " + serverHandshake);
			}
			catch (SocketException e)
			{
				//Console.WriteLine("SocketException: " + e.Message);
			}
			catch (ArgumentNullException e)
			{
				//Console.WriteLine("ArgumentNullException: " + e.Message);
			}
			catch (Exception e)
			{
				//Console.WriteLine("UnknownException: " + e.Message);
			}
		}

		public void CloseConnection()
		{
			socket.Shutdown(SocketShutdown.Both);
			socket.Close();

			socket = null;

			//Console.WriteLine("Connection w/ server was closed");
		}

		private void SendToServer(string message, string header, string request = "")
		{
			string HEAD = header;
			string REQUEST = request;

			for (int i = HEAD.Length; i < HEADER_LENGTH; i++)
				HEAD += " ";

			for (int i = REQUEST.Length; i < REQUEST_LENGTH; i++)
				REQUEST += " ";

			byte[] byteStream = Encoding.UTF8.GetBytes(HEAD + REQUEST + message);

			try
			{
				socket.Send(byteStream, byteStream.Length, 0);
			}
			catch (SocketException e)
			{
				//Console.WriteLine("SocketError occured: " + e.Message);
			}
		}

		private string GetServerAnswer()
		{
			try
			{
				byte[] buffer = new byte[1024];

				StringBuilder sb = new StringBuilder();

				int byteRecv = 0;

				do
				{
					byteRecv = socket.Receive(buffer, 0, buffer.Length, 0);
					sb.Append(Encoding.UTF8.GetString(buffer, 0, byteRecv));
				}
				while (socket.Available > 0);

				//Console.WriteLine("Get answer from server: " + sb.ToString());
				return (sb.ToString());
			}
			catch (SocketException e)
			{
				return ("SocketException: " + e.Message);
			}
			catch (ArgumentNullException e)
			{
				return ("ArgumentNullException: " + e.Message);
			}
			catch (Exception e)
			{
				return ("UnknownException: " + e.Message);
			}
		}

		private string GetServerCmdAnswer(string message, string header, string request = "")
		{
			SendToServer(message, header, request);

			return GetServerAnswer();
		}

		public int GetPersonalID(string login)
		{
			string serverAnswer = GetServerCmdAnswer(login, "cmd", "GetPersonalID");

			if (serverAnswer == "Empty")
				return -1;
			else
				return int.Parse(serverAnswer);
		}

		public string[] GetBuilderConstructions()
		{
			string serverAnswer = GetServerCmdAnswer(BuilderData.builderID.ToString(), "cmd", "GetConstructionsByBuilder");

			if (serverAnswer == "Empty")
				return new string[0];

			string[] splitConstructions = serverAnswer.Split(new string[] { "<!>" }, StringSplitOptions.None);

			return splitConstructions;
		}

		public void SetBuilderLastGeoLocation()
		{
			string serverAnswer = GetServerCmdAnswer((BuilderData.builderID + ";" + BuilderData.geoLocation), "cmd", "SetBuilderGeoLocation");
		}

		public string GetBuilderGeoLocation()
		{
			string location = GetServerCmdAnswer(BuilderData.builderID.ToString(), "cmd", "GetBuilderGeoLocation");
			return location;
		}

		public void SendSOSSignal()
		{
			SendToServer(BuilderData.builderID.ToString(),"sos");
		}

		public void SendStartWorkRequest()
		{
			string serverAnswer = GetServerCmdAnswer(BuilderData.builderID.ToString(), "cmd", "StartWork");
		}
		
		public void SendEndWorkRequest()
		{
			string serverAnswer = GetServerCmdAnswer(BuilderData.builderID.ToString(), "cmd", "EndWork");
		}

		public bool SendRegisterRequest(string[] registerData)
		{
			string data = "";

			for (int i = 0; i < registerData.Length; i++)
				data += registerData[i] + ";";
			data = data.TrimEnd(';');

			string serverAnswer = GetServerCmdAnswer(data, "cmd", "RegisterUser");

			if (serverAnswer == "True")
				return true;
			else
				return false;
		}

		public bool SendAuthentificationRequest(string[] authData)
		{
			string data = "";

			for (int i = 0; i < authData.Length; i++)
				data += authData[i] + ";";
			data = data.TrimEnd(';');

			string serverAnswer = GetServerCmdAnswer(data, "cmd", "AuthUser");

			if (serverAnswer == "True")
				return true;
			else
				return false;
		}

		public NetworkClient()
		{
			ipAddress = IPAddress.Parse(ADDRESS);
			endPoint = new IPEndPoint(ipAddress, PORT);
		}
	}
}
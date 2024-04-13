using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

class Program {
    static List<Peer> peers = new List<Peer>();

    static void Main(string[] args) {
        int port = 8000;  // Porta padrão
        if (args.Length > 0) {
            port = int.Parse(args[0]);  // Aceita a porta como argumento
        }

        StartNode(port);
    }

    static void StartNode(int port) {
        TcpListener listener = new TcpListener(IPAddress.Any, port);
        listener.Start();
        Console.WriteLine("Node started on port " + port);

        while (true) {
            TcpClient client = listener.AcceptTcpClient();
            Peer peer = new Peer(client, "localhost:" + port);
            peers.Add(peer);

            Thread thread = new Thread(new ThreadStart(peer.Listen));
            thread.Start();
        }
    }
}


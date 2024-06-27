using System.Net.Sockets;
using System.Text;

public class Peer {
    public TcpClient client;
    public string address;
    public List<string> Blockchain = new List<string>();

    public Peer(TcpClient client, string address) {
        this.client = client;
        this.address = address;
    }

    public void Listen() {
        NetworkStream stream = this.client.GetStream();
        byte[] receivedBytes = new byte[1024];
        int byte_count;

        while ((byte_count = stream.Read(receivedBytes, 0, receivedBytes.Length)) > 0) {
            string data = Encoding.ASCII.GetString(receivedBytes, 0, byte_count);
            Console.WriteLine("Received: " + data);
            ProcessData(data);
        }
    }

    private void ProcessData(string data) {
        if (data == "Requesting Blockchain") {
            SendData(string.Join(",", Blockchain));
        }
    }

    public void SendData(string message) {
        byte[] data = Encoding.ASCII.GetBytes(message);
        NetworkStream stream = this.client.GetStream();
        stream.Write(data, 0, data.Length);
        Console.WriteLine("Sent: " + message);
    }
}

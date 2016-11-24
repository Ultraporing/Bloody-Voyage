using FastSockets.Networking;

public class BV_Client : BaseClient<BV_Packets, BV_Client>
{
    public BV_Client() : base("", 5)
    {

    }
}

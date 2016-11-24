using FastSockets.Networking;

public class BV_Server : BaseServer<BV_Packets, BV_Server>
{
    public BV_Server() : base("", EConnectionType.SECTOR_SERVER, 1, 5)
    {
        string cfgString = "PORT=1234;NAME=Herpderp;";
        LoadConfigFromString(cfgString);
        SetPort();
    }
}

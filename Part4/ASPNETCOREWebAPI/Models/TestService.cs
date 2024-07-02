namespace ASPNETCOREWebAPI.Models;

public class TestService
{
    private string[] files;
    public TestService()
    {
        files = Directory.GetFiles("F:\\于富", "*.dll", SearchOption.AllDirectories);
    }
    public int Count
    {
        get { return files.Length; }
    }
}

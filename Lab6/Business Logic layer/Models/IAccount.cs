using Data_Access_layer;

namespace Business_Logic_layer.Models;

public interface IAccount
{
    public string GetUserName();
    public bool IsPasswordCorrect(string password);
}
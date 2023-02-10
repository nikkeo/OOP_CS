using CustomExceptions;

namespace Banks.Models;

public class Client
{
    private string _name;
    private string _surname;
    private string _address;
    private string _passportInfo;
    public Client(string name, string surname, string address = "  ", string passportInfo = "  ")
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new NotCorrectClientNameException();
        if (string.IsNullOrWhiteSpace(surname))
            throw new NotCorrectClientSurnameException();

        _name = name;
        _surname = surname;
        _address = address;
        _passportInfo = passportInfo;
    }

    public string Name { get => _name; }
    public string Surname { get => _surname; }

    public string Address
    {
        get
        {
            return _address;
        }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new NotCorrectAddressException();
            _address = value;
            Verified = string.IsNullOrWhiteSpace(_address) && string.IsNullOrWhiteSpace(_passportInfo);
        }
    }

    public string PassportInfo
    {
        get
        {
            return _passportInfo;
        }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new NotCorrectPassportInfoException();
            _passportInfo = value;
            Verified = string.IsNullOrWhiteSpace(_address) && string.IsNullOrWhiteSpace(_passportInfo);
        }
    }

    public bool Verified { get; private set; }
}
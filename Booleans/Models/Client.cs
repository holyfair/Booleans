

namespace Booleans.Models
{
    internal class Client
    {
        #region Properties
        public string ClientId { get; set; }
        public string PassportNumber { get; set; }

        public string Surname { get; set; }
        public string Name { get; set; }
        #endregion


        #region Constructor
        public Client(string clientId, string passportNumber, string surname, string name)
        {
            ClientId = clientId;
            PassportNumber = passportNumber;
            Surname = surname;
            Name = name;
        }

        public Client(string surname, string name)
        {
            Surname = surname;
            Name = name;
        }
        #endregion

        public override string ToString()
        {
            return $"{nameof(ClientId)}: {ClientId}, {nameof(PassportNumber)}: {PassportNumber}, {nameof(Surname)}: {Surname}, {nameof(Name)}: {Name}";
        }

    }
}

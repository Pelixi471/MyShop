using System.ComponentModel.DataAnnotations;

namespace MyShop.Domain.Entites
{
    public class Account : IEntity
    {
        private string _passwordHash;
        private string _email;
        private string _name;
        private static readonly EmailAddressAttribute EmailAddressAttribute = new ();

#pragma warning disable CS8618
        protected Account()
#pragma warning disable CS8618
        { 
        }

        public Account(string name, string email, string passwordHash)
        {
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
        }

        public int Id { get; init; }
        public string Name
        {
            get => _name;

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("name can't be empty", nameof(value));
                if (value.Length is <3 or > 100)
                    throw new ArgumentException("the name should be between 3 and 100 characters", nameof(value));

                _name = value;
            }
        }
        public string Email
        {
            get => _email;

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("email can't be empty", nameof(value));
                if(!EmailAddressAttribute.IsValid(value)) 
                    throw new ArgumentException("the email address is invalid", nameof(value));

                _email = value;
            }
        }
        public string PasswordHash 
        { 
            get => _passwordHash;
            set 
            {
                if(string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("password can't be empty", nameof(value));

                _passwordHash = value; 
            }
        }
    }
}

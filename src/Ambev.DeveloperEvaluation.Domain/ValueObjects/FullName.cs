namespace Ambev.DeveloperEvaluation.Domain.ValueObjects
{
    public class FullName
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        private FullName() { } 

        public FullName(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name must not be empty.");

            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name must not be empty.");

            FirstName = firstName;
            LastName = lastName;
        }

        public override string ToString() => $"{FirstName} {LastName}";
    }
}

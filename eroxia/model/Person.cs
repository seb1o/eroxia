namespace eroxia.model
{
    internal abstract class Person
    {
        public string FiscalCode { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public Person(string fiscalCode, string name, string surname)
        {
            FiscalCode = fiscalCode;
            Name = name;
            Surname = surname;
        }
    }
}
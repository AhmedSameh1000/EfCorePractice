namespace ExecuteSqlRow
{
    public class Customer
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual decimal Balance { get; set; }

        public override string ToString()
        {
            return $"[{Id}], Name: {Name}, Balance: {Balance}";
        }
    }
}
